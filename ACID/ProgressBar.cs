using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACID
{
    class ProgressBar
    {
        public Authentication Auth;
        public delegate void ClosePBar();
        public ClosePBar myDelegate;
        public ProgressBar()
        {
            Auth = new Authentication();
            myDelegate = new ClosePBar(kill);
        }
        public void Start()
        {
           Auth.ShowDialog();
        }
        private void kill ()
        {
            Auth.Dispose();
        }
    }
}
