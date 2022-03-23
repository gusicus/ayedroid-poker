namespace Ayedroid.Poker.App.Models
{
    public class UniqueEntity
    {
        public UniqueEntity(string name) : this(name, Guid.NewGuid().ToString())
        {
        }

        public UniqueEntity(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }
}
