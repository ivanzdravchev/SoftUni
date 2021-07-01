using System;

namespace MyWebServer.Server.Common
{
    public static class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                // if no value, take the value to the right
                name ??= "Value";
                throw new ArgumentException($"{name} cannot be null.");
            }
        }
    }
}
