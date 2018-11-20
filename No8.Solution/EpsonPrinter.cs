namespace No8.Solution
{
    using System;
    using System.IO;

    public sealed class EpsonPrinter : Printer
    {
        public EpsonPrinter(string name, string model)
            : base(name, model)
        {
            if (name.ToUpperInvariant() != "EPSON")
            {
                throw new ArgumentException("Incorrect printer name!", nameof(name));
            }
        }

        public override void Print(FileStream fs)
        {
            Console.WriteLine("Epson prints:\n");
            base.Print(fs);
        }
    }
}