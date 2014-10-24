using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace edict
{
    public partial class SplashScreen : Form
    {
        private Timer displayTimer;

        public SplashScreen()
        {
            InitializeComponent();
            //Bitmap b = new Bitmap(this.BackgroundImage);
            //b.MakeTransparent(b.GetPixel(1, 1));
            //this.BackgroundImage = b;

            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version applicationVersion = assemName.Version;
            this.nameVersionLbl.Text = "Exif Direction Image Correction Tool  - V" +
                String.Format("{0,1:0}.", applicationVersion.Major) +
                String.Format("{0,1:0}.", applicationVersion.Minor) +
                String.Format("{0,3:000}", applicationVersion.Build);


            displayTimer = new Timer();
            displayTimer.Interval = 3000;
            displayTimer.Start();
            displayTimer.Tick += new EventHandler(closeSplashScreen);
        }

        public void closeSplashScreen(object sender, EventArgs eArgs)
        {
            Close();
        }

    }
}
