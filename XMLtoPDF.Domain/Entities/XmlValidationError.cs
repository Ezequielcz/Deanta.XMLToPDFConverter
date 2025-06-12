namespace XMLtoPDF.Domain.Entities
{
    public class XmlValidationError
    {
        public int LineNumber { get; set; }
        public int LinePosition { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
