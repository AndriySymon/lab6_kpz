using System;
using System.Windows.Forms;

namespace ClassLibrary2
{
    public class SessionManager
    {
        private Timer sessionTimer;
        private TimeSpan sessionTimeout;
        private int secondsInactive = 0;

        public event Action SessionExpired;

        public SessionManager(TimeSpan timeout)
        {
            sessionTimeout = timeout;
            sessionTimer = new Timer();
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;
        }

        public void Start() => sessionTimer.Start();

        public void Stop() => sessionTimer.Stop();

        public void ResetInactivityTimer() => secondsInactive = 0;

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            secondsInactive++;
            if (secondsInactive >= sessionTimeout.TotalSeconds)
            {
                sessionTimer.Stop();
                SessionExpired?.Invoke();
            }
        }

        public void HookActivityEvents(Control form)
        {
            form.MouseMove += (s, e) => ResetInactivityTimer();
            form.Click += (s, e) => ResetInactivityTimer();
            form.KeyDown += (s, e) => ResetInactivityTimer();
            RegisterRecursive(form.Controls);
        }

        private void RegisterRecursive(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.MouseMove += (s, e) => ResetInactivityTimer();
                control.Click += (s, e) => ResetInactivityTimer();
                control.KeyDown += (s, e) => ResetInactivityTimer();

                if (control.HasChildren)
                    RegisterRecursive(control.Controls);
            }
        }
    }
}
