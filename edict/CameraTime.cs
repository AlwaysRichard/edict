using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace edict
{
    public partial class CameraTime : Form
    {
        private DateTime imageDateTime;
        private DateTime gpsDateTime;
        private long delta = 0;
        private string deltaSeconds = "0 Seconds";
        private long oneSecond;
        bool usingUTC = true;
        bool imageUTC = true;

        public CameraTime(bool bUTC, string imageLocation)
        {
            InitializeComponent();
            this.gpsImage.ImageLocation = "";
            this.usingUTC = bUTC;
            this.openGpsImageDlg.InitialDirectory = imageLocation;

            DateTime dateTime = DateTime.Now;
            this.day.Value = dateTime.Day;
            this.month.SelectedIndex = dateTime.Month - 1;
            this.year.Value = dateTime.Year;
            this.cameraTimeLbl.Text = "";

            DateTime d1 = new DateTime(2009, 1, 1, 1, 0, 0);
            DateTime d2 = new DateTime(2009, 1, 1, 1, 0, 1);
            this.oneSecond = Math.Abs(d2.Ticks - d1.Ticks);

            if (usingUTC == false)
            {
                this.ImageUTC.Checked = false;
                this.ImageLocal.Checked = true;
                this.GpsUTC.Checked = false;
                this.GpsLocal.Checked = true;
            }
            else
            {
                this.ImageUTC.Checked = true;
                this.ImageLocal.Checked = false;
                this.GpsUTC.Checked = true;
                this.GpsLocal.Checked = false;
            }
        }

        public long getDeltaTicks()
        {
            return this.delta;
        }

        public string getDeltaSeconds()
        {
            return deltaSeconds;
        }

        public bool getImageUTC()
        {
            return this.imageUTC;
        }

        private void loadImageBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openGpsImageDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = this.openGpsImageDlg.FileName;
                if (File.Exists(filename))
                {
                    this.gpsImage.SizeMode = PictureBoxSizeMode.Zoom;
                    this.gpsImage.ImageLocation = "";
                    Bitmap image = new Bitmap(filename);
                    try
                    {
                        string cameraExifTime = gpsUtils.getCameraDateTime(image);
                        string[] dateTimeSplit = cameraExifTime.Split(':');
                        this.imageDateTime = new DateTime(
                            (int)Int32.Parse(dateTimeSplit[0]), (int)Int32.Parse(dateTimeSplit[1]), (int)Int32.Parse(dateTimeSplit[2]),
                            (int)Int32.Parse(dateTimeSplit[3]), (int)Int32.Parse(dateTimeSplit[4]), (int)Int32.Parse(dateTimeSplit[5]));

                        setGpsTimeControls(this.imageDateTime, this.GpsUTC.Checked);
                        
                        this.gpsImage.ImageLocation = filename;
                        this.cameraTimeLbl.Text = String.Format("{0:HH:mm:ss dd'-'MMM'-'yyyy}", this.imageDateTime);
                        calculateDeltaTime();
                    }
                    catch (Exception)
                    {
                        imageDateTime = DateTime.Now;
                        this.gpsImage.ImageLocation = "";
                        this.cameraTimeLbl.Text = "";
                    }

                    try
                    {
                        string gpsExifTime = gpsUtils.getGpsDateTime(image);
                        string[] dateTimeSplit = gpsExifTime.Split(':');
                        this.gpsDateTime = new DateTime(
                            (int)Int32.Parse(dateTimeSplit[0]), (int)Int32.Parse(dateTimeSplit[1]), (int)Int32.Parse(dateTimeSplit[2]),
                            (int)Int32.Parse(dateTimeSplit[3]), (int)Int32.Parse(dateTimeSplit[4]), (int)Int32.Parse(dateTimeSplit[5]));

                        setGpsTimeControls(this.gpsDateTime, true);
                    }
                    catch (Exception) { /* do nothing */ }
                }
            }
        }

        private void setGpsTimeControls(DateTime argTime, bool useZulu)
        {
            this.hour.Value = argTime.Hour;
            this.minute.Value = argTime.Minute;
            this.seconds.Value = argTime.Second;
            this.day.Value = argTime.Day;
            this.month.SelectedIndex = argTime.Month - 1;
            this.year.Value = argTime.Year;
            this.GpsUTC.Checked = useZulu;
            this.GpsLocal.Checked = !useZulu;
        }

        private void calculateDeltaTime()
        {
            if (this.gpsImage.ImageLocation.Length > 0)
            {
                gpsDateTime = new DateTime(Decimal.ToInt32(this.year.Value), this.month.SelectedIndex + 1, Decimal.ToInt32(this.day.Value),
                                           Decimal.ToInt32(this.hour.Value), Decimal.ToInt32(this.minute.Value), Decimal.ToInt32(this.seconds.Value));
                if (this.GpsLocal.Checked == true)
                {
                    gpsDateTime = gpsDateTime.ToUniversalTime();
                }

                DateTime imageTime = this.imageDateTime;
                if (this.ImageLocal.Checked == true)
                {
                    imageTime = imageTime.ToUniversalTime();
                }

                this.delta = this.gpsDateTime.Ticks - imageTime.Ticks;
                double secondsOff = (double)this.delta / (double)this.oneSecond;
                this.deltaSeconds = String.Format("{0:0}", secondsOff) + " Seconds";
                this.offsetSeconds.Text = this.deltaSeconds;
            }
        }

        private void calculateDeltaTime_Click(object sender, EventArgs e)
        {
            calculateDeltaTime();
            this.imageUTC = this.ImageUTC.Checked;
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void done_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
