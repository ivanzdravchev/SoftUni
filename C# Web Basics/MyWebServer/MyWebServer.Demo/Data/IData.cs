using MyWebServer.Data.Models;
using System.Collections.Generic;

namespace MyWebServer.Data
{
    public interface IData
    {
        IEnumerable<Cat> Cats { get; }
    }
}
