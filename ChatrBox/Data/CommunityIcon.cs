using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ChatrBox.Data
{
    public class CommunityIcon
    {
        public int Id { get; set; }
        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }
        public string Url { get; set; }
        public string Hash { get; set; }


        [NotMapped]
        public bool IsSafe
        {
            get
            {
                using (var sha = SHA256.Create())
                {
                    using (var fs = new FileStream(Url, FileMode.Open))
                    {
                        var fileHash = sha.ComputeHash(fs);
                        return Hash == Convert.ToHexString(fileHash);
                    }
                }
            }
        }
    }
}

