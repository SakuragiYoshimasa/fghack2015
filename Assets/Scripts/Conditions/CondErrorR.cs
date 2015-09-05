/// <summary>
/// エラー時のコンディションレスポンス
/// </summary>
public class CondErrorR : CondBase.Response
{
    public ConnectBase.Item Item { get; private set; }

    public CondErrorR(ConnectBase.Item item)
        : base(item.cond)
    {
        Item = item;
    }
}
