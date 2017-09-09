using System.ComponentModel.DataAnnotations;

namespace ANummerGenerator.Model
{
    public class ANummerModel
    {
        [Required]
        [Range(1, 10,ErrorMessage = "The range is 1 to 10")]
        public int Amount { get; set; } = 1;
    }
}
