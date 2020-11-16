namespace JokerCore
{
    public class NetworkMessage
    {
        // ATTRIBUTES

        /// <summary>
        /// Sender's IP address (either the player's client, the opponent's client or the server).
        /// </summary>
        public string SourceAddress { get; set; }

        /// <summary>
        /// Receiver's IP address (either the player's client, the opponent's client or the server).
        /// </summary>
        public string DestinationAddress { get; set; }
        
        /// <summary>
        /// Content of the message. Depending on the message type, the body can be a pure string or a JSON object to parse.
        /// Both the client and the server need to use the <see cref="JokerClient"/> class to process the body correctly.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Flag to quickly tell what kind of message it is. Server can only return 2 types: Success and Failure.
        /// </summary>
        public EMessageType MessageType { get; set; }

        // METHODS
    }
}