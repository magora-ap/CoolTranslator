using System.ComponentModel.DataAnnotations;

namespace CoolTranslator.RestApi.Models
{
    public class TranslationRequest
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string Language { get; set; }
    }
}