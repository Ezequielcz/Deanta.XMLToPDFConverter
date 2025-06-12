using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoPDF.Domain.Entities
{
    public class XmlValidationResult
    {
        public bool IsValid { get; set; }
        public List<XmlValidationError> Errors { get; set; } = new();
    }
}
