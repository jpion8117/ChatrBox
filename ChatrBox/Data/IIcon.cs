namespace ChatrBox.Data
{
    public interface IIcon
    {
        string Hash { get; set; }
        int Id { get; set; }
        string Url { get; set; }
    }
}