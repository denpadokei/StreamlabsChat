using ChatCore.Interfaces;
using ChatCore.Utilities;
using StreamlabsChat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamlabsChat.Models
{
    public class StreamlabsMessage : IChatMessage
    {
        public StreamlabsMessage()
        {

        }

        public StreamlabsMessage(JSONNode json)
        {
            var tags = json["tags"].AsObject;

            this.Id = tags["id"];
            this.IsSystemMessage = false;
            this.IsActionMessage = false;
            this.IsHighlighted = false;
            this.IsPing = false;
            this.Message = json["body"].Value;
            this.Sender = new StreamlabsUser(json);
            this.Channel = new StreamlabsChannel(json);
            this.Emotes = Array.Empty<IChatEmote>();
            if (json["platform"].Value == StreamlabsService.TWITCH_ACCOUT) {
                this.CType = ChannnelType.Twitch;
            }
            else if (json["platform"].Value == StreamlabsService.YOUTUBE_ACCOUT) {
                this.CType = ChannnelType.Youtube;
            }
            else {
                this.CType = ChannnelType.Other;
            }
        }

        public string Id { get; set; }

        public bool IsSystemMessage { get; set; }

        public bool IsActionMessage { get; set; }

        public bool IsHighlighted { get; set; }

        public bool IsPing { get; set; }

        public string Message { get; set; }

        public IChatUser Sender { get; set; }

        public IChatChannel Channel { get; set; }

        public IChatEmote[] Emotes { get; set; }

        public ChannnelType CType { get; set; }

        public ReadOnlyDictionary<string, string> Metadata { get; set; } = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        public JSONObject ToJson()
        {
            var json = new JSONObject();
            json[nameof(this.Id)] = this.Id;
            json[nameof(this.IsSystemMessage)] = new JSONBool(this.IsSystemMessage);
            json[nameof(this.IsActionMessage)] = new JSONBool(this.IsActionMessage);
            json[nameof(this.IsHighlighted)] = new JSONBool(this.IsHighlighted);
            json[nameof(this.IsPing)] = new JSONBool(this.IsPing);
            json[nameof(this.Message)] = this.Message;
            json[nameof(this.Sender)] = this.Sender.ToJson();
            json[nameof(this.Channel)] = this.Channel.ToJson();
            var emotes = new JSONArray();
            foreach (var emote in this.Emotes) {
                emotes.Add(emote.ToJson());
            }
            json[nameof(this.Emotes)] = emotes;
            return json;
        }

        public enum ChannnelType
        {
            Twitch,
            Youtube,
            Other
        }
    }
}
