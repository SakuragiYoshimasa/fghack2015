public abstract class ConnectBase
{
    public class Item
    {
        public CondBase cond;
        public System.Action<CondBase.Response> finishCallback;
        public System.Action<CondErrorR> errorCallback;

        public Item(CondBase cond, System.Action<CondBase.Response> finishCallback, System.Action<CondErrorR> errorCallback)
        {
            this.cond = cond;
            this.finishCallback = finishCallback;
            this.errorCallback = errorCallback;
        }
    }

    MTHandler mt;

    /// <summary>
    /// ログインしているかどうか
    /// </summary>
    public abstract bool IsLogIn { get; }

    /// <summary>
    /// ログインID
    /// </summary>
    public abstract string UserName { get; }

    public ConnectBase(MTHandler mt)
    {
        this.mt = mt;
    }

    /// <summary>
    /// メインスレッドに合流
    /// </summary>
    public void JoinMT(System.Action action)
    {
        mt.Enqueue(action);
    }

    /// <summary>
    /// サインアップする
    /// </summary>
    public abstract void SignUp(string username, string password, string displayName, System.Action finishCallback, System.Action errorCallback);

    /// <summary>
    /// ログインする
    /// </summary>
    public abstract void LogIn(string username, string password, System.Action finishCallback, System.Action errorCallback);

    /// <summary>
    /// ログアウトする
    /// </summary>
    public abstract void LogOut();

    /// <summary>
    /// 情報を要求する
    /// </summary>
    public abstract void Request(CondBase cond, System.Action<CondBase.Response> finishCallback, System.Action<CondErrorR> errorCallback);
}
