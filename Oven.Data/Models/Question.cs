using System.Collections.Generic;

namespace Oven.Data.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public double? MinimumScale { get; set; }

        public double? MaximumScale { get; set; }

        public List<MultipleChoiceOption>? AnswerOptions { get; set; }

        public Question(string questionText, QuestionType questionType, double? minimumScale = null, double? maximumScale = null, List<MultipleChoiceOption>? answerOptions = null)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            MinimumScale = minimumScale;
            MaximumScale = maximumScale;
            AnswerOptions = answerOptions;
        }

        public Question()
        {
            QuestionText = string.Empty;
            QuestionType = QuestionType.NoneOrInvalid;
        }
    }
}