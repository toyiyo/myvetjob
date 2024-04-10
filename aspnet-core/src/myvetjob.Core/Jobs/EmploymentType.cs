using System.Text.Json.Serialization;

namespace myvetjob.Jobs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmploymentType
        {
            FullTime,
            PartTime,
            Contract,
            Internship,
            Volunteer,
            Temporary
        }
}