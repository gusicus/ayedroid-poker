namespace Ayedroid.Poker.App.Models.Dto
{
    public class StartSessionDto
    {
        public string SessionName { get; set; } = string.Empty;
        public IEnumerable<string> Sizes { get; set; } = Array.Empty<string>();
    }
}
