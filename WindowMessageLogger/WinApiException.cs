using System;

namespace WindowMessageLogger
{
    public class WinApiException : Exception
    {
        public WinApiException(string message)
            : base(message)
        {
        }
    }
}
