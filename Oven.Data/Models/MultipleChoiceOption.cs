using System.ComponentModel.DataAnnotations;

namespace Oven.Data.Models;

public class MultipleChoiceOption(int id, string optionTitle, bool shouldRejectIfChosen)
{
    public int Id { get; set; } = id;
    
    [StringLength(50)]
    public string OptionTitle { get; set; } = optionTitle;
    public bool ShouldRejectIfChosen { get; set; } = shouldRejectIfChosen;

    public MultipleChoiceOption() : this(-1, string.Empty, false)
    {
    }
}