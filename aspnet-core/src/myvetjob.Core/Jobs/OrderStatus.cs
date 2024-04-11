using System.Text.Json.Serialization;

namespace myvetjob.Jobs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
        {
            Pending,
            Paid
        }
}