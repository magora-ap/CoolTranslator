using System;
using CoolTranslator.Core.Abstract;
using System.Web.Hosting;

namespace CoolTranslator.RestApi.Concrete
{
    public class PathMapper : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            return HostingEnvironment.MapPath(relativePath);
        }
    }
}