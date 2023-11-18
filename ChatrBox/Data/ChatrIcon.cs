#nullable disable
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ChatrBox.Data
{
    public class ChatrIcon
    {
        public int Id { get; set; }
        public string ChatrId { get; set; }
        public Chatr Chatr {  get; set; }
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
