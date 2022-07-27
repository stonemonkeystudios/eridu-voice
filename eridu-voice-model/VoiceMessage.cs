using MessagePack;

namespace Eridu.Voice.Model
{
    [MessagePackObject]
    public class VoiceMessage
    {
        [Key(0)]
        public int SenderPlayerId { get; set; }
        [Key(1)]
        public string SenderPlayerName { get; set; }
        [Key(2)]
        public byte[] EncodedData { get; set; }
        [Key(3)]
        public int DataLength { get; set; }
    }
}
