namespace edict
{
    partial class Aircraft
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label18;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aircraft));
            this.heading315Lbl = new System.Windows.Forms.Label();
            this.photographerPosition = new System.Windows.Forms.GroupBox();
            this.projectGps = new System.Windows.Forms.GroupBox();
            this.vAngle = new System.Windows.Forms.TextBox();
            this.vAngleDiagram = new System.Windows.Forms.PictureBox();
            this.projectToTarget = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.restoreOrigHeading = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.direction135 = new System.Windows.Forms.RadioButton();
            this.direction090 = new System.Windows.Forms.RadioButton();
            this.direction045 = new System.Windows.Forms.RadioButton();
            this.direction225 = new System.Windows.Forms.RadioButton();
            this.direction270 = new System.Windows.Forms.RadioButton();
            this.direction315 = new System.Windows.Forms.RadioButton();
            this.findDirectoryBtn = new System.Windows.Forms.Button();
            this.imageFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.convertImageFiles = new System.Windows.Forms.Button();
            this.finishedBtn = new System.Windows.Forms.Button();
            this.readingFile = new System.Windows.Forms.Label();
            this.direction = new System.Windows.Forms.Label();
            this.fileFailures = new System.Windows.Forms.Label();
            this.imageLocation = new System.Windows.Forms.GroupBox();
            this.imagePath = new AutoEllipsis.LabelEllipsis();
            this.processImages = new System.Windows.Forms.GroupBox();
            this.currentFile = new AutoEllipsis.LabelEllipsis();
            this.trackLogLocation = new System.Windows.Forms.GroupBox();
            this.trackLogFile = new AutoEllipsis.LabelEllipsis();
            this.openTrackLogDialog = new System.Windows.Forms.OpenFileDialog();
            this.cameraTime = new System.Windows.Forms.GroupBox();
            this.TimeLocal = new System.Windows.Forms.RadioButton();
            this.TimeUTC = new System.Windows.Forms.RadioButton();
            this.deltaSeconds = new System.Windows.Forms.Label();
            this.cameraTimeBtn = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reverseGeocode = new System.Windows.Forms.GroupBox();
            this.useYahooMaps = new System.Windows.Forms.RadioButton();
            this.useGoogleMaps = new System.Windows.Forms.RadioButton();
            this.doReverseGeocode = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            this.photographerPosition.SuspendLayout();
            this.projectGps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vAngleDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.imageLocation.SuspendLayout();
            this.processImages.SuspendLayout();
            this.trackLogLocation.SuspendLayout();
            this.cameraTime.SuspendLayout();
            this.reverseGeocode.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(22, 58);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(28, 13);
            label4.TabIndex = 1;
            label4.Text = "270";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(22, 89);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(28, 13);
            label5.TabIndex = 2;
            label5.Text = "225";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(189, 26);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(28, 13);
            label6.TabIndex = 3;
            label6.Text = "045";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(189, 58);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(28, 13);
            label7.TabIndex = 4;
            label7.Text = "090";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(189, 89);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(28, 13);
            label8.TabIndex = 5;
            label8.Text = "135";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(10, 51);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(96, 13);
            label10.TabIndex = 5;
            label10.Text = "Converting File:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(12, 551);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(324, 13);
            label11.TabIndex = 8;
            label11.Text = "Copyright © 2009 by Richard E. Cox Jr., of Civil Air Patrol, NH Wing";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label15.Location = new System.Drawing.Point(310, 25);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(107, 13);
            label15.TabIndex = 16;
            label15.Text = "No GPS Heading:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label18.Location = new System.Drawing.Point(167, 25);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(100, 13);
            label18.TabIndex = 18;
            label18.Text = "Direction Offset:";
            // 
            // heading315Lbl
            // 
            this.heading315Lbl.AutoSize = true;
            this.heading315Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heading315Lbl.Location = new System.Drawing.Point(22, 26);
            this.heading315Lbl.Name = "heading315Lbl";
            this.heading315Lbl.Size = new System.Drawing.Size(28, 13);
            this.heading315Lbl.TabIndex = 0;
            this.heading315Lbl.Text = "315";
            // 
            // photographerPosition
            // 
            this.photographerPosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.photographerPosition.Controls.Add(this.projectGps);
            this.photographerPosition.Controls.Add(this.projectToTarget);
            this.photographerPosition.Controls.Add(this.pictureBox2);
            this.photographerPosition.Controls.Add(this.restoreOrigHeading);
            this.photographerPosition.Controls.Add(this.label12);
            this.photographerPosition.Controls.Add(this.direction135);
            this.photographerPosition.Controls.Add(this.direction090);
            this.photographerPosition.Controls.Add(this.direction045);
            this.photographerPosition.Controls.Add(this.direction225);
            this.photographerPosition.Controls.Add(this.direction270);
            this.photographerPosition.Controls.Add(this.direction315);
            this.photographerPosition.Controls.Add(label8);
            this.photographerPosition.Controls.Add(label7);
            this.photographerPosition.Controls.Add(label6);
            this.photographerPosition.Controls.Add(label5);
            this.photographerPosition.Controls.Add(label4);
            this.photographerPosition.Controls.Add(this.heading315Lbl);
            this.photographerPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.photographerPosition.Location = new System.Drawing.Point(15, 189);
            this.photographerPosition.Name = "photographerPosition";
            this.photographerPosition.Size = new System.Drawing.Size(470, 164);
            this.photographerPosition.TabIndex = 0;
            this.photographerPosition.TabStop = false;
            this.photographerPosition.Text = "Photographer Position";
            // 
            // projectGps
            // 
            this.projectGps.Controls.Add(this.vAngle);
            this.projectGps.Controls.Add(this.vAngleDiagram);
            this.projectGps.Enabled = false;
            this.projectGps.Location = new System.Drawing.Point(231, 37);
            this.projectGps.Name = "projectGps";
            this.projectGps.Size = new System.Drawing.Size(227, 117);
            this.projectGps.TabIndex = 23;
            this.projectGps.TabStop = false;
            // 
            // vAngle
            // 
            this.vAngle.Location = new System.Drawing.Point(22, 73);
            this.vAngle.Name = "vAngle";
            this.vAngle.Size = new System.Drawing.Size(54, 20);
            this.vAngle.TabIndex = 25;
            this.vAngle.Text = "30.0";
            // 
            // vAngleDiagram
            // 
            this.vAngleDiagram.Image = ((System.Drawing.Image)(resources.GetObject("vAngleDiagram.Image")));
            this.vAngleDiagram.Location = new System.Drawing.Point(6, 19);
            this.vAngleDiagram.Name = "vAngleDiagram";
            this.vAngleDiagram.Size = new System.Drawing.Size(213, 92);
            this.vAngleDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.vAngleDiagram.TabIndex = 26;
            this.vAngleDiagram.TabStop = false;
            // 
            // projectToTarget
            // 
            this.projectToTarget.AutoSize = true;
            this.projectToTarget.Location = new System.Drawing.Point(231, 19);
            this.projectToTarget.Name = "projectToTarget";
            this.projectToTarget.Size = new System.Drawing.Size(164, 17);
            this.projectToTarget.TabIndex = 22;
            this.projectToTarget.Text = "Project Coordinates to Target";
            this.projectToTarget.UseVisualStyleBackColor = true;
            this.projectToTarget.CheckedChanged += new System.EventHandler(this.setProjectGpsData);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(69, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(103, 91);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // restoreOrigHeading
            // 
            this.restoreOrigHeading.AutoSize = true;
            this.restoreOrigHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreOrigHeading.Location = new System.Drawing.Point(14, 133);
            this.restoreOrigHeading.Name = "restoreOrigHeading";
            this.restoreOrigHeading.Size = new System.Drawing.Size(142, 17);
            this.restoreOrigHeading.TabIndex = 20;
            this.restoreOrigHeading.Text = "Restore Original Heading";
            this.restoreOrigHeading.UseVisualStyleBackColor = true;
            this.restoreOrigHeading.CheckedChanged += new System.EventHandler(this.restoreOrigHeading_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 12;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // direction135
            // 
            this.direction135.AutoSize = true;
            this.direction135.Location = new System.Drawing.Point(178, 89);
            this.direction135.Name = "direction135";
            this.direction135.Size = new System.Drawing.Size(14, 13);
            this.direction135.TabIndex = 10;
            this.direction135.UseVisualStyleBackColor = true;
            this.direction135.Click += new System.EventHandler(this.direction_Click);
            // 
            // direction090
            // 
            this.direction090.AutoSize = true;
            this.direction090.Location = new System.Drawing.Point(178, 58);
            this.direction090.Name = "direction090";
            this.direction090.Size = new System.Drawing.Size(14, 13);
            this.direction090.TabIndex = 10;
            this.direction090.UseVisualStyleBackColor = true;
            this.direction090.Click += new System.EventHandler(this.direction_Click);
            // 
            // direction045
            // 
            this.direction045.AutoSize = true;
            this.direction045.Location = new System.Drawing.Point(178, 25);
            this.direction045.Name = "direction045";
            this.direction045.Size = new System.Drawing.Size(14, 13);
            this.direction045.TabIndex = 9;
            this.direction045.UseVisualStyleBackColor = true;
            this.direction045.Click += new System.EventHandler(this.direction_Click);
            // 
            // direction225
            // 
            this.direction225.AutoSize = true;
            this.direction225.Location = new System.Drawing.Point(49, 89);
            this.direction225.Name = "direction225";
            this.direction225.Size = new System.Drawing.Size(14, 13);
            this.direction225.TabIndex = 8;
            this.direction225.UseVisualStyleBackColor = true;
            this.direction225.Click += new System.EventHandler(this.direction_Click);
            // 
            // direction270
            // 
            this.direction270.AutoSize = true;
            this.direction270.Checked = true;
            this.direction270.Location = new System.Drawing.Point(49, 58);
            this.direction270.Name = "direction270";
            this.direction270.Size = new System.Drawing.Size(14, 13);
            this.direction270.TabIndex = 8;
            this.direction270.TabStop = true;
            this.direction270.UseVisualStyleBackColor = true;
            this.direction270.Click += new System.EventHandler(this.direction_Click);
            // 
            // direction315
            // 
            this.direction315.AutoSize = true;
            this.direction315.Location = new System.Drawing.Point(49, 25);
            this.direction315.Name = "direction315";
            this.direction315.Size = new System.Drawing.Size(14, 13);
            this.direction315.TabIndex = 7;
            this.direction315.UseVisualStyleBackColor = true;
            this.direction315.Click += new System.EventHandler(this.direction_Click);
            // 
            // findDirectoryBtn
            // 
            this.findDirectoryBtn.Location = new System.Drawing.Point(13, 19);
            this.findDirectoryBtn.Name = "findDirectoryBtn";
            this.findDirectoryBtn.Size = new System.Drawing.Size(104, 23);
            this.findDirectoryBtn.TabIndex = 4;
            this.findDirectoryBtn.Text = "Image Directory ...";
            this.findDirectoryBtn.UseVisualStyleBackColor = true;
            this.findDirectoryBtn.Click += new System.EventHandler(this.findDirectoryBtn_Click);
            // 
            // imageFolderBrowserDialog
            // 
            this.imageFolderBrowserDialog.Description = "Locate the directory where the images are stored that you wish to adjust the GPS " +
                "Electronic Compass Heading information.";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 74);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(447, 10);
            this.progressBar.TabIndex = 7;
            // 
            // convertImageFiles
            // 
            this.convertImageFiles.Location = new System.Drawing.Point(13, 19);
            this.convertImageFiles.Name = "convertImageFiles";
            this.convertImageFiles.Size = new System.Drawing.Size(136, 23);
            this.convertImageFiles.TabIndex = 9;
            this.convertImageFiles.Text = "Convert File Headings";
            this.convertImageFiles.UseVisualStyleBackColor = true;
            this.convertImageFiles.Click += new System.EventHandler(this.convertImageFiles_Click);
            // 
            // finishedBtn
            // 
            this.finishedBtn.Location = new System.Drawing.Point(411, 536);
            this.finishedBtn.Name = "finishedBtn";
            this.finishedBtn.Size = new System.Drawing.Size(75, 23);
            this.finishedBtn.TabIndex = 11;
            this.finishedBtn.Text = "Finished";
            this.finishedBtn.UseVisualStyleBackColor = true;
            this.finishedBtn.Click += new System.EventHandler(this.finishedBtn_Click);
            // 
            // readingFile
            // 
            this.readingFile.AutoSize = true;
            this.readingFile.Location = new System.Drawing.Point(112, 51);
            this.readingFile.Name = "readingFile";
            this.readingFile.Size = new System.Drawing.Size(70, 13);
            this.readingFile.TabIndex = 13;
            this.readingFile.Text = "1000 of 1000";
            // 
            // direction
            // 
            this.direction.AutoSize = true;
            this.direction.Location = new System.Drawing.Point(272, 25);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(25, 13);
            this.direction.TabIndex = 19;
            this.direction.Text = "000";
            this.direction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fileFailures
            // 
            this.fileFailures.AutoSize = true;
            this.fileFailures.Location = new System.Drawing.Point(419, 25);
            this.fileFailures.Name = "fileFailures";
            this.fileFailures.Size = new System.Drawing.Size(31, 13);
            this.fileFailures.TabIndex = 17;
            this.fileFailures.Text = "1000";
            // 
            // imageLocation
            // 
            this.imageLocation.Controls.Add(this.findDirectoryBtn);
            this.imageLocation.Controls.Add(this.imagePath);
            this.imageLocation.Location = new System.Drawing.Point(16, 12);
            this.imageLocation.Name = "imageLocation";
            this.imageLocation.Size = new System.Drawing.Size(470, 55);
            this.imageLocation.TabIndex = 20;
            this.imageLocation.TabStop = false;
            this.imageLocation.Text = "Image Location";
            // 
            // imagePath
            // 
            this.imagePath.AutoEllipsis = AutoEllipsis.EllipsisFormat.Path;
            this.imagePath.Location = new System.Drawing.Point(123, 24);
            this.imagePath.Name = "imagePath";
            this.imagePath.Size = new System.Drawing.Size(334, 13);
            this.imagePath.TabIndex = 3;
            this.imagePath.Text = "imageDirectoryEllipsis";
            // 
            // processImages
            // 
            this.processImages.Controls.Add(this.convertImageFiles);
            this.processImages.Controls.Add(label10);
            this.processImages.Controls.Add(this.fileFailures);
            this.processImages.Controls.Add(this.currentFile);
            this.processImages.Controls.Add(this.direction);
            this.processImages.Controls.Add(this.progressBar);
            this.processImages.Controls.Add(this.readingFile);
            this.processImages.Controls.Add(label15);
            this.processImages.Controls.Add(label18);
            this.processImages.Location = new System.Drawing.Point(16, 415);
            this.processImages.Name = "processImages";
            this.processImages.Size = new System.Drawing.Size(470, 95);
            this.processImages.TabIndex = 21;
            this.processImages.TabStop = false;
            this.processImages.Text = "Process Images";
            // 
            // currentFile
            // 
            this.currentFile.AutoEllipsis = AutoEllipsis.EllipsisFormat.Start;
            this.currentFile.Location = new System.Drawing.Point(188, 51);
            this.currentFile.Name = "currentFile";
            this.currentFile.Size = new System.Drawing.Size(269, 16);
            this.currentFile.TabIndex = 10;
            this.currentFile.Text = "currentFile";
            // 
            // trackLogLocation
            // 
            this.trackLogLocation.Controls.Add(this.trackLogFile);
            this.trackLogLocation.Location = new System.Drawing.Point(16, 73);
            this.trackLogLocation.Name = "trackLogLocation";
            this.trackLogLocation.Size = new System.Drawing.Size(470, 55);
            this.trackLogLocation.TabIndex = 23;
            this.trackLogLocation.TabStop = false;
            this.trackLogLocation.Text = "Track Log       (only used when GPS Heading is missing from image EXIF data)";
            // 
            // trackLogFile
            // 
            this.trackLogFile.AutoEllipsis = AutoEllipsis.EllipsisFormat.None;
            this.trackLogFile.Location = new System.Drawing.Point(10, 21);
            this.trackLogFile.Name = "trackLogFile";
            this.trackLogFile.Size = new System.Drawing.Size(441, 13);
            this.trackLogFile.TabIndex = 1;
            this.trackLogFile.Text = "trackLogEllipsis";
            // 
            // openTrackLogDialog
            // 
            this.openTrackLogDialog.AddExtension = false;
            this.openTrackLogDialog.FileName = "gpsTracks.csv";
            this.openTrackLogDialog.Filter = "EDICT Track Log Format| *.csv";
            this.openTrackLogDialog.Title = "Select the GPS Track Log file downloaded for the images selected";
            // 
            // cameraTime
            // 
            this.cameraTime.Controls.Add(this.TimeLocal);
            this.cameraTime.Controls.Add(this.TimeUTC);
            this.cameraTime.Controls.Add(this.deltaSeconds);
            this.cameraTime.Controls.Add(this.cameraTimeBtn);
            this.cameraTime.Location = new System.Drawing.Point(15, 134);
            this.cameraTime.Name = "cameraTime";
            this.cameraTime.Size = new System.Drawing.Size(471, 49);
            this.cameraTime.TabIndex = 24;
            this.cameraTime.TabStop = false;
            this.cameraTime.Text = "Camera Time    (only used when GPS data is missing from image EXIF data)";
            // 
            // TimeLocal
            // 
            this.TimeLocal.AutoSize = true;
            this.TimeLocal.Location = new System.Drawing.Point(66, 20);
            this.TimeLocal.Name = "TimeLocal";
            this.TimeLocal.Size = new System.Drawing.Size(51, 17);
            this.TimeLocal.TabIndex = 3;
            this.TimeLocal.Text = "Local";
            this.TimeLocal.UseVisualStyleBackColor = true;
            this.TimeLocal.CheckedChanged += new System.EventHandler(this.TimeUTC_CheckedChanged);
            // 
            // TimeUTC
            // 
            this.TimeUTC.AutoSize = true;
            this.TimeUTC.Checked = true;
            this.TimeUTC.Location = new System.Drawing.Point(11, 20);
            this.TimeUTC.Name = "TimeUTC";
            this.TimeUTC.Size = new System.Drawing.Size(47, 17);
            this.TimeUTC.TabIndex = 2;
            this.TimeUTC.TabStop = true;
            this.TimeUTC.Text = "UTC";
            this.TimeUTC.UseVisualStyleBackColor = true;
            this.TimeUTC.CheckedChanged += new System.EventHandler(this.TimeUTC_CheckedChanged);
            // 
            // deltaSeconds
            // 
            this.deltaSeconds.AutoSize = true;
            this.deltaSeconds.Location = new System.Drawing.Point(265, 22);
            this.deltaSeconds.Name = "deltaSeconds";
            this.deltaSeconds.Size = new System.Drawing.Size(72, 13);
            this.deltaSeconds.TabIndex = 1;
            this.deltaSeconds.Text = "deltaSeconds";
            // 
            // cameraTimeBtn
            // 
            this.cameraTimeBtn.Location = new System.Drawing.Point(123, 17);
            this.cameraTimeBtn.Name = "cameraTimeBtn";
            this.cameraTimeBtn.Size = new System.Drawing.Size(136, 23);
            this.cameraTimeBtn.TabIndex = 0;
            this.cameraTimeBtn.Text = "Synchronize Camera...";
            this.cameraTimeBtn.UseVisualStyleBackColor = true;
            this.cameraTimeBtn.Click += new System.EventHandler(this.cameraTimeBtn_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(298, 533);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(84, 13);
            this.labelVersion.TabIndex = 25;
            this.labelVersion.Text = "Version: 1.4.123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 524);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 22);
            this.label1.TabIndex = 26;
            this.label1.Text = "NOT FOR COMMERCIAL USE";
            // 
            // reverseGeocode
            // 
            this.reverseGeocode.Controls.Add(this.useYahooMaps);
            this.reverseGeocode.Controls.Add(this.useGoogleMaps);
            this.reverseGeocode.Controls.Add(this.doReverseGeocode);
            this.reverseGeocode.Location = new System.Drawing.Point(16, 359);
            this.reverseGeocode.Name = "reverseGeocode";
            this.reverseGeocode.Size = new System.Drawing.Size(469, 49);
            this.reverseGeocode.TabIndex = 27;
            this.reverseGeocode.TabStop = false;
            this.reverseGeocode.Text = "Reverse Geocode";
            // 
            // useYahooMaps
            // 
            this.useYahooMaps.AutoSize = true;
            this.useYahooMaps.Location = new System.Drawing.Point(285, 20);
            this.useYahooMaps.Name = "useYahooMaps";
            this.useYahooMaps.Size = new System.Drawing.Size(85, 17);
            this.useYahooMaps.TabIndex = 2;
            this.useYahooMaps.Text = "Yahoo Maps";
            this.useYahooMaps.UseVisualStyleBackColor = true;
            // 
            // useGoogleMaps
            // 
            this.useGoogleMaps.AutoSize = true;
            this.useGoogleMaps.Checked = true;
            this.useGoogleMaps.Location = new System.Drawing.Point(191, 20);
            this.useGoogleMaps.Name = "useGoogleMaps";
            this.useGoogleMaps.Size = new System.Drawing.Size(88, 17);
            this.useGoogleMaps.TabIndex = 1;
            this.useGoogleMaps.TabStop = true;
            this.useGoogleMaps.Text = "Google Maps";
            this.useGoogleMaps.UseVisualStyleBackColor = true;
            // 
            // doReverseGeocode
            // 
            this.doReverseGeocode.AutoSize = true;
            this.doReverseGeocode.Checked = true;
            this.doReverseGeocode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.doReverseGeocode.Location = new System.Drawing.Point(13, 20);
            this.doReverseGeocode.Name = "doReverseGeocode";
            this.doReverseGeocode.Size = new System.Drawing.Size(141, 17);
            this.doReverseGeocode.TabIndex = 0;
            this.doReverseGeocode.Text = "Reverse Geocode using";
            this.doReverseGeocode.UseVisualStyleBackColor = true;
            this.doReverseGeocode.CheckedChanged += new System.EventHandler(this.doReverseGeocode_changed);
            // 
            // Aircraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 576);
            this.Controls.Add(this.reverseGeocode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.cameraTime);
            this.Controls.Add(this.trackLogLocation);
            this.Controls.Add(this.processImages);
            this.Controls.Add(this.imageLocation);
            this.Controls.Add(this.finishedBtn);
            this.Controls.Add(label11);
            this.Controls.Add(this.photographerPosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Aircraft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDICT - Exif Direction Image Correction Tool";
            this.photographerPosition.ResumeLayout(false);
            this.photographerPosition.PerformLayout();
            this.projectGps.ResumeLayout(false);
            this.projectGps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vAngleDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.imageLocation.ResumeLayout(false);
            this.processImages.ResumeLayout(false);
            this.processImages.PerformLayout();
            this.trackLogLocation.ResumeLayout(false);
            this.cameraTime.ResumeLayout(false);
            this.cameraTime.PerformLayout();
            this.reverseGeocode.ResumeLayout(false);
            this.reverseGeocode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox photographerPosition;
        private AutoEllipsis.LabelEllipsis imagePath;
        private System.Windows.Forms.Button findDirectoryBtn;
        private System.Windows.Forms.FolderBrowserDialog imageFolderBrowserDialog;
        private System.Windows.Forms.RadioButton direction045;
        private System.Windows.Forms.RadioButton direction225;
        private System.Windows.Forms.RadioButton direction270;
        private System.Windows.Forms.RadioButton direction315;
        private System.Windows.Forms.RadioButton direction135;
        private System.Windows.Forms.RadioButton direction090;
        private System.Windows.Forms.Button convertImageFiles;
        private AutoEllipsis.LabelEllipsis currentFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button finishedBtn;
        private System.Windows.Forms.Label readingFile;
        private System.Windows.Forms.Label fileFailures;
        private System.Windows.Forms.Label direction;
        private System.Windows.Forms.CheckBox restoreOrigHeading;
        private System.Windows.Forms.GroupBox imageLocation;
        private System.Windows.Forms.GroupBox processImages;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox trackLogLocation;
        private AutoEllipsis.LabelEllipsis trackLogFile;
        private System.Windows.Forms.OpenFileDialog openTrackLogDialog;
        private System.Windows.Forms.Label heading315Lbl;
        private System.Windows.Forms.GroupBox cameraTime;
        private System.Windows.Forms.Button cameraTimeBtn;
        private System.Windows.Forms.Label deltaSeconds;
        private System.Windows.Forms.RadioButton TimeLocal;
        private System.Windows.Forms.RadioButton TimeUTC;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox reverseGeocode;
        private System.Windows.Forms.CheckBox doReverseGeocode;
        private System.Windows.Forms.RadioButton useYahooMaps;
        private System.Windows.Forms.RadioButton useGoogleMaps;
        private System.Windows.Forms.GroupBox projectGps;
        private System.Windows.Forms.TextBox vAngle;
        private System.Windows.Forms.CheckBox projectToTarget;
        private System.Windows.Forms.PictureBox vAngleDiagram;
    }
}