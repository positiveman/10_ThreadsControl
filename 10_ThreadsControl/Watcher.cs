using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _10_ThreadsControl
{
    class Watcher
    {
        private DateTime _lastTime;

        public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
        public event TimeChangedEventHandler TimeChanged;
        public CancellationToken Token { get; set; }
        public CancellationTokenSource TokenSource { get; set; }

        public Watcher()
        {
            _lastTime = DateTime.Now;

        }

        public Task StartWatch()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = new Task(()=>TrackTimeChange(cts.Token), cts.Token);
            task.Start();
            TokenSource = cts;
            Token = cts.Token;
            return task;
        }

        private void TrackTimeChange(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            while (true)
            { 

                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();                          
                if (_lastTime.AddSeconds(1).Second == DateTime.Now.Second)
                {
                    Task.Factory.StartNew(() =>
                    TimeChanged(this, new TimeChangedEventArgs { PreviousDateTime = _lastTime, LastChangeDateTime = DateTime.Now }));

                    _lastTime = DateTime.Now;
                    }
                }
            }
        }
    

   
}
