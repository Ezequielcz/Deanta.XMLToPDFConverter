using System.ComponentModel.DataAnnotations;

namespace SE.GMO.Billing.Application.Options
{
    public class AppSettings
    {
        [Required]
        public required string InputFolderXML { get; init; }

        [Required]
        public required string OutputFolderPDF { get; init; }

        [Required]
        public required string ProcessingFolderXML { get; init; }
        
        [Required]
        public required string ErrorFolderXML { get; init; }
        

        [Required]
        public required string PollingInputFolderXML { get; init; }
    }
}
