namespace Oven.Bot.Models
{
    public enum AnswerKind
    {
        NoneOrInvalid,
        Text,
        Integer,
        Decimal,
        CustomScale,
        Percentage,
        MultipleChoice,
        Url
    }
}