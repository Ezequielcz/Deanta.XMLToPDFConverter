using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLtoPDF.Application.Interfaces;
using XMLtoPDF.Domain.Entities;

namespace XMLtoPDF.Infrastructure.Services
{
    public class XmlValidator : IXmlValidator
    {
        public XmlValidationResult Validate(string xmlContent)
        {
            var result = new XmlValidationResult();

            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                ValidationType = ValidationType.DTD,
                XmlResolver = new XmlUrlResolver()
            };

            settings.ValidationEventHandler += (sender, e) =>
            {
                if (e.Exception != null)
                {
                    result.Errors.Add(new XmlValidationError
                    {
                        LineNumber = e.Exception.LineNumber,
                        LinePosition = e.Exception.LinePosition,
                        Message = e.Message
                    });
                }
            };

            using var stringReader = new StringReader(xmlContent);

            using var reader = XmlReader.Create(stringReader, settings);

            try
            {
                while (reader.Read()) { }

                result.IsValid = !result.Errors.Any();
            }
            catch (XmlException ex)
            {
                result.Errors.Add(new XmlValidationError
                {
                    LineNumber = ex.LineNumber,
                    LinePosition = ex.LinePosition,
                    Message = ex.Message
                });
                result.IsValid = false;
            }

            return result;
        }
    }
}
