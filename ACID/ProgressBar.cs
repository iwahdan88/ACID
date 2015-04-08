using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACID
{
    class ProgressBar
    {
        Authentication Auth;
        public ProgressBar()
        {
            Auth = new Authentication();
        }
        public void Start()
        {
            Auth.ShowDialog();
        }
        public void Stop()
        {
            Auth.Close();
        }
    }
}
