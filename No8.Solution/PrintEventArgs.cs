namespace No8.Solution
{
    using System;

    public class PrintEventArgs : EventArgs
    {
        public PrintEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}