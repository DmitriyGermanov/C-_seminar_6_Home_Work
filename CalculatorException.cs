namespace Seminar_6
{
    public class CalculatorException : Exception
    {
        private Stack<CalcActionLog> errors;

        public CalculatorException(string message) : base(message)
        {
            errors = new Stack<CalcActionLog>();
        }

        public CalculatorException(string message, Exception inner) : base(message, inner)
        {
            errors = new Stack<CalcActionLog>();
        }

        public CalculatorException(string message, Stack<CalcActionLog> errors, Exception inner) : base(message, inner)
        {
            this.errors = errors;
        }

        public override string ToString()
        {
            return this.Message + " " + string.Join(" ", errors?.Select(x => $"{x.Action} {x.Value}"));
        }
    }

    internal class CalculatorDivideByZeroException : CalculatorException
    {
        public CalculatorDivideByZeroException(string message) : base(message)
        {
        }

        public CalculatorDivideByZeroException(string message, Exception inner) : base(message, inner)
        {
        }

        public CalculatorDivideByZeroException(string message, Stack<CalcActionLog> errors, Exception inner) : base(message, errors, inner)
        {
        }
    }

    internal class CalculatorOverflowException : CalculatorException
    {
        public CalculatorOverflowException(string message) : base(message)
        {
        }

        public CalculatorOverflowException(string message, Exception inner) : base(message, inner)
        {
        }

        public CalculatorOverflowException(string message, Stack<CalcActionLog> errors, Exception inner) : base(message, errors, inner)
        {
        }
    }
}