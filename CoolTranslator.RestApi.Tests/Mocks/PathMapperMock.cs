using CoolTranslator.Core.Abstract;
using System.IO;

namespace CoolTranslator.RestApi.Tests.Mocks
{
    class PathMapperMock : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Plugins");
        }
    }
}
