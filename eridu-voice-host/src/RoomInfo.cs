using System.Collections.Concurrent;
using System.Linq;
using Eridu.Voice.Model;

namespace Eridu.Voice.Server
{
    class RoomInfo
    {
        const int defaultMaxPlayers = 10;

        int MaxPlayers { get; }
        string Name { get; }

        private ConcurrentDictionary<string, VoiceClient> _playerList;

        public RoomInfo(string name)
        {
            this.Name = name;
            this.MaxPlayers = defaultMaxPlayers;
            this._playerList = new ConcurrentDictionary<string, VoiceClient>();
        }

        public RoomInfo(string name, int maxPlayers)
        {
            this.Name = name;
            this.MaxPlayers = maxPlayers;
            this._playerList = new ConcurrentDictionary<string, VoiceClient>();
        }

        public VoiceClient AddPlayer(string userId, string playerName)
        {
            VoiceClient player = new VoiceClient() { UserId = userId, Username = playerName};
            if (_playerList.Count < MaxPlayers)
            {
                _playerList.TryAdd(userId, player);
            }

            return player;
        }

        public VoiceClient GetPlayer(string userId)
        {
            VoiceClient player = null;
            _playerList.TryGetValue(userId, out player);
            return player;
        }

        public VoiceClient[] GetPlayers()
        {
            return _playerList.Values.ToArray();
        }

        public bool RemovePlayer(string userId)
        {
            VoiceClient player;
            return _playerList.TryRemove(userId, out player);
        }

        private int FindNewActorNumber()
        {
            var list = _playerList.Values.ToDictionary(player => player.ActorNumber);

            for (int id = 0; id < MaxPlayers; id++)
            {
                if (!list.ContainsKey(id))
                {
                    return id;
                }
            }

            return -1;
        }
    }
}
