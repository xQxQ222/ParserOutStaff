using System.ComponentModel.DataAnnotations;

namespace ParserOutStaff.Models.DetailSearch
{
    public class DetailSearchParams:BaseModel
    {
        [MinLength(1, ErrorMessage = "The MinLength of field SearchPhraseList must be equal one")]
        [Required(ErrorMessage = "The field ProductLinks is required.")]
        //[ListEmptyElement(ErrorMessage = "The link list contains an empty value")]
        public List<string> ProductLinks { get; set; } = new List<string>();
    }
}
