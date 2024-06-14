
namespace Seminar_6
{
    public class Calculator : ICalc
    {
        private Stack<CalcActionLog> actions;
        public event EventHandler<EventArgs> GotResult;
        public double Result => actions.Peek().Value;
        public CalcAction CalcAction => (CalcAction)actions.Peek().Action;

        public Calculator()
        {
            actions = new Stack<CalcActionLog>();
            actions.Push(new CalcActionLog(0));
        }
        public void Add(int i)
        {
            Add((double)i);
        }

        public void Sub(int i)
        {
            Sub((double)i);
        }

        public void Div(int i)
        {
            Div((double)i);
        }

        public void Mul(int i)
        {
            Mul((double)i);
        }

        public void Add(double i)
        {
            double temp = i + actions.Peek().Value;
            if (double.IsInfinity(temp) || double.IsNaN(temp))
            {
                var innerException = new OverflowException($"Ошибка при сложении {actions.Peek().Value} и {i}");
                throw new CalculatorOverflowException("Ошибка! Переполнение типа double!", actions, innerException);
            }
            Action(i, (a, b) => a + b, CalcAction.Add);
        }

        public void Sub(double i)
        {
            double temp = actions.Peek().Value - i;
            if (double.IsInfinity(temp) || double.IsNaN(temp))
            {
                var innerException = new OverflowException($"Ошибка при вычитании {actions.Peek().Value} и {i}");
                throw new CalculatorOverflowException("Ошибка! Переполнение типа double!", actions, innerException);
            }
            Action(i, (a, b) => a - b, CalcAction.Sub);
        }

        public void Div(double i)
        {
            if (i == 0)
            {
                throw new CalculatorDivideByZeroException("Ошибка! Деление на ноль!", actions, null);
            }
            double temp = actions.Peek().Value / i;
            if (double.IsInfinity(temp) || double.IsNaN(temp))
            {
                var innerException = new OverflowException($"Ошибка при делении {actions.Peek().Value} на {i}");
                throw new CalculatorOverflowException("Ошибка! Переполнение типа double!", actions, innerException);
            }
            Action(i, (a, b) => a / b, CalcAction.Div);
        }

        public void Mul(double i)
        {
            double temp = i * actions.Peek().Value;
            if (double.IsInfinity(temp) || double.IsNaN(temp))
            {
                var innerException = new OverflowException($"Ошибка при умножении {actions.Peek().Value} на {i}");
                throw new CalculatorOverflowException("Ошибка! Переполнение типа double!", actions, innerException);
            }
            Action(i, (a, b) => a * b, CalcAction.Mul);
        }

        public void CancelLast()
        {
            if (actions.Count > 1)
            {
                actions.Pop();
                OnGotResult();
            }
        }

        private void Action(double i, Func<double, double, double> operation, CalcAction action)
        {
            double lastResult = actions.Peek().Value;
            double newResult = operation(lastResult, i);
            actions.Push(new CalcActionLog(action, newResult));
            OnGotResult();
        }

        protected virtual void OnGotResult()
        {
            GotResult?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return actions.Peek().ToString();
        }
    }



}