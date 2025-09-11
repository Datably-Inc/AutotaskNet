using System.Web;

namespace AutotaskNet.Utilities;

internal static class UriExtensions
{
    public static Uri AppendRelativePath(this Uri uri, object relativePath)
    {
        var absoluteUri = new Uri(uri.AbsoluteUri.TrimEnd('/') + '/');
        return new Uri(absoluteUri, relativePath.ToString());
    }

    public static Uri UpsertQueryParam(this Uri uri, string name, string value)
    {
        var queryParams = HttpUtility.ParseQueryString(uri.Query);

        queryParams.Remove(name);
        queryParams.Add(name, value);

        var ub = new UriBuilder(uri)
        {
            Query = queryParams.ToString()
        };

        return ub.Uri;
    }
}