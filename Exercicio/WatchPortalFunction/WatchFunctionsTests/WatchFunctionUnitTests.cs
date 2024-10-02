using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
namespace WatchFunctionsTests;

public class WatchFunctionUnitTests
 {
    [Fact]
    public void TestWatchFunctionSuccess()
    {
        var queryStringValue = "abc";
        var request = new DefaultHttpRequest(new DefaultHttpContext())
        {
            Query = new QueryCollection
            (
              new System.Collections.Generic.Dictionary<string, StringValues>()
              {
                    { "model", queryStringValue }
              }
             )
        };

        var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
        var response = WatchPortalFunction.Function1.Run(request, logger);
        response.Wait();

        Assert.IsAssignableFrom<OkObjectResult>(response.Result);

        var result = (OkObjectResult)response.Result;
        dynamic watchinfo = new { Manufacturer = "abc", CaseType = "Solid", Bezel = "Titanium", Dial = "Roman", CaseFinish = "Silver", Jewels = 15 };
        string watchInfo = $"Watch Details: {watchinfo.Manufacturer}, {watchinfo.CaseType}, {watchinfo.Bezel}, {watchinfo.Dial}, {watchinfo.CaseFinish}, {watchinfo.Jewels}";
        Assert.Equal(watchInfo, result.Value);
    }
}
