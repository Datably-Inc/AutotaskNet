using AutotaskNet.Utilities;
using Xunit;

namespace AutotaskNetTests.Utilities;

public class UriExtensionsTests
{
    [Theory]
    [InlineData("https://datably.io", "pages/contact", "https://datably.io/pages/contact")]
    [InlineData("https://datably.io/", "/pages/contact", "https://datably.io/pages/contact")]
    [InlineData("https://datably.io/pages", "contact", "https://datably.io/pages/contact")]
    public void AppendRelativePath(string url, string relativePath, string expectedUrl)
    {
        var uri = new Uri(url)
            .AppendRelativePath(relativePath);

        Assert.Equal(expectedUrl, uri.ToString());
    }

    [Fact]
    public void UpsertQueryParam_AddsParam()
    {
        var uri = new Uri("https://google.com/search")
            .UpsertQueryParam("q", "Datably, Inc.");
        
        Assert.Equal("https://google.com/search?q=Datably%2c+Inc.", uri.ToString());
    }

    [Fact]
    public void UpsertQueryParams_UpdatesParamIfExists()
    {
        var uri = new Uri("https://google.com/search")
            .UpsertQueryParam("q", "Datably, Inc.");

        uri = uri.UpsertQueryParam("q", "Chattanooga");
        
        Assert.Equal("https://google.com/search?q=Chattanooga", uri.ToString());
    }
}