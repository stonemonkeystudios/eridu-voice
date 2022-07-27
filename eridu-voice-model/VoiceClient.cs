using MessagePack;

namespace Eridu.Voice.Model
{
    [MessagePackObject]
    public class VoiceClient
    {
        [Key(0)]
        public int UserId { get; set; }
        [Key(1)]
        public string Username { get; set; }
    }
}
