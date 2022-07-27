using MagicOnion;
using System.Threading.Tasks;
using HQDotNet;

namespace Eridu.Voice.Model
{
    public interface IVoiceChatHub : IStreamingHub<IVoiceChatHub, IVoiceChatHubReceiver>
    {
        Task SendMessageExceptSelfAsync(VoiceMessage message);

        Task<VoiceClient[]> JoinAsync(string roomName, string playerName, int userId);
        Task LeaveAsync();
    }

    public interface IVoiceChatHubReceiver : IDispatchListener
    {
        void OnReceivedMessage(VoiceMessage message);
        void OnJoin(VoiceClient player);
        void OnLeave(VoiceClient player);
    }
}
