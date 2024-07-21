namespace RateIdeas.Application.Commons.Helpers;

public class HttpContextHelper
{
#nullable disable
    public static IHttpContextAccessor Accessor { get; set; } // CS8597

    private static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? GetUserId => long.TryParse(HttpContext?.User?.FindFirst("UserId")?.Value,
        out long _tempUserId) ? _tempUserId : null;
    //public static string UserRole => HttpContext?.User?.FindFirst("Role")?.Value;
}