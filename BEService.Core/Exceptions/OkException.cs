using System;

namespace BEService.Core.Exceptions
{
    public class OkException: Exception
    {
        public OkException(string message) : base(message)
        {

        }
    }
}
