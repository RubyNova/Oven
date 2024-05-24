﻿namespace Oven.Data.Models;

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