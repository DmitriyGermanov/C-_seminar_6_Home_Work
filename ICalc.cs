namespace Seminar_6
{
    interface ICalc
    {
        event EventHandler<EventArgs> GotResult;
        void Add(double i);
        void Sub(double i);
        void Div(double i);
        void Mul(double i);
        void CancelLast();
    }

}
