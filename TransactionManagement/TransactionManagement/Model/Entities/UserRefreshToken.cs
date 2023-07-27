using System.Text.Json.Serialization;

namespace TransactionManagement.Model.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        // Navigation property
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
