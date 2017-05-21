using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Payment : IModel<string>
    {
        public Payment()
        {
            this.ResourcePath = "payments";
        }

        public string Id { get; set; }

        public string ResourcePath { get; set; }

        [JsonProperty("session_id")]
        public string SessionId{ get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

        public Item Item { get; set; }

        public Session Session { get; set; }
    }
}
