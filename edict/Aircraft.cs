using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoEllipsis;
using System.Threading;

namespace edict
{
    public partial class Aircraft : Form
    {
        public static string TRACK_LOG_FILE = "gpsTracks.csv";
        public static string LOG_FILE = "edict-conversion.log";
        public static string CAP_IMAGE_PROCESSOR = "CapImageProcessor.csv";
        public static string IMAGE_ROOT = "C:\\Missions";
        string workingDirectory;
        private long gpsImageDelta = 0;
        private bool usingUTC;
        
        int fileCount = 0;
        List<TrackLogEntry> trackLog = null;

        public Aircraft()
        {
            InitializeComponent();
            updateVersion();
            this.usingUTC = true;
            
            EllipsisFormat fmt = EllipsisFormat.None;
            fmt |= EllipsisFormat.Middle;
            fmt |= EllipsisFormat.Word;

            this.imagePath.AutoEllipsis = fmt;
            this.workingDirectory = null;
            this.fileCount = 0;
            this.imagePath.Text = "";
            this.trackLogFile.Text = "";
            this.trackLog = null;
            this.trackLogLocation.Enabled = false;
            this.currentFile.Text = "";
            this.readingFile.Text = "0000 of 0000";
            this.fileFailures.Text = String.Format("{0,4:0000}", 0); ;
            this.convertImageFiles.Enabled = false;
            this.direction.Text = String.Format("{0:000}", getOffsetHeading());
            this.photographerPosition.Enabled = false;
            this.trackLogFile.Text = "";
            this.cameraTime.Enabled = false;
            this.deltaSeconds.Text = "";
            this.doReverseGeocode.Checked = isNetworkOn() ? true : false;
            setGeocodingOption(false);

            this.heading315Lbl.Visible = false;
            this.direction315.Enabled = false;
            this.direction315.Visible = false;

            if (Directory.Exists(IMAGE_ROOT))
            {
                this.imageFolderBrowserDialog.SelectedPath = IMAGE_ROOT;
            }
            else
            {
                this.imageFolderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
        }

        private void setGeocodingOption(bool onOff)
        {
            this.reverseGeocode.Enabled = false;
            if (isNetworkOn() && onOff == true)
            {
                this.reverseGeocode.Enabled = true;
            }
            checkGeocodeProviderButons();
            this.doReverseGeocode.Refresh();
        }

        private bool isNetworkOn()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void updateVersion()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version applicationVersion = assemName.Version;
            this.labelVersion.Text = "Version: " +
                String.Format("{0}.", applicationVersion.Major) + 
                String.Format("{0}.", applicationVersion.Minor) +
                String.Format("{0,3:000}", applicationVersion.Build);
        }

        private void findDirectoryBtn_Click(object sender, EventArgs e)
        {
            this.workingDirectory = null;

            this.imagePath.Text = "";
            this.imagePath.Refresh();

            this.trackLogFile.Text = "";
            this.trackLog = null;
            this.trackLogLocation.Enabled = false;
            this.currentFile.Refresh();

            this.cameraTime.Enabled = false;
            this.TimeUTC.Checked = true;
            this.TimeLocal.Checked = false;
            this.deltaSeconds.Text = "";
            this.usingUTC = true;
            this.deltaSeconds.Refresh();

            this.photographerPosition.Enabled = false;
            this.photographerPosition.Refresh();

            setGeocodingOption(false);

            this.convertImageFiles.Enabled = false;
            this.convertImageFiles.Refresh();

            this.currentFile.Text = "";
            this.readingFile.Text = "0000 of 0000";
            this.fileCount = 0;

            this.readingFile.Refresh();
            this.currentFile.Refresh();

            this.fileFailures.Text = String.Format("{0,4:0000}", 0);
            this.fileFailures.Refresh();

            this.progressBar.Value = 0;
            this.progressBar.Refresh();

            DialogResult result = this.imageFolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.workingDirectory = this.imageFolderBrowserDialog.SelectedPath;
                this.imagePath.Text = this.workingDirectory;
                this.photographerPosition.Enabled = true;
                this.convertImageFiles.Enabled = true;

                setGeocodingOption(true);

                this.fileCount = Directory.GetFiles(this.workingDirectory, "*.jpg").Length;
                this.readingFile.Text = String.Format("0000 of {0:0000}", fileCount);
                this.readingFile.Refresh();

                // if a track logfile exists in this directory, enable button to use it.
                if (File.Exists(this.workingDirectory + "\\" + TRACK_LOG_FILE))
                {
                    loadTrackLog();
                }
            }
        }

