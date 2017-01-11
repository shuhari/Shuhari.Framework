namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Generic submit result, with if success and error message
    /// </summary>
    public class ResultDTO
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ResultDTO()
        {
            SetResult(true);
        }

        /// <summary>
        /// if query is uccess
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Set result
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public void SetResult(bool success, string message = null)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
