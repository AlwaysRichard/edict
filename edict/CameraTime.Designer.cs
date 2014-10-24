namespace edict
{
    partial class CameraTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraTime));
            this.openGpsImageDlg = new System.Windows.Forms.OpenFileDialog();
            this.loadImageBtn = new System.Windows.Forms.Button();
            this.gpsImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hour = new System.Windows.Forms.NumericUpDown();
            this.minute = new System.Windows.Forms.NumericUpDown();
            this.seconds = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.day = new System.Windows.Forms.NumericUpDown();
            this.month = new System.Windows.Forms.ComboBox();
            this.year = new System.Windows.Forms.NumericUpDown();
            this.offsetSeconds = new System.Windows.Forms.Label();
            this.done = new System.Windows.Forms.Button();
            this.cancleBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cameraTimeLbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImageLocal = new System.Windows.Forms.RadioButton();
            this.ImageUTC = new System.Windows.Forms.RadioButton();
            this.GpsUTC = new System.Windows.Forms.RadioButton();
            this.GpsLocal = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gpsImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.year)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGpsImageDlg
            // 
            this.openGpsImageDlg.FileName = "GpsImage.jpg";
            this.openGpsImageDlg.Filter = "JPEG Files|*.jpg";
            // 
            // loadImageBtn
            // 
            this.loadImageBtn.Location = new System.Drawing.Point(13, 13);
            this.loadImageBtn.Name = "loadImageBtn";
            this.loadImageBtn.Size = new System.Drawing.Size(149, 23);
            this.loadImageBtn.TabIndex = 0;
            this.loadImageBtn.Text = "Load GPS Clock Image...";
            this.loadImageBtn.UseVisualStyleBackColor = true;
            this.loadImageBtn.Click += new System.EventHandler(this.loadImageBtn_Click);
            // 
            // gpsImage
            // 
            this.gpsImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gpsImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gpsImage.BackgroundImage")));
            this.gpsImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gpsImage.Location = new System.Drawing.Point(13, 43);
            this.gpsImage.Name = "gpsImage";
            this.gpsImage.Size = new System.Drawing.Size(436, 341);
            this.gpsImage.TabIndex = 1;
            this.gpsImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Time:";
            // 
            // hour
            // 
            this.hour.Location = new System.Drawing.Point(47, 18);
            this.hour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.hour.Name = "hour";
            this.hour.Size = new System.Drawing.Size(36, 20);
            this.hour.TabIndex = 3;
            this.hour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.hour.ValueChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // minute
            // 
            this.minute.Location = new System.Drawing.Point(86, 18);
            this.minute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minute.Name = "minute";
            this.minute.Size = new System.Drawing.Size(36, 20);
            this.minute.TabIndex = 4;
            this.minute.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minute.ValueChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(126, 18);
            this.seconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(36, 20);
            this.seconds.TabIndex = 5;
            this.seconds.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.seconds.ValueChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date:";
            // 
            // day
            // 
            this.day.Location = new System.Drawing.Point(47, 44);
            this.day.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(36, 20);
            this.day.TabIndex = 7;
            this.day.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.day.ValueChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // month
            // 
            this.month.FormattingEnabled = true;
            this.month.Items.AddRange(new object[] {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"});
            this.month.Location = new System.Drawing.Point(86, 44);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(49, 21);
            this.month.TabIndex = 8;
            this.month.SelectedIndexChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // year
            // 
            this.year.Location = new System.Drawing.Point(139, 44);
            this.year.Maximum = new decimal(new int[] {
            2099,
            0,
            0,
            0});
            this.year.Minimum = new decimal(new int[] {
            2009,
            0,
            0,
            0});
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(53, 20);
            this.year.TabIndex = 9;
            this.year.Value = new decimal(new int[] {
            2009,
            0,
            0,
            0});
            this.year.ValueChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // offsetSeconds
            // 
            this.offsetSeconds.AutoSize = true;
            this.offsetSeconds.Location = new System.Drawing.Point(60, 73);
            this.offsetSeconds.Name = "offsetSeconds";
            this.offsetSeconds.Size = new System.Drawing.Size(49, 13);
            this.offsetSeconds.TabIndex = 11;
            this.offsetSeconds.Text = "Seconds";
            // 
            // done
            // 
            this.done.Location = new System.Drawing.Point(373, 504);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(75, 23);
            this.done.TabIndex = 12;
            this.done.Text = "Accept";
            this.done.UseVisualStyleBackColor = true;
            this.done.Click += new System.EventHandler(this.done_Click);
            // 
            // cancleBtn
            // 
            this.cancleBtn.Location = new System.Drawing.Point(292, 503);
            this.cancleBtn.Name = "cancleBtn";
            this.cancleBtn.Size = new System.Drawing.Size(75, 23);
            this.cancleBtn.TabIndex = 13;
            this.cancleBtn.Text = "Cancle";
            this.cancleBtn.UseVisualStyleBackColor = true;
            this.cancleBtn.Click += new System.EventHandler(this.cancleBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Delta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Time:";
            // 
            // cameraTimeLbl
            // 
            this.cameraTimeLbl.AutoSize = true;
            this.cameraTimeLbl.Location = new System.Drawing.Point(59, 20);
            this.cameraTimeLbl.Name = "cameraTimeLbl";
            this.cameraTimeLbl.Size = new System.Drawing.Size(112, 13);
            this.cameraTimeLbl.TabIndex = 16;
            this.cameraTimeLbl.Text = "hh:mm:ss dd-mon-yyyy";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GpsLocal);
            this.groupBox1.Controls.Add(this.GpsUTC);
            this.groupBox1.Controls.Add(this.hour);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.minute);
            this.groupBox1.Controls.Add(this.seconds);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.day);
            this.groupBox1.Controls.Add(this.month);
            this.groupBox1.Controls.Add(this.year);
            this.groupBox1.Location = new System.Drawing.Point(13, 392);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 97);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time in GPS image";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ImageLocal);
            this.groupBox2.Controls.Add(this.ImageUTC);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.offsetSeconds);
            this.groupBox2.Controls.Add(this.cameraTimeLbl);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(223, 392);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 97);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camera Time";
            // 
            // ImageLocal
            // 
            this.ImageLocal.AutoSize = true;
            this.ImageLocal.Location = new System.Drawing.Point(115, 42);
            this.ImageLocal.Name = "ImageLocal";
            this.ImageLocal.Size = new System.Drawing.Size(51, 17);
            this.ImageLocal.TabIndex = 18;
            this.ImageLocal.Text = "Local";
            this.ImageLocal.UseVisualStyleBackColor = true;
            this.ImageLocal.CheckedChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // ImageUTC
            // 
            this.ImageUTC.AutoSize = true;
            this.ImageUTC.Checked = true;
            this.ImageUTC.Location = new System.Drawing.Point(62, 42);
            this.ImageUTC.Name = "ImageUTC";
            this.ImageUTC.Size = new System.Drawing.Size(47, 17);
            this.ImageUTC.TabIndex = 17;
            this.ImageUTC.TabStop = true;
            this.ImageUTC.Text = "UTC";
            this.ImageUTC.UseVisualStyleBackColor = true;
            this.ImageUTC.CheckedChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // GpsUTC
            // 
            this.GpsUTC.AutoSize = true;
            this.GpsUTC.Location = new System.Drawing.Point(47, 71);
            this.GpsUTC.Name = "GpsUTC";
            this.GpsUTC.Size = new System.Drawing.Size(47, 17);
            this.GpsUTC.TabIndex = 10;
            this.GpsUTC.TabStop = true;
            this.GpsUTC.Text = "UTC";
            this.GpsUTC.UseVisualStyleBackColor = true;
            this.GpsUTC.CheckedChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // GpsLocal
            // 
            this.GpsLocal.AutoSize = true;
            this.GpsLocal.Location = new System.Drawing.Point(100, 71);
            this.GpsLocal.Name = "GpsLocal";
            this.GpsLocal.Size = new System.Drawing.Size(51, 17);
            this.GpsLocal.TabIndex = 11;
            this.GpsLocal.TabStop = true;
            this.GpsLocal.Text = "Local";
            this.GpsLocal.UseVisualStyleBackColor = true;
            this.GpsLocal.CheckedChanged += new System.EventHandler(this.calculateDeltaTime_Click);
            // 
            // CameraTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 539);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancleBtn);
            this.Controls.Add(this.done);
            this.Controls.Add(this.gpsImage);
            this.Controls.Add(this.loadImageBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraTime";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjust Camera Time";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gpsImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.year)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openGpsImageDlg;
        private System.Windows.Forms.Button loadImageBtn;
        private System.Windows.Forms.PictureBox gpsImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown hour;
        private System.Windows.Forms.NumericUpDown minute;
        private System.Windows.Forms.NumericUpDown seconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown day;
        private System.Windows.Forms.ComboBox month;
        private System.Windows.Forms.NumericUpDown year;
        private System.Windows.Forms.Label offsetSeconds;
        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button cancleBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cameraTimeLbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ImageUTC;
        private System.Windows.Forms.RadioButton ImageLocal;
        private System.Windows.Forms.RadioButton GpsLocal;
        private System.Windows.Forms.RadioButton GpsUTC;
    }
}