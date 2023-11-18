namespace ChatrBox.Data
{
    public class CommunityUser
    {
        public int Id { get; set; }
        public virtual string ChatrId { get; set; }
        public virtual Chatr Chatr { get; set; }

        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }
    }
}
