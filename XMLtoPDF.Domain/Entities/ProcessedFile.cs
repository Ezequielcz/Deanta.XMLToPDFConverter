using LiteDB;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XMLtoPDF.Domain.Entities
{
    public class ProcessedFile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string PdfFileName { get; set; }
        public string XmlFileName { get; set; }
        public DateTime GenerationDate { get; set; }
        public string Status { get; set; } // "Success" | "Error"
        public long FileSizeInBytes { get; set; }
        public string? PdfFilePath { get; set; }

        public List<XmlValidationError>? Errors { get; set; } = new();
    }
}
