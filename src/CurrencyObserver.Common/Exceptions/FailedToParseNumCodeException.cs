namespace CurrencyObserver.Common.Exceptions;

public class FailedToParseNumCodeException : Exception
{
    private const string MessageTemplate = "Failed to parse NumCode to int value";

    public FailedToParseNumCodeException(string numCode) 
        : base($"{MessageTemplate} - ({numCode})") { }
}