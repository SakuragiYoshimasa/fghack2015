using UnityEngine;
using System.Collections;

public class Ctrl : MonoBehaviour
{
    ConnectParse parse;

    void Start()
    {
        parse = new ConnectParse(this, gameObject.AddComponent<MTHandler>());

        System.Action configAction = () =>
        {
            parse.Request(new CondConfigSync(), response =>
                {
                    CondConfigSync.R config = response as CondConfigSync.R;
                    Debug.Log("できたよ GameTime : " + config.GameTime);
                }, error =>
                {
                });
        };

        if (parse.IsLogIn)
        {
            Debug.Log("ログインしてるよ");
            configAction();
        }
        else
        {
            parse.SignUp(System.Guid.NewGuid().ToString(), "asdf1234", "Hack", () =>
                {
                    configAction();
                }, () =>
                {
                    Debug.Log("できなかったよ");
                });
        }
    }

    public void OnRegisterClick()
    {
        parse.Request(new CondRegister(1234, 0, 123.456, 123.456), response =>
            {
                Debug.Log("Register");
            }, error =>
            {
                Debug.LogError("Register");
            });

    }

    public void OnStartClick()
    {
        parse.Request(new CondStart(), response =>
            {
                Debug.Log("Start");
            }, error =>
            {
                Debug.LogError("Start");
            });
    }

    public void OnCheckClick()
    {
        string hitEnemyID = "asdf";
        bool isSuccessGesture = true;

        parse.Request(new CondCheck(hitEnemyID, isSuccessGesture), response =>
            {
                CondCheck.R res = response as CondCheck.R;
                foreach (CondCheck.EnemyInfo info in res.EnemyList)
                {
                    Debug.Log("enemy id : " + info.id);
                }

                foreach (CondCheck.TeamInfo info in res.TeamList)
                {
                    Debug.Log("team id : " + info.id);
                }

                Debug.Log("Check");
            }, error =>
            {
                Debug.LogError("Check");
            });
    }
}
