namespace Project.Item
{
    public interface IItemable
    {
        int ID { get; }
        ItemType ItemType { get; }
    }
}