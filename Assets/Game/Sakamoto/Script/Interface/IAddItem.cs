
public interface IAddItem
{
    /// <summary>
    /// アイテムを受け取るメソッドアイテムを渡せるかBool型で返す
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <param name="item3"></param>
    public bool ReceiveItems(ItemData item);
}
