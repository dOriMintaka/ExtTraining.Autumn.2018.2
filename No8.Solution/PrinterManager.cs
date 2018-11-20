using System.Collections.Generic;
using System.Windows.Forms;

namespace No8.Solution
{
    using System;
    using System.Collections;
    using System.IO;

    public class PrinterManager : IEnumerable<Printer>
    {
        private readonly List<Printer> printers;

        public PrinterManager()
        {
            this.printers = new List<Printer>();
        }

        public event EventHandler<PrintEventArgs> Printing;

        public void AddPrinter(Printer printer)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(nameof(printer), "Null printer!");
            }

            if (this.printers.Contains(printer))
            {
                throw new ArgumentException("Printer already exists!");
            }

            this.printers.Add(printer);
        }

        public void Print(Printer p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p), "Null printer!");
            }

            this.Printing?.Invoke(this, new PrintEventArgs("Print started"));
            var o = new OpenFileDialog();
            o.ShowDialog();
            var f = File.OpenRead(o.FileName);
            p.Print(f);
            this.Printing?.Invoke(this, new PrintEventArgs("Print finished"));
        }

        public bool Contains(Printer p)
        {
            return this.printers.Contains(p);
        }

        public IEnumerator<Printer> GetEnumerator()
        {
            return this.printers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
