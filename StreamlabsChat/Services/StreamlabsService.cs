using ChatCore.Interfaces;
using ChatCore.Utilities;
using StreamlabsChat.Interfaces;
using StreamlabsChat.Models;
using StreamlabsChat.WebSockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamlabsChat.Services
{
    public class StreamlabsService : IChatService
    {
        public string DisplayName => "Streamlabs";

        public const string TWITCH_ACCOUT = "twitch_account";
        public const string YOUTUBE_ACCOUT = "youtube_account";

        public event Action<IChatService> OnLogin;
        public event Action<IChatService, IChatMessage> OnTextMessageReceived;
        public event Action<IChatService, IChatChannel> OnJoinChannel;
        public event Action<IChatService, IChatChannel> OnRoomStateUpdated;
        public event Action<IChatService, IChatChannel> OnLeaveChannel;
        public event Action<IChatService, string> OnChatCleared;
        public event Action<IChatService, string> OnMessageCleared;
        public event Action<IChatService, IChatChannel, Dictionary<string, IChatResourceData>> OnChannelResourceDataCached;

        public void SendTextMessage(string message, IChatChannel channel)
        {

        }

        private StreamlabsServer _streamlabsable;

        public StreamlabsService(StreamlabsServer streamlabsable)
        {
            this._streamlabsable = streamlabsable;
            this._streamlabsable.OnReciveMessage += this.OnStreamlabsable_OnReciveMessage;
        }

        private void OnStreamlabsable_OnReciveMessage(string obj)
        {
            var json = JSON.Parse(obj);
            var message = new StreamlabsMessage(json);
#if !DEBUG
            if (message.CType == StreamlabsMessage.ChannnelType.Twitch) {
                return;
            }
#endif

            this.OnTextMessageReceived?.Invoke(this, message);
        }
    }
}
