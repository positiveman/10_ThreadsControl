using System;

namespace _10_ThreadsControl
{
    public class TimeChangedEventArgs : EventArgs
    {
        public DateTime PreviousDateTime { get; set; }
        public DateTime LastChangeDateTime { get; set; }

    }
}