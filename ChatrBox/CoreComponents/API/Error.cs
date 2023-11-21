namespace ChatrBox.CoreComponents.API
{
    public class Error
    {
        public static Error MakeReport(ErrorCodes error, string description)
        {
            return new Error
            {
                Code = error,
                Description = description
            };
        }

        /// <summary>
        /// Error code associated with this error.
        /// </summary>
        public ErrorCodes Code { get; private set; } = ErrorCodes.GeneralFailure;

        /// <summary>
        /// Description of why this error happened
        /// </summary>
        public string Description { get; private set; } = "";
    }
}
