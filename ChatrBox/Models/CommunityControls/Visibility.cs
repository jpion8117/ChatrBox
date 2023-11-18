namespace ChatrBox.Models.CommunityControls
{
    public enum Visibility
    {
        /// <summary>
        /// Closed community: The community manager, must manually invite users 
        /// to the community. The community will not be visible in searches and 
        /// the links to the community will only work for registered users of 
        /// the community.
        /// </summary>
        Closed,

        /// <summary>
        /// Private community: Users may follow links to the community, but 
        /// will only be allowed to view content within the community if they 
        /// are a member. If they are not members, they will be able to request 
        /// access from the community manager. This community will not show up 
        /// in search results.
        /// </summary>
        Private,

        /// <summary>
        /// Protected community: Users may visit the community and read messages 
        /// posted within the community, but may not join and post to the community 
        /// without the approval of the community manger. This type of community 
        /// will not show up in search results.
        /// </summary>
        Protected,

        /// <summary>
        /// ProtectedPlus community: Same as protected, but it does show up in 
        /// community searches.
        /// </summary>
        ProtectedPlus,

        /// <summary>
        /// Public community: These communities are open for anyone with the 
        /// community link to join. They will not show up in community searches 
        /// though.
        /// </summary>
        Public,

        /// <summary>
        /// Open community: This is the most open type of community. 
        /// Anyone can join or search for this community in the community locater.
        /// </summary>
        Open
    }
}
