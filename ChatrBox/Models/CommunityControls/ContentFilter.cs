namespace ChatrBox.Models.CommunityControls
{
    public enum ContentFilter
    {
        /// <summary>
        /// Message content is not moderated.
        /// </summary>
        NSFW,

        /// <summary>
        /// Messages containing profanity will be delivered, but will be censored with asterisks ('*').
        /// </summary>
        Censored,

        /// <summary>
        /// Messages containing profanity will not be delivered and the sender will be notified. 
        /// </summary>
        Blocked,

        /// <summary>
        /// Messages containing profanity will not be delivered and the sender will be banned from 
        /// posting in the server by the gods whom smite them. (Prob won't get implemented, lol) 
        /// </summary>
        Banhammer
    }
}
