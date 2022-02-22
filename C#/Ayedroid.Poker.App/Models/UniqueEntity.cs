namespace Ayedroid.Poker.App.Models
{
    public class UniqueEntity
    {
        public UniqueEntity(string name)
        {
            Name = name;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; private set; }
    }
}
