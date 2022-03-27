namespace Ayedroid.Poker.App.Models
{
    public class Topic : UniqueEntity
    {
        public Topic(string name) : base(name)
        {

        }

        public string Description { get; init; } = string.Empty;
        // UserId, Size
        public Dictionary<string, Size> Votes = new Dictionary<string, Size>();
    }
}
