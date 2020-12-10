using System;

namespace CloudTrader.Api.Exceptions
{
    public class ApiConnectionError : Exception
    {
        public ApiConnectionError(string api)
            : base($"There was an error connecting to the {api} api") { }
    }
}