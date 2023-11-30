namespace ChatrBox.Data
{
    public class ImageBase : IImageDbReference
    {
        public string ImageHash { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsUnaltered => IntegretyVerifyer.Veryify(ImageUrl,ImageHash);

        public void QuickAssign(IImageDbReference newImageRef)
        {
            ImageUrl = newImageRef.ImageUrl;
            ImageHash = newImageRef.ImageHash;
        }
    }
}
