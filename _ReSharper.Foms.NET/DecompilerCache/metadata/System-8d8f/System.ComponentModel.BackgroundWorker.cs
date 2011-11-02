// Type: System.ComponentModel.BackgroundWorker
// Assembly: System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll

namespace System.ComponentModel
{
    [DefaultEvent("DoWork")]
    public class BackgroundWorker : Component
    {
        public BackgroundWorker();

        [Browsable(false)]
        public bool CancellationPending { get; }

        [Browsable(false)]
        public bool IsBusy { get; }

        [DefaultValue(false)]
        public bool WorkerReportsProgress { get; set; }

        [DefaultValue(false)]
        public bool WorkerSupportsCancellation { get; set; }

        public void CancelAsync();
        protected virtual void OnDoWork(DoWorkEventArgs e);
        protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e);
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e);
        public void ReportProgress(int percentProgress);
        public void ReportProgress(int percentProgress, object userState);
        public void RunWorkerAsync();
        public void RunWorkerAsync(object argument);

        public event DoWorkEventHandler DoWork;
        public event ProgressChangedEventHandler ProgressChanged;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
    }
}
