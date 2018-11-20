namespace No8.Solution
{
    using System;
    using System.IO;

    public sealed class CanonPrinter : Printer
    {
        public CanonPrinter(string name, string model)
            : base(name, model)
        {
            if (name.ToUpperInvariant() != "CANON")
            {
                throw new ArgumentException("Incorrect printer name!", nameof(name));
            }
        }

        public override void Print(FileStream fs)
        {
            Console.WriteLine("Canon prints:\n");
            base.Print(fs);
        }
    }
}
