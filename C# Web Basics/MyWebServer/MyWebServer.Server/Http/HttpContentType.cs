using System;

namespace MyWebServer.Server.Http
{
    public class HttpContentType
    {
        public const string PlainText = "text/plain; charset=UTF-8";
        public const string Html = "text/html; charset=UTF-8";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public static string GetByFileExtension(string fileExtension) 
        {
            return fileExtension switch
            {
                "css" => "text/css",
                "js" => "application/javascript",
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                _ => PlainText
            };
        }
    }
}
