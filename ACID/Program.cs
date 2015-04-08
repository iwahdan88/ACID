using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACID
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Login LoginForm;
            Engine eng;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(LoginForm = new Login());
            while (LoginForm.IsDisposed == false)
            {
                /* Do Nothing */
            }
            if(LoginForm.IsLoginValid() == false)
            {
                return;
            }
            Application.Run(eng = new Engine(LoginForm.GetUserID(), LoginForm.GetPass()));
            return;
        }
    }
}
