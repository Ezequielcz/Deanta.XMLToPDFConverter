using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLtoPDF.Application.Interfaces;

namespace XMLtoPDF.Infrastructure.Services
{
    public class XmlFileLoader : IXmlFileLoader
    {
        public async Task<string> LoadAsync(string path)
        {
            return await File.ReadAllTextAsync(path);
        }
    }
}
