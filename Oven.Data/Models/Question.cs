using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oven.Data.Models;

public class Question(
    string questionText,
    AnswerKind userAnswerKind,
    double? minimumScale = null,
    double? maximumScale = null,
    List<MultipleChoiceOption>? answerOptions = null)
{
    public int Id { get; set; }
    
    [StringLength(250)]
    public string QuestionText { get; set; } = questionText;
    public AnswerKind UserAnswerKind { get; set; } = userAnswerKind;
    public double? MinimumScale { get; set; } = minimumScale;
    public double? MaximumScale { get; set; } = maximumScale;
    public List<MultipleChoiceOption>? AnswerOptions { get; set; } = answerOptions;

    public Question() : this(string.Empty, AnswerKind.NoneOrInvalid, null, null, null)
    {
    }
}