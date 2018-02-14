using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _10_ThreadsControl
{
    class Watcher
    {
        private DateTime _fileLastWriteTime;
        public static string pathToFile { get; private set; } = @"C:\test\test.txt";

        public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
        public event TimeChangedEventHandler TimeChanged;
       
        public CancellationTokenSource TokenSource { get; set; }
      

        public Watcher()
        {
            if (File.Exists(pathToFile))
            {                
                File.Delete(pathToFile);
            }

            using (FileStream fs = File.Create(pathToFile))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("0");
                fs.Write(info, 0, info.Length);
            }
            _fileLastWriteTime = File.GetLastWriteTime(pathToFile);

        }

        public Task StartWatch()
        {
            
            var task = new Task(()=>TrackTimeChange());
            task.Start();
            
            
            return task;
        }

        private void TrackTimeChange()
        {
           
            while (true)
            {                                     
                if (_fileLastWriteTime < File.GetLastWriteTime(pathToFile))
                {
                    Task.Factory.StartNew(() =>
                    TimeChanged(this, new TimeChangedEventArgs { PreviousDateTime = _fileLastWriteTime, LastChangeDateTime = File.GetLastWriteTime(pathToFile) }));

                    _fileLastWriteTime = File.GetLastWriteTime(pathToFile);
                    }
                }
            }
        }
    

   
}
