using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_ThreadsControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Watcher watcher = new Watcher();
            watcher.TimeChanged += OnTimeChange;
            Task watcherTask = watcher.StartWatch();
            var token = watcher.Token;
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                watcher.TokenSource.Cancel();
            }

        }

        static void OnTimeChange(object sender, TimeChangedEventArgs e)
        {
            Console.WriteLine($"Previous time: {e.PreviousDateTime.TimeOfDay}");
            Console.WriteLine($"Last time: {e.LastChangeDateTime.TimeOfDay}");
        }
    }
}
