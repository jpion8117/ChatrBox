using Microsoft.AspNetCore.Identity;

namespace ChatrBox.Data
{
    public class Chatr : IdentityUser
    {
        public bool ActiveUser { get; set; }
        public int? IconId { get; set; }
        public ChatrIcon Icon { get; set; }
    }
}
