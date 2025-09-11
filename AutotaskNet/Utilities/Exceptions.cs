namespace AutotaskNet.Utilities;

internal class AutotaskNetCoreException(string msg) : Exception(msg);

internal class AutotaskNetCoreValidationException(string msg) : Exception(msg);