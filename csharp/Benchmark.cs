using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Utils
{
    /// <summary>
    /// A logger class that lets you count the running time of a code block. 
    /// It is intended to be used primarily within a using block, but it also lets you control the counter manually.
    /// This particular implementation uses the log4net library.
    /// </summary>
    public class Benchmark : IDisposable
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public string Message { get; set; }
        private bool _showStart;

        public Benchmark()
        {
            this.Start = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
        }
        
        public Benchmark(string message, bool showStart = true) : this()
        {
            this._showStart = showStart;
            this.Message = message;
            if (showStart)
            {
                string _start = String.Format("{0,-8}", "Inicio: ");
                string _date = DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss.fff tt]") + " ";
                string _message = String.Format("{0,-21}", "[") + "] " + _start + this.Message;
                System.Diagnostics.Debug.WriteLine(_date + _message);
                log.Info(_message);
            }
        }

        public void Dispose()
        {
            this.Stop();
            this.Show();
        }

        public void Stop()
        {
            this.End = DateTime.Now;
        }

        public void Reset()
        {
            this.Start = DateTime.Now;
            this.End = default(DateTime);
        }

        public void Reset(string message)
        {
            this.Message = message;
            this.Reset();
        }

        public TimeSpan Duration
        {
            get
            {
                if (this.Start.Equals(DateTime.MinValue) || this.End.Equals(DateTime.MinValue))
                {
                    return default(TimeSpan);
                }

                return this.End.Subtract(this.Start);
            }
        }

        public void Show()
        {
            string _end = _showStart ? String.Format("{0,-8}", "Fin: ") : "";
            string _date = DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss.fff tt]") + " ";
            string _message = "[" + String.Format("{0,-20}", "Dur: " + this.Duration.TotalMilliseconds + "ms") + "] " + _end + this.Message;
            System.Diagnostics.Debug.WriteLine(_date + _message);
            log.Info(_message);
        }

    }
}
