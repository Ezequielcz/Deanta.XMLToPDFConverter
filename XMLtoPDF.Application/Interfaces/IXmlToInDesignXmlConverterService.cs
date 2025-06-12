namespace XMLtoPDF.Application.Interfaces
{
    public interface IXmlToInDesignXmlConverterService
    {
        /// <summary>
        /// Converts a validated XML into an InDesign-compatible XML format.
        /// </summary>
        /// <param name="sourceXmlFilePath">Path of the source validated XML.</param>
        /// <param name="outputXmlFilePath">Target path for the InDesign XML output.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ConvertAsync(string sourceXmlFilePath, string outputXmlFilePath);
    }
}
