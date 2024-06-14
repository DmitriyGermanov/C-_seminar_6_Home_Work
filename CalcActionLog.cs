namespace Seminar_6
{
    public class CalcActionLog
    {
        public CalcAction? Action { get; private set; }
        public double Value { get; private set; }
        public CalcActionLog(CalcAction action, double value)
        {
            Action = action;
            Value = value;
        }
        public CalcActionLog(double value) { Value = value; Action = null;  }
    }
}
