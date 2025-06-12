namespace XMLtoPDF.Domain.Entities
{
    public class XmlFile
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Full path to the stored XML file.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Name of the uploaded XML file.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Date/time when the XML was uploaded (UTC).
        /// </summary>
        public DateTime UploadTimestamp { get; set; } = DateTime.UtcNow;
    }
}
