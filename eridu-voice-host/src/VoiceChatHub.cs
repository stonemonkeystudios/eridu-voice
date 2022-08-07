using System;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Server.Hubs;
using Eridu.Voice.Model;

namespace Eridu.Voice.Server
{
    [GroupConfiguration(typeof(ConcurrentDictionaryGroupRepositoryFactory))]
    class VoiceChatHub : StreamingHubBase<IVoiceChatHub, IVoiceChatHubReceiver>, IVoiceChatHub
    {
        IGroup room;
        VoiceClient self;
        IInMemoryStorage<VoiceClient> _clientStorage;

        public async Task SendMessageExceptSelfAsync(VoiceMessage message) {
            Broadcast(room).OnReceivedMessage(message);
            //BroadcastExceptSelf(room).OnReceivedMessage(message);
            await Task.CompletedTask;
        }

        public async Task<VoiceClient[]> JoinAsync(string roomName, string username, int userId)
        {
            self = new VoiceClient() { Username = username, UserId = userId };

            (room, _clientStorage) = await Group.AddAsync(roomName, self);
            Broadcast(room).OnJoin(self);
            return _clientStorage.AllValues.ToArray();
        }

        public async Task LeaveAsync()
        {
            if (room != null) {
                await (room.RemoveAsync(this.Context));
                Broadcast(room).OnLeave(self);
            }
        }

        protected override ValueTask OnDisconnected() {
            return CompletedTask;
        }

        protected override ValueTask OnConnecting() {
            return CompletedTask;
        }
    }
}
