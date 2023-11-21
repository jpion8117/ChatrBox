using ChatrBox.Models.CommunityControls;

namespace ChatrBox.Data
{
    public class CommunityUser
    {
        public int Id { get; set; }
        public virtual string ChatrId { get; set; }
        public virtual Chatr Chatr { get; set; }

        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        public bool CanPost { get; set; }
        public bool CanRead { get; set; }

        /// <summary>
        /// Create a community user ready to be inserted into the database with posting 
        /// controls based on community visibility.
        /// </summary>
        /// <param name="communityId">Id of the community the Chat'r is being added to.</param>
        /// <param name="chatrId">Id of the Chat'r</param>
        /// <param name="communityVisibility">Visibility setting of the community</param>
        /// <returns></returns>
        public static CommunityUser Create(int communityId,  string chatrId, Visibility communityVisibility)
        {
            var comUser = new CommunityUser()
            {
                Id = communityId,
                ChatrId = chatrId,
            };

            switch (communityVisibility)
            {
                case Visibility.Closed:
                    comUser.CanPost = false;
                    comUser.CanRead = false;
                    break;
                case Visibility.Private:
                    comUser.CanPost = false;
                    comUser.CanRead = false;
                    break;
                case Visibility.Protected:
                case Visibility.ProtectedPlus:
                    comUser.CanPost = false;
                    comUser.CanRead = true;
                    break;
                case Visibility.Public:
                    comUser.CanPost = true;
                    comUser.CanRead = true;
                    break;
                case Visibility.Open:
                default:
                    comUser.CanPost = true;
                    comUser.CanRead = true;
                    break;
            }

            return comUser;
        }
    }
}
