using ChatCore.Interfaces;
using ChatCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamlabsChat.Models
{
    public class StreamlabsUser : IChatUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Color { get; set; }

        public bool IsBroadcaster { get; set; }

        public bool IsModerator { get; set; }

        public IChatBadge[] Badges { get; set; }

        public StreamlabsUser(JSONNode json)
        {
            var tags = json["tags"].AsObject;
            this.Id = tags["user-id"].Value;
            this.UserName = json["from"].Value;
            this.DisplayName = tags["display-name"].Value;
            this.Color = string.IsNullOrEmpty(tags["color"].Value) ? "#FFFFFF" : tags["color"].Value;
            this.IsBroadcaster = json["owner"].AsBool;
            this.IsModerator = tags["mod"].AsInt != 0;
            var badge = tags["badges"].Value.Split(',');
            var badgelist = new List<StreamlabsChatBadge>();
            foreach (var badgename in badge) {
                var sbadge = new StreamlabsChatBadge(json, badgename);
                if (string.IsNullOrEmpty(sbadge.Uri)) {
                    continue;
                }
                badgelist.Add(new StreamlabsChatBadge(json, badgename));
            }
            this.Badges = badgelist.ToArray();
        }

        public JSONObject ToJson()
        {
            var json = new JSONObject();
            json[nameof(this.Id)] = this.Id;
            json[nameof(this.UserName)] = this.UserName;
            json[nameof(this.DisplayName)] = this.DisplayName;
            json[nameof(this.Color)] = this.Color;
            json[nameof(this.IsBroadcaster)] = new JSONBool(this.IsBroadcaster);
            json[nameof(this.IsModerator)] = new JSONBool(this.IsModerator);

            var badges = new JSONArray();
            foreach (var badge in this.Badges) {
                badges.Add(badge.ToJson());
            }
            json[nameof(this.Badges)] = badges;
            return json;
        }
    }
}
