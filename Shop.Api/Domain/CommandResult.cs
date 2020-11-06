namespace Shop.Api.Domain
{
    public class CommandResult<T>
    {
        private CommandResult(T result)
        {
            Result = result;
        }

        private CommandResult(CommandResultFailures failureReason)
        {
            FailureReason = failureReason;
        }

        public CommandResultFailures? FailureReason { get; }

        public bool IsSuccess => !FailureReason.HasValue;

        public T Result { get; }

        public static CommandResult<T> Fail(CommandResultFailures reason)
        {
            return new CommandResult<T>(reason);
        }

        public static CommandResult<T> Success(T result)
        {
            return new CommandResult<T>(result);
        }
    }
}