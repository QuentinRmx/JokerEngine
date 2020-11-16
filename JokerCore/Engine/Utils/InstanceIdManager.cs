namespace JokerCore
{
    public static class InstanceIdManager
    {
        private static int _lastInstanceId = 0;

        /// <summary>
        /// Auto-increment the hidden field and returns the new value which is the next instance id to use for a Card.
        /// </summary>
        public static int NextInstanceId
        {
            get
            {
                _lastInstanceId++;
                return _lastInstanceId;
            }
            private set => _lastInstanceId = value;
        }

        /// <summary>
        /// Reset the instance id to 0.
        /// </summary>
        public static void Reset()
        {
            _lastInstanceId = 0;
        }
    }
}