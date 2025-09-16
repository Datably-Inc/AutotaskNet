using AutotaskNet.Domain.Requests;
using AutotaskNet.Implementation;

namespace AutotaskNet.Api;

public class AutotaskNetTestHelper
{
    /// <summary>
    /// Creates an instance of IAutotaskNet for use in integration testing.
    /// </summary>
    public static IAutotaskNet CreateAutotaskNet(AutotaskCredentials credentials)
    {
        return new Implementation.AutotaskNet(new IAutotaskProxy.Imp(new IProxy.Imp(new HttpClient()), credentials));
    }
}