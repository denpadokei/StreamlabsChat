using ChatCore.Interfaces;
using ChatCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamlabsChat.Models
{
    public class StreamlabsChannel : IChatChannel
    {
        public StreamlabsChannel(JSONNode json)
        {
            var tags = json["tags"].AsObject;
            this.Name = json["from"];
            this.Id = tags["room-id"];
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public JSONObject ToJson()
        {
            var json = new JSONObject();
            json[nameof(this.Id)] = this.Id;
            json[nameof(this.Name)] = this.Name;
            return json;
        }
    }
}
