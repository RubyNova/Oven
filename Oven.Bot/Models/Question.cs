using System;
using System.Collections.Generic;

namespace Oven.Bot.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public AnswerKind UserAnswerKind { get; set; }
        public double? MinimumScale { get; set; }
        public double? MaximumScale { get; set; }
        public List<MultipleChoiceOption>? AnswerOptions { get; set; }

        public Question(string questionText, AnswerKind userAnswerKind, double? minimumScale = null, double? maximumScale = null, List<MultipleChoiceOption>? answerOptions = null)
        {
            QuestionText = questionText;
            UserAnswerKind = userAnswerKind;
            MinimumScale = minimumScale;
            MaximumScale = maximumScale;
            AnswerOptions = answerOptions;
        }

        public Question()
        {
            QuestionText = string.Empty;
            UserAnswerKind = AnswerKind.NoneOrInvalid;
        }
    }
}