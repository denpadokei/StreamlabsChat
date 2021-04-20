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
    public class StreamlabsChatBadge : IChatBadge
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public JSONObject ToJson()
        {
            var json = new JSONObject();
            json[nameof(this.Id)] = this.Id;
            json[nameof(this.Name)] = this.Name;
            json[nameof(this.Uri)] = this.Uri;
            
            return json;
        }

        public StreamlabsChatBadge(JSONNode json, string badgename)
        {
            this.Id = badgename;
            var accountType = json["platform"].Value;
            if (accountType == StreamlabsService.TWITCH_ACCOUT) {
                this.Name = badgename.Split('/')[0];
                if (TwitchBadges.TryGetValue(this.Name, out var uri)) {
                    this.Uri = uri;
                }
            }
            else if (accountType == StreamlabsService.YOUTUBE_ACCOUT) {
                this.Name = badgename.Split('/')[0];
                if (YouTubeBadges.TryGetValue(this.Name, out var uri)) {
                    this.Uri = uri;
                }
            }
            else {
                this.Name = badgename.Split('/')[0];
                this.Uri = "";
            }
        }

        static StreamlabsChatBadge()
        {
            var badges = new Dictionary<string, string>();
            badges.Add("admin", "https://static-cdn.jtvnw.net/badges/v1/9ef7e029-4cdf-4d4d-a0d5-e2b3fb2583fe/1");
            badges.Add("broadcaster", "https://static-cdn.jtvnw.net/badges/v1/5527c58c-fb7d-422d-b71b-f309dcb85cc1/1");
            badges.Add("global_mod", "https://static-cdn.jtvnw.net/badges/v1/9384c43e-4ce7-4e94-b2a1-b93656896eba/1");
            badges.Add("mod", "https://static-cdn.jtvnw.net/badges/v1/3267646d-33f0-4b17-b3df-f923a41db1d0/1");
            badges.Add("staff", "https://static-cdn.jtvnw.net/badges/v1/d97c37bd-a6f5-4c38-8f57-4e4bef88af34/1");

            TwitchBadges = new ReadOnlyDictionary<string, string>(badges);

            badges = new Dictionary<string, string>();
            badges.Add("admin", "https://storage.googleapis.com/support-kms-prod/7D205F483A53FDB0CB0A8FD172142A683647");
            badges.Add("broadcaster", "https://storage.googleapis.com/support-kms-prod/1AAF81CF4CB2A93ECB79F4CAE1510D048AAB");
            badges.Add("global_mod", "https://storage.googleapis.com/support-kms-prod/7D205F483A53FDB0CB0A8FD172142A683647");
            badges.Add("mod", "https://storage.googleapis.com/support-kms-prod/7D205F483A53FDB0CB0A8FD172142A683647");
            badges.Add("staff", "https://storage.googleapis.com/support-kms-prod/7D205F483A53FDB0CB0A8FD172142A683647");
            badges.Add("turbo", "https://lh3.googleusercontent.com/rEIdMDIJ7jNgLYpH7pjKtxpFDq_kh_XvxL5GSPLmPa8ls5gwi4clkBGIhOVg=h15");
            badges.Add("premium", "https://lh3.googleusercontent.com/rEIdMDIJ7jNgLYpH7pjKtxpFDq_kh_XvxL5GSPLmPa8ls5gwi4clkBGIhOVg=h15");
            badges.Add("subscriber", "https://lh3.googleusercontent.com/rEIdMDIJ7jNgLYpH7pjKtxpFDq_kh_XvxL5GSPLmPa8ls5gwi4clkBGIhOVg=h15");

            YouTubeBadges = new ReadOnlyDictionary<string, string>(badges);
        }

        private static ReadOnlyDictionary<string, string> TwitchBadges;
        private static ReadOnlyDictionary<string, string> YouTubeBadges;
    }
}
