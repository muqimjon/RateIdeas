using System.Text.Json.Serialization;

namespace RateIdeas.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Roles
{
    User,
    Admin,
    SuperAdmin,
}