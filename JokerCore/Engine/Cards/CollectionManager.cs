using System.IO;

namespace JokerCore
{
    public class CollectionManager
    {
        // ATTRIBUTES

        private static CollectionManager _instance;

        public static CollectionManager Instance => _instance ??= new CollectionManager();

        public CollectionJson CollectionJson { get; set; }

        // CONSTRUCTORS

        private CollectionManager()
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText(Configuration.PathJsonCardData);
//            CollectionJson data = JsonSerializer.Deserialize<CollectionJson>(json);
            CollectionJson data = new CollectionJson();
            CollectionJson = data;
        }

        // METHODS
    }
}