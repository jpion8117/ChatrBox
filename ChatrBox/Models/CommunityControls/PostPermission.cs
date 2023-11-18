namespace ChatrBox.Models.CommunityControls
{
    public enum PostPermission
    {
        /// <summary>
        /// Any server member may post messages in this topic.
        /// </summary>
        Open,

        /// <summary>
        /// Only the community manager or moderators may post 
        /// in this topic
        /// </summary>
        Closed
    }
}
