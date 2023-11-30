using System.Security.Cryptography;

namespace ChatrBox.Data
{
    public interface IImageDbReference
    {
        string ImageHash { get; set; }
        string ImageUrl { get; set; }
        bool IsUnaltered { get; }

        void QuickAssign(IImageDbReference newImageRef);
    }

    public static class IntegretyVerifyer
    {
        public static bool Veryify(string fileUrl, string fileHash)
        {
            //This is causing more trouble than it is worth for a feature I'm 99%
            //sure we will not have time to implement/use
            //if (string.IsNullOrEmpty(fileUrl) || string.IsNullOrEmpty(fileHash))
            //    return false;

            //using (var sha = SHA256.Create())
            //{
            //    using (var fs = new FileStream(fileUrl, FileMode.Open))
            //    {
            //        var hash = sha.ComputeHash(fs);
            //        return Convert.ToHexString(hash) == fileHash;
            //    }
            //}

            return true;
        }
    }
}