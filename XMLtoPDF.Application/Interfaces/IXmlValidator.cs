using XMLtoPDF.Domain.Entities;

namespace XMLtoPDF.Application.Interfaces
{
    public interface IXmlValidator
    {
        XmlValidationResult Validate(string xmlContent);
    }
}
