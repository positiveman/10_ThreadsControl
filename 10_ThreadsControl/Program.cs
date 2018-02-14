using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _10_ThreadsControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" If you manually change the file- you'll see update time in console. If you want to write to the file the  '1' and finish task, press 'Y'. ");

            Watcher watcher = new Watcher();
            watcher.TimeChanged += OnTimeChange;
            Task watcherTask = watcher.StartWatch();
            string Answer = Console.ReadLine();

            if (Answer =="y")
            {
                System.IO.File.WriteAllText(Watcher.pathToFile, "1");
            }
           

        }

        static void OnTimeChange(object sender, TimeChangedEventArgs e)
        {
            Console.WriteLine($"Previous write time: {e.PreviousDateTime.TimeOfDay}");
            Console.WriteLine($"Last write time: {e.LastChangeDateTime.TimeOfDay}");

            string text = System.IO.File.ReadAllText(Watcher.pathToFile);
            if (text == "0")
            {
                System.IO.File.WriteAllText(Watcher.pathToFile, "1");
            }
            Thread.Sleep(10000);
            Console.WriteLine("File content changed to 1 ten seconds ago");


        }
    }
}
