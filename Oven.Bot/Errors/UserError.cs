using Remora.Results;

namespace Oven.Bot.Errors
{
    public record UserError : ResultError
    {
        public UserError(string message) : base(message)
        {
        }
    }
}