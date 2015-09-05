using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse;

public class ConnectParse : ConnectBase
{
    const float TIMEOUT_SECS = 60;
    const int SC_OK = 0;
    const int SC_NG = 1000;

    Item currentItem;
    bool running;

    public override bool IsLogIn { get { return ParseUser.CurrentUser != null; } }

    public override string UserName
    {
        get
        {
            if (ParseUser.CurrentUser == null)
            {
                return null;
            }
            return ParseUser.CurrentUser.Username;
        }
    }

    Queue<Item> requestQueue = new Queue<Item>();

    public ConnectParse(MonoBehaviour parentComponent, MTHandler mt)
        : base(mt)
    {
    }

    public override void SignUp(string username, string password, string displayName, System.Action finishCallback, System.Action errorCallback)
    {
        var user = new ParseUser()
        {
            Username = username,
            Password = password,
        };

        user["displayName"] = displayName;
        user.SignUpAsync().ContinueWith(signUpTask =>
            {
                if (signUpTask.IsFaulted || signUpTask.IsCanceled)
                {
                    JoinMT(errorCallback);
                }
                else
                {
                    JoinMT(finishCallback);
                }
            });
    }

    public override void LogIn(string username, string password, System.Action finishCallback, System.Action errorCallback)
    {
        if (ParseUser.CurrentUser != null)
        {
            finishCallback();
            return;
        }

        ParseUser.LogInAsync(username, password).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    JoinMT(errorCallback);
                }
                else
                {
                    JoinMT(finishCallback);
                }
            });
    }

    public override void LogOut()
    {
        ParseUser.LogOut();
    }

    public override void Request(CondBase cond, System.Action<CondBase.Response> finishCallback, System.Action<CondErrorR> errorCallback)
    {
        requestQueue.Enqueue(new Item(cond, finishCallback, errorCallback));

        if (running)
        {
            return;
        }

        Next();
    }

    void Next()
    {
        if (requestQueue.Count == 0)
        {
            running = false;
            return;
        }

        Item item = requestQueue.Dequeue();
        CondBase cond = item.cond;

        currentItem = item;
        running = true;

        // レスポンスを受け取ったらメインスレッドで処理する
        System.Action<CondBase.Response> onResponse = (response) =>
        {
            JoinMT(() =>
                {
                    currentItem.finishCallback(response);
                    currentItem = null;

                    Next();
                });
        };

        if (cond is CondConfigSync)
        {
            StartConfigLoad(cond, onResponse);
        }
        else if (cond is CondRegister)
        {
            StartRegister((cond as CondRegister), onResponse);
        }
        else if (cond is CondStart)
        {
            StartStart((cond as CondStart), onResponse);
        }
        else if (cond is CondCheck)
        {
            StartCheck((cond as CondCheck), onResponse);
        }
    }

    #region Sync Methods

    void StartConfigLoad(CondBase cond, System.Action<CondBase.Response> response)
    {
        ParseConfig.GetAsync().ContinueWith(t =>
            {
                CheckTaskResult(t, () =>
                    {
                        ParseConfig c = t.Result;

                        response(new CondConfigSync.R(
                                c.Get<int>("gameTime"),
                                cond)
                        );
                    }, response);
            });
    }

    void StartRegister(CondRegister cond, System.Action<CondBase.Response> response)
    {
        ParseCloud.CallFunctionAsync<Dictionary<string, object>>("register", cond.GetParams()).ContinueWith(t =>
            {
                Dictionary<string, object> result = t.Result;
                CheckCloudCodeStatus(t, result, code =>
                    {
                        response(new CondRegister.R(cond));
                    }, response);
            });
    }

    void StartStart(CondStart cond, System.Action<CondBase.Response> response)
    {
        ParseCloud.CallFunctionAsync<Dictionary<string, object>>("start", cond.GetParams()).ContinueWith(t =>
            {
                Dictionary<string, object> result = t.Result;
                CheckCloudCodeStatus(t, result, code =>
                    {
                        response(new CondStart.R(cond));
                    }, response);
            });
    }

    void StartCheck(CondCheck cond, System.Action<CondBase.Response> response)
    {
        ParseCloud.CallFunctionAsync<Dictionary<string, object>>("check", cond.GetParams()).ContinueWith(t =>
            {
                Dictionary<string, object> result = t.Result;
                CheckCloudCodeStatus(t, result, code =>
                    {
                        IList<object> enemies = result["enemies"] as IList<object>;
                        List<CondCheck.EnemyInfo> enemyList = new List<CondCheck.EnemyInfo>();
                        foreach (object o in enemies)
                        {
                            Dictionary<string, object> enemy = o as Dictionary<string, object>;

                            CondCheck.EnemyInfo info = new CondCheck.EnemyInfo();
                            enemyList.Add(info);

                            info.id = enemy["id"].ToString();
                            info.type = int.Parse(enemy["type"].ToString());
                            info.lat = double.Parse(enemy["lat"].ToString());
                            info.lon = double.Parse(enemy["lon"].ToString());
                        }

                        IList<object> teams = result["team"] as IList<object>;
                        List<CondCheck.TeamInfo> teamList = new List<CondCheck.TeamInfo>();
                        foreach (object o in teams)
                        {
                            Dictionary<string, object> team = o as Dictionary<string, object>;

                            CondCheck.TeamInfo info = new CondCheck.TeamInfo();
                            teamList.Add(info);

                            info.id = int.Parse(team["id"].ToString());
                            info.point = int.Parse(team["point"].ToString());
                        }

                        response(new CondCheck.R(enemyList, teamList, cond));
                    }, response);
            });
    }

    #endregion

    /// <summary>
    /// タスクの汎用確認を行い、失敗、キャンセルの場合はnullでレスポンスを返す
    /// </summary>
    void CheckTaskResult(Task t, System.Action successCallback, System.Action<CondBase.Response> response)
    {
        if (t.IsFaulted || t.IsCanceled)
        {
            response(null);
        }
        else
        {
            successCallback();
        }
    }

    /// <summary>
    /// CloudCodeのコードを確認して汎用エラーならnullレスポンス
    /// </summary>
    void CheckCloudCodeStatus(Task t, IDictionary<string, object> result, System.Action<int> successCallback, System.Action<CondBase.Response> response)
    {
        CheckTaskResult(t, () =>
            {
                int code = int.Parse(result["code"].ToString());
                if (code == SC_NG)
                {
                    response(null);
                }
                else
                {
                    successCallback(code);
                }
            }, response);
    }
}
