namespace Oven.Bot.Models
{
    public class MultipleChoiceOption
    {
        public int Id { get; set; }
        public string OptionTitle { get; set; }
        public bool ShouldRejectIfChosen { get; set; }

        public MultipleChoiceOption(int id, string optionTitle, bool shouldRejectIfChosen)
        {
            Id = id;
            OptionTitle = optionTitle;
            ShouldRejectIfChosen = shouldRejectIfChosen;
        }

        public MultipleChoiceOption()
        {
            Id = -1;
            OptionTitle = string.Empty;
            ShouldRejectIfChosen = false;
        }
    }
}