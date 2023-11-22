namespace ChatrBox.Data
{
    enum ImageType
    {
        ChatrIcon,
        CommnunityIcon
    }
    public interface IImageDbReference
    {
        string ImageHash { get; set; }
        int Id { get; set; }
        string ImageUrl { get; set; }
    }
}