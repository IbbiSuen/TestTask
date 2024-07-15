using Newtonsoft.Json;

namespace APITests.APICore.Models
{
    public class Activities
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("dueDate")]
        public string DueDate { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }
    }
}
