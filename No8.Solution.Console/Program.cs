using System;
using NLog;

namespace No8.Solution.Console
{
    using Console = System.Console;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConfigureLog();
            var logger = LogManager.GetCurrentClassLogger();
            PrinterManager manager = new PrinterManager();
            manager.Printing += OnPrint;
            bool isRunning = true;
            while (isRunning)
            {
                Help();
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        AddPrinter(manager);
                        break;
                    case ConsoleKey.D2:
                        Print(manager, (printer) => printer is CanonPrinter);
                        break;
                    case ConsoleKey.D3:
                        Print(manager, (printer) => printer is EpsonPrinter);
                        break;
                    case ConsoleKey.D4:
                        isRunning = false;
                        logger.Info("Application closed");
                        break;
                    default:
                        Help();
                        break;
                }
            }
        }

        static void Print(PrinterManager manager, Predicate<Printer> condition)
        {
            try
            {
                foreach (var p in manager)
                {
                    if (condition(p))
                    {
                        Console.WriteLine(p);
                    }
                }

                string model;
                do
                {
                    Console.WriteLine("Select printer model: ");
                    model = Console.ReadLine();
                }
                while (model == null);

                foreach (var p in manager)
                {
                    if (condition(p) && p.Model.ToUpperInvariant() == model.ToUpperInvariant())
                    {
                        manager.Print(p);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(e);
            }
        }

        static void AddPrinter(PrinterManager manager)
        {
            try
            {
                var logger = LogManager.GetCurrentClassLogger();
                string name;
                do
                {
                    Console.WriteLine("Input valid printer name: ");
                    name = Console.ReadLine();
                }
                while (name == null || (name.ToUpperInvariant() != "EPSON" && name.ToUpperInvariant() != "CANON"));

                Console.WriteLine("Input printer model: ");
                string model = Console.ReadLine();
                Printer temp;
                if (name.ToUpperInvariant() == "CANON")
                {
                    temp = new CanonPrinter(name, model);
                }
                else
                {
                    temp = new EpsonPrinter(name, model);
                }

                if (manager.Contains(temp))
                {
                    Console.WriteLine("This printer already exists");
                    return;
                }

                manager.AddPrinter(temp);
                Console.WriteLine("Printer added");
                logger.Info($"Printer added: {temp}");
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(e);
            }
        }

        static void OnPrint(object sender, PrintEventArgs e)
        {
            Console.WriteLine(e.Message);
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info(e.Message);
        }

        static void Help()
        {
            Console.WriteLine("Select your choice:");
            Console.WriteLine("1. Add new printer");
            Console.WriteLine("2. Print on Canon");
            Console.WriteLine("3. Print on Epson");
            Console.WriteLine("4. Exit");
        }

        static void ConfigureLog()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;
        }
    }
}
