namespace ChatrBox.CoreComponents.API
{
    /// <summary>
    /// Defines general error/success conditions to be paired with ChatrBox.System.API.Error. 
    /// Error conditions can be further clairifed through the use of the Error class's 
    /// description property.
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// sent when an API operation succeeds correctly
        /// </summary>
        Success,

        /// <summary>
        /// Sent when opperation is completed successfully, but reuires no action
        /// </summary>
        SuccessNoAction,

        /// <summary>
        /// Marks the beginning of failure codes. All codes prior to this indicate success
        /// </summary>
        GeneralFailure = 100,

        /// <summary>
        /// The content is marked as restricted for the given user attempting to access it
        /// </summary>
        ContentRestricted,

        /// <summary>
        /// Failed to locate content.
        /// </summary>
        FailedToLocate,

        /// <summary>
        /// Failed to upload content.
        /// </summary>
        UploadFailed
    }
}
