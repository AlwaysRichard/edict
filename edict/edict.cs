using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace edict
{
    class edict
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen splash = new SplashScreen();
            Application.Run(splash);

            Application.Run(new Aircraft());
        }
    }
}
