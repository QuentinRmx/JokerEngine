namespace JokerCore
{
    /// <summary>
    /// Used as a flag to quickly tell what kind of message is sent.
    /// The 
    /// </summary>
    public enum EMessageType
    {
        /// <summary>
        /// Used by the server to tell that the previously sent request has NOT been correctly treated and therefore
        /// should be cancelled in the client side. The NetworkMessage contains an error message that explains why the
        /// request made by the client couldn't be treated.
        /// </summary>
        Failure = 0,

        /// <summary>
        /// Used by the server to tell that the previously sent request has been correctly treated. The NetworkMessage
        /// flagged with Success usually contains the changes made by the previous request.
        /// </summary>
        Success = 1
    }
}