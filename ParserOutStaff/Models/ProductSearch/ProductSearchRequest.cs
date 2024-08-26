using System.ComponentModel.DataAnnotations;

namespace ParserOutStaff.Models.ProductSearch
{
    public class ProductSearchRequest:BaseModel
    {
        [MinLength(1, ErrorMessage = "The MinLength of field SearchPhraseList must be equal one")]
        [Required(ErrorMessage = "The field SearchPhraseList is required.")]
        //[ListEmptyElement(ErrorMessage = "The phrase list contains an empty value")]
        public List<string> SearchPhraseList { get; set; } = new List<string>();
        public int waitTimeout { get; set; } = 0;
        public int maxProductsCount { get; set; } = 0;
    }
}
