using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ChatrBox.Data
{
    public class CommunityIcon : IImageDbReference
    {
        public int Id { get; set; }
        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }
        public string ImageUrl { get; set; }
        public string ImageHash { get; set; }


        [NotMapped]
        public bool IsSafe
        {
            get
            {
                using (var sha = SHA256.Create())
                {
                    using (var fs = new FileStream(ImageUrl, FileMode.Open))
                    {
                        var fileHash = sha.ComputeHash(fs);
                        return ImageHash == Convert.ToHexString(fileHash);
                    }
                }
            }
        }
    }
}

