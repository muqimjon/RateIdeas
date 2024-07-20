namespace RateIdeas.Application.Commons.Helpers;

public class TimeHelper
{
    public static DateTimeOffset GetDateTime()
        => DateTimeOffset.UtcNow.AddHours(TimeConstants.UTC);

    public static DateTimeOffset ToLocalize(DateTimeOffset dateTime)
        => dateTime.ToUniversalTime().AddHours(TimeConstants.UTC);
}