        private void convertImageFiles_Click(object sender, EventArgs e)
        {
            if (this.workingDirectory == null)
            {
                DialogResult result = MessageBox.Show(
                    "Please select the directory where the images are stored",
                    "Image Directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                this.imageLocation.Enabled = false;
                this.trackLogLocation.Enabled = false;
                this.photographerPosition.Enabled = false;
                this.cameraTime.Enabled = false;
                this.convertImageFiles.Enabled = false;
                this.finishedBtn.Enabled = false;
                this.reverseGeocode.Enabled = false;

                Thread startConversion = new Thread(new ThreadStart(convertFiles));
                startConversion.Start();
                //convertFiles();
            }
        }

        private void convertFiles()
        {
            string folderName = this.workingDirectory;
            this.progressBar.Maximum = fileCount;
            this.progressBar.Minimum = 0;
            this.progressBar.Value = 0;
            int fileConversionFailure = 0;
            bool imageError = false;
            TextWriter edictLog = new StreamWriter(folderName + "\\" + LOG_FILE);
            edictLog.WriteLine("EDICT - start conversion " + DateTime.Now);

            TextWriter capImageProcessorFil = new StreamWriter(folderName + "\\" + CAP_IMAGE_PROCESSOR);
            CapImageProcessorHeader(capImageProcessorFil);

            int offsetHeading = getOffsetHeading();
            Bitmap image;
            foreach (string file in Directory.GetFiles(folderName, "*.jpg"))
            {
                this.currentFile.Text = Path.GetFileName(file);
                this.currentFile.Refresh();
                this.progressBar.Value += 1;

                this.readingFile.Text =
                    String.Format("{0:0000}", progressBar.Value) +
                    " of " +
                    String.Format("{0:0000}", fileCount);
                this.readingFile.Refresh();

                try
                {
                    image = new Bitmap(file);

                    bool okToGeocode = isNetworkOn() ? this.doReverseGeocode.Checked : false;

                    /* Calculate offset heading, the estimated direction the camera
                     * is pointing when the image was taken, not the direction of the 
                     * aircraft.
                     */
                    if (gpsUtils.isImageFile(file) == true && 
                        gpsUtils.offsetGpsImgDirection(image, offsetHeading, trackLog, this.gpsImageDelta, this.usingUTC) == true)
                    {
                        
                        /* sync GPS time to Camera exposure time to ensure RoboGEO calculates correct TOT */
                        gpsUtils.syncGpsCameraTime(image);

                        if (projectToTarget.Checked)
                        {
                            double altitude = gpsUtils.getGpsAltitude(image);
                            double elevation = gpsUtils.getGoogleElevation(image);
                            double agl = altitude - elevation;
                            gpsUtils.projectImageGeoReference(image, Convert.ToDouble(agl), Convert.ToDouble(vAngle.Text));
                        }

                        /* Using Google Maps API, perform a reverse geocoding lookup to find
                         * Road or Geographic name (Thoroughfare Name)
                         * City/Town (Locality Name)
                         * State (Administrative Area Name)
                         */
                        if (okToGeocode)
                        {
                            if (useGoogleMaps.Checked)
                            {
                                GoogleGPS reverseGoogleGeocode = new GoogleGPS(gpsUtils.getGpsLat(image).decimalDegrees, gpsUtils.getGpsLong(image).decimalDegrees);
                                gpsUtils.setImageDescription(image,
                                    reverseGoogleGeocode.ThoroughfareName + " - " +
                                    reverseGoogleGeocode.LocalityName + ", " +
                                    reverseGoogleGeocode.AdministrativeAreaName + "\0");
                            }
                            else
                            {
                                YahooGPS reverseYahooGeocode = new YahooGPS(gpsUtils.getGpsLat(image).decimalDegrees, gpsUtils.getGpsLong(image).decimalDegrees);
                                gpsUtils.setImageDescription(image,
                                    reverseYahooGeocode.County + " - " +
                                    reverseYahooGeocode.City + ", " +
                                    reverseYahooGeocode.StateCode + "\0");
                            }
                        }


                        CapImageProcessorData(capImageProcessorFil, Path.GetFileName(file), image);


                        /* A bug with Microsoft Bitmat renders the Bitmat.save function
                         * on the image opened, so we must copy the image to a new 
                         * bitmat, but the copy does not include the EXIF data
                         * so we need to save that from the original image so we
                         * can insert it into the new image to be saved.
                         */
                        ArrayList alPropertyItems = new ArrayList();
                        foreach (PropertyItem pi in image.PropertyItems)
                        {
                            alPropertyItems.Add(pi);
                        }
                        Image iSave = new Bitmap(image.Width, image.Height, image.PixelFormat);
                        Graphics gSave = Graphics.FromImage(iSave);
                        gSave.DrawImage(image, 0, 0, image.Width, image.Height);
                        ImageFormat ifOriginal = image.RawFormat;
                        image.Dispose();
                        gSave.Dispose();

                        /* Now add the property data into the newly create image, and
                         * then replace the image with this new one.
                         */
                        foreach (PropertyItem pi in alPropertyItems)
                        {
                            iSave.SetPropertyItem(pi);
                        }
                        iSave.Save(file, ifOriginal);
                        iSave.Dispose();
                        edictLog.WriteLine("Success: " + file);
                    }
                    else
                    {
                        edictLog.WriteLine("Error: no gps data " + file);
                        this.fileFailures.Text = String.Format("{0,4:0000}", fileConversionFailure++);
                        this.fileFailures.Refresh();
                    }
                }
                catch (Exception)
                {
                    edictLog.WriteLine("Error: invalid image file " + file);
                    imageError = true;
                }
                Thread.Sleep(10);
            }

            // Sound all is done.
            System.Media.SystemSounds.Beep.Play();
            edictLog.WriteLine("EDICT - finished conversion " + DateTime.Now);
            edictLog.Close();
            capImageProcessorFil.Close();

            // Update conversion failure information box
            this.fileFailures.Text = String.Format("{0,4:0000}", fileConversionFailure);
            this.fileFailures.Refresh();

            // Change file name to finished.
            this.currentFile.Text = "Finished Processing";
            this.currentFile.Refresh();

            if (imageError == true)
            {
                DialogResult result = MessageBox.Show(
                    "One or more files are invalid, see log for more information.",
                    "Image Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            this.imageLocation.Enabled = true;
            if (File.Exists(this.workingDirectory + "\\" + TRACK_LOG_FILE))
            {
                this.cameraTime.Enabled = true;
                this.trackLogLocation.Enabled = true;
            }
            this.photographerPosition.Enabled = true;
            this.convertImageFiles.Enabled = false;
            this.finishedBtn.Enabled = true;
            setGeocodingOption(true);
        }

        private void CapImageProcessorHeader(TextWriter capImageProcessorFil)
        {
            String headerLine = "Filename,Target ID,Target Name,City,State,Lat,Lon,Zulu Date,Zulu Time Over Target,Picture Heading (true)";
            capImageProcessorFil.WriteLine(headerLine);
        }

        private void CapImageProcessorData(TextWriter capImageProcessorFil, String filename, Image image)
        {
            // 123.jpg,1,Signal Peak,Irvine,CA,34 15.5,117 30.4,30-Oct-07,21:00,230
            coordinate lat = gpsUtils.getGpsLat(image);
            coordinate lon = gpsUtils.getGpsLong(image);
            double heading = gpsUtils.getGpsImageHeading(image);

            // Convert string date "2009:10:15:09:36:15" to DateTime object
            string[] dateTimeSplit = gpsUtils.getGpsDateTime(image).Split(':');
            DateTime gpsDateTime = new DateTime(
                (int)Int32.Parse(dateTimeSplit[0]), (int)Int32.Parse(dateTimeSplit[1]), (int)Int32.Parse(dateTimeSplit[2]),
                (int)Int32.Parse(dateTimeSplit[3]), (int)Int32.Parse(dateTimeSplit[4]), (int)Int32.Parse(dateTimeSplit[5]));

            string city = "";
            string state = "";
            try
            {
                string[] cityState = gpsUtils.getImageDescription(image).Split(',');
                city = cityState[0];
                state = cityState[1].Trim();
            }
            catch (Exception) { /* do nothing */ }

            capImageProcessorFil.WriteLine(
                filename + "," +                                // filename
                "ID," +                                         // Target ID
                "CAP," +                                        // Target Name
                city + "," +                                    // City
                state + "," +                                   // State
                CapImageProcessorCoord(lat) + "," +             // Lat
                CapImageProcessorCoord(lon) + "," +             // Lon
                String.Format("{0:d-MMM-yy},", gpsDateTime) +   // Zulu Date
                String.Format("{0:HH:mm},", gpsDateTime) +      // Zulu Time
                String.Format("{0:0,#}", heading));             // Picture Heading
       }

        private String CapImageProcessorCoord(coordinate cord)
        {
            double minsec = cord.min + (cord.sec / 60.0);
            return String.Format("{0:0} ", cord.deg) +
                   String.Format("{0:0.00}", Math.Round(minsec, 2));
        }


        private int getOffsetHeading()
        {
            int offsetHeading = 0;
            if (this.direction045.Checked == true) { offsetHeading = 45;  }
            if (this.direction090.Checked == true) { offsetHeading = 90;  }
            if (this.direction135.Checked == true) { offsetHeading = 135; }
            if (this.direction225.Checked == true) { offsetHeading = 225; }
            if (this.direction270.Checked == true) { offsetHeading = 270; }
            if (this.direction315.Checked == true) { offsetHeading = 315; }

            return offsetHeading;
        }

        private void finishedBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restoreOrigHeading_CheckedChanged(object sender, EventArgs e)
        {
            if (this.restoreOrigHeading.Checked == true)
            {
                setHeadingRadio(false);
            }
            else
            {
                setHeadingRadio(true);
            }
            this.direction.Text = String.Format("{0:000}", getOffsetHeading());
            this.convertImageFiles.Enabled = true;
        }

        private void direction_Click(object sender, EventArgs e)
        {
            this.direction.Text = String.Format("{0:000}", getOffsetHeading());
            this.convertImageFiles.Enabled = true;
        }

        private void setHeadingRadio(bool isOn)
        {
            this.direction045.Enabled = isOn;
            this.direction090.Enabled = isOn;
            this.direction135.Enabled = isOn;
            this.direction225.Enabled = isOn;
            this.direction270.Enabled = isOn;
            //this.direction315.Enabled = isOn;

            if (isOn == true)
            {
                this.direction225.Checked = true;
            }
            else
            {
                this.direction045.Checked = false;
                this.direction090.Checked = false;
                this.direction135.Checked = false;
                this.direction225.Checked = false;
                this.direction270.Checked = false;
                this.direction315.Checked = false;
            }
        }

        private void findTrackLogBtn_Click(object sender, EventArgs e)
        {
            string usingTrackLogFile = this.workingDirectory + "\\" + TRACK_LOG_FILE;
            if (this.trackLog == null && File.Exists(usingTrackLogFile))
            {
                this.trackLogFile.Text = usingTrackLogFile;
                this.trackLogFile.Refresh();

                this.trackLog = new List<TrackLogEntry>();
                TextReader tr = new StreamReader(usingTrackLogFile);
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    this.trackLog.Add(new TrackLogEntry(line));
                }
                
                this.cameraTime.Enabled = true;
                this.deltaSeconds.Text = "";

                this.convertImageFiles.Enabled = true;
            }
            else
            {
                this.trackLog = null;
                this.trackLogFile.Text = "";
                this.trackLogFile.Refresh();

                this.cameraTime.Enabled = false;
                this.deltaSeconds.Text = "";
                this.deltaSeconds.Refresh();
            }
        }

        private void loadTrackLog()
        {
            string trackLogFile = this.workingDirectory + "\\" + TRACK_LOG_FILE;
            if (File.Exists(trackLogFile))
            {
                this.trackLogFile.Text = trackLogFile;
                this.trackLogLocation.Enabled = true;
                this.trackLogFile.Refresh();

                this.trackLog = new List<TrackLogEntry>();
                TextReader tr = new StreamReader(trackLogFile);
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    this.trackLog.Add(new TrackLogEntry(line));
                }

                /*
                for (int i = 1; i < trackLog.Count - 1; i++)
                {
                    this.trackLog[i] = gpsUtils.getHeading(trackLog[i],
                        trackLog[i - 1].lat,
                        trackLog[i - 1].lon,
                        trackLog[i + 1].lat,
                        trackLog[i + 1].lon);
                }
                */

                this.cameraTime.Enabled = true;
                this.deltaSeconds.Text = "";
                this.convertImageFiles.Enabled = true;

            }
            else
            {
                this.trackLog = null;
                this.trackLogFile.Text = "No Track Log";
                this.trackLogLocation.Enabled = false;
                this.trackLogFile.Refresh();

                this.cameraTime.Enabled = false;
                this.deltaSeconds.Text = "";
                this.deltaSeconds.Refresh();
            }
        }

        private void cameraTimeBtn_Click(object sender, EventArgs e)
        {
            CameraTime ct = new CameraTime(this.TimeUTC.Checked, this.workingDirectory);
            DialogResult result = ct.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.usingUTC = ct.getImageUTC();
                this.TimeUTC.Checked = this.usingUTC;
                this.TimeLocal.Checked = !this.usingUTC;
                this.convertImageFiles.Enabled = true;
                this.deltaSeconds.Text = ct.getDeltaSeconds();
                this.gpsImageDelta = ct.getDeltaTicks();
            }
            else
            {
                this.deltaSeconds.Text = "";
                this.gpsImageDelta = 0;
                this.TimeUTC.Checked = true;
                this.TimeLocal.Checked = false;
                this.usingUTC = true;
            }
            ct.Dispose();
        }

        private void TimeUTC_CheckedChanged(object sender, EventArgs e)
        {
            this.usingUTC = this.TimeUTC.Checked;
            this.deltaSeconds.Text = "";
            this.gpsImageDelta = 0;
            this.convertImageFiles.Enabled = true;
        }

        private void doReverseGeocode_changed(object sender, EventArgs e)
        {
            checkGeocodeProviderButons();
        }

        private void checkGeocodeProviderButons()
        {
            if (doReverseGeocode.Checked)
            {
                useGoogleMaps.Enabled = true;
                useYahooMaps.Enabled = true;
            }
            else
            {
                useGoogleMaps.Enabled = false;
                useYahooMaps.Enabled = false;
            }
        }

        private void setProjectGpsData(object sender, EventArgs e)
        {
            this.projectGps.Enabled = projectToTarget.Checked;
        }
    }
}
