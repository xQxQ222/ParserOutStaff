using System.ComponentModel.DataAnnotations;

namespace ParserOutStaff.Models
{
    public class BaseModel
    {
        public App App { get; set; } = new App();
    }
    public class App
    {
        [Required(ErrorMessage = "The field AppId is required.")]
        public string AppId { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field AppSecret is required.")]
        public string AppSecret { get; set; } = string.Empty;
    }
}
