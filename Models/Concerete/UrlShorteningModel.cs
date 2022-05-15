using System.ComponentModel.DataAnnotations;

namespace Models.Concerete;

public class UrlShorteningModel
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    private string shorteningTag;

    [Required]
    public string InitialUrl { get; set; } = "-";

    [Required]
    [MaxLength(6, ErrorMessage = "ShorteningTag cannot be longer than 6 characters.")]
    public string ShorteningTag { get; set; }    

    public string Message { get; set; } = "-";

    [Required]
    public DateTime DateInfo { get; set; } = DateTime.Now;
}
