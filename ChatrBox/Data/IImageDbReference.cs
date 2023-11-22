using System.Security.Cryptography;

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
        string ImageUrl { get; set; }
        bool IsUnaltered { get; }
    }

    public static class IntegretyVerifyer
    {
        public static bool Veryify(string fileUrl, string fileHash)
        {
            using (var sha = SHA256.Create())
            {
                using (var fs = new FileStream(fileUrl, FileMode.Open))
                {
                    var hash = sha.ComputeHash(fs);
                    return Convert.ToHexString(hash) == fileHash;
                }
            }
        }
    }
}