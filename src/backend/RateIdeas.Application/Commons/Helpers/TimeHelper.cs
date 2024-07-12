namespace RateIdeas.Application.Commons.Helpers;

public class TimeHelper
{
    public static DateTimeOffset GetDateTime()
        => DateTimeOffset.UtcNow.AddHours(TimeConstants.UTC).ToUniversalTime();

    public static DateTimeOffset ToLocalize(DateTimeOffset dateTime)
        => dateTime.AddHours(TimeConstants.UTC).ToUniversalTime();
}
