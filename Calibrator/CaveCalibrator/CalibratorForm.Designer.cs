namespace CaveCalibrator
{
    partial class CalibratorForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseApp = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Dragger = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectedDisplay = new System.Windows.Forms.Label();
            this.SelectedCorner = new System.Windows.Forms.Label();
            this.TopLeft = new System.Windows.Forms.Button();
            this.TopRight = new System.Windows.Forms.Button();
            this.BottomRight = new System.Windows.Forms.Button();
            this.BottomLeft = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.ShowQuadsCheckbox = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.DisplayNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.OthersOffCheckbox = new System.Windows.Forms.CheckBox();
            this.CalibrationQuad = new System.Windows.Forms.PictureBox();
            this.JoyConRight = new System.Windows.Forms.PictureBox();
            this.JoyConLeft = new System.Windows.Forms.PictureBox();
            this.Dragger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationQuad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoyConRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoyConLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseApp
            // 
            this.CloseApp.BackColor = System.Drawing.Color.IndianRed;
            this.CloseApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseApp.ForeColor = System.Drawing.Color.White;
            this.CloseApp.Location = new System.Drawing.Point(752, 2);
            this.CloseApp.Name = "CloseApp";
            this.CloseApp.Size = new System.Drawing.Size(45, 34);
            this.CloseApp.TabIndex = 1;
            this.CloseApp.Text = "🗙";
            this.CloseApp.UseVisualStyleBackColor = false;
            this.CloseApp.Click += new System.EventHandler(this.CloseApp_Click);
            // 
            // Dragger
            // 
            this.Dragger.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Dragger.Controls.Add(this.SearchButton);
            this.Dragger.Controls.Add(this.HelpButton);
            this.Dragger.Controls.Add(this.BrowseButton);
            this.Dragger.Controls.Add(this.SaveButton);
            this.Dragger.Controls.Add(this.label1);
            this.Dragger.Location = new System.Drawing.Point(2, 2);
            this.Dragger.Name = "Dragger";
            this.Dragger.Size = new System.Drawing.Size(744, 34);
            this.Dragger.TabIndex = 2;
            this.Dragger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnDraggerMouseDown);
            this.Dragger.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDraggerMouseMove);
            this.Dragger.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDraggerMouseUp);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.White;
            this.SearchButton.Location = new System.Drawing.Point(246, 3);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(33, 28);
            this.SearchButton.TabIndex = 18;
            this.SearchButton.Text = "🔍";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HelpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton.ForeColor = System.Drawing.Color.White;
            this.HelpButton.Location = new System.Drawing.Point(286, 3);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(33, 28);
            this.HelpButton.TabIndex = 17;
            this.HelpButton.Text = "?";
            this.HelpButton.UseVisualStyleBackColor = false;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(168, 3);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(33, 28);
            this.BrowseButton.TabIndex = 16;
            this.BrowseButton.Text = "📁";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click_1);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Location = new System.Drawing.Point(207, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(33, 28);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "💾";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Cave Calibrator";
            // 
            // SelectedDisplay
            // 
            this.SelectedDisplay.AutoSize = true;
            this.SelectedDisplay.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedDisplay.Location = new System.Drawing.Point(204, 79);
            this.SelectedDisplay.Name = "SelectedDisplay";
            this.SelectedDisplay.Size = new System.Drawing.Size(382, 45);
            this.SelectedDisplay.TabIndex = 5;
            this.SelectedDisplay.Text = "Front Beamer Right Eye";
            this.SelectedDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectedCorner
            // 
            this.SelectedCorner.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedCorner.Location = new System.Drawing.Point(281, 372);
            this.SelectedCorner.Name = "SelectedCorner";
            this.SelectedCorner.Size = new System.Drawing.Size(233, 42);
            this.SelectedCorner.TabIndex = 6;
            this.SelectedCorner.Text = "Top Left";
            this.SelectedCorner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopLeft
            // 
            this.TopLeft.BackColor = System.Drawing.Color.Silver;
            this.TopLeft.ForeColor = System.Drawing.Color.AliceBlue;
            this.TopLeft.Location = new System.Drawing.Point(281, 137);
            this.TopLeft.Name = "TopLeft";
            this.TopLeft.Size = new System.Drawing.Size(40, 40);
            this.TopLeft.TabIndex = 13;
            this.TopLeft.UseVisualStyleBackColor = false;
            this.TopLeft.Click += new System.EventHandler(this.TopLeft_Click);
            // 
            // TopRight
            // 
            this.TopRight.BackColor = System.Drawing.Color.DimGray;
            this.TopRight.ForeColor = System.Drawing.Color.AliceBlue;
            this.TopRight.Location = new System.Drawing.Point(474, 137);
            this.TopRight.Name = "TopRight";
            this.TopRight.Size = new System.Drawing.Size(40, 40);
            this.TopRight.TabIndex = 14;
            this.TopRight.UseVisualStyleBackColor = false;
            this.TopRight.Click += new System.EventHandler(this.TopRight_Click);
            // 
            // BottomRight
            // 
            this.BottomRight.BackColor = System.Drawing.Color.Silver;
            this.BottomRight.ForeColor = System.Drawing.Color.AliceBlue;
            this.BottomRight.Location = new System.Drawing.Point(474, 329);
            this.BottomRight.Name = "BottomRight";
            this.BottomRight.Size = new System.Drawing.Size(40, 40);
            this.BottomRight.TabIndex = 10;
            this.BottomRight.UseVisualStyleBackColor = false;
            this.BottomRight.Click += new System.EventHandler(this.BottomRight_Click);
            // 
            // BottomLeft
            // 
            this.BottomLeft.BackColor = System.Drawing.Color.DimGray;
            this.BottomLeft.ForeColor = System.Drawing.Color.AliceBlue;
            this.BottomLeft.Location = new System.Drawing.Point(281, 329);
            this.BottomLeft.Name = "BottomLeft";
            this.BottomLeft.Size = new System.Drawing.Size(40, 40);
            this.BottomLeft.TabIndex = 12;
            this.BottomLeft.UseVisualStyleBackColor = false;
            this.BottomLeft.Click += new System.EventHandler(this.BottomLeft_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.PrevButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrevButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.PrevButton.Location = new System.Drawing.Point(228, 213);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(47, 79);
            this.PrevButton.TabIndex = 9;
            this.PrevButton.Text = "🡸";
            this.PrevButton.UseVisualStyleBackColor = false;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.NextButton.Location = new System.Drawing.Point(520, 213);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(47, 80);
            this.NextButton.TabIndex = 11;
            this.NextButton.Text = "🡺";
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // ShowQuadsCheckbox
            // 
            this.ShowQuadsCheckbox.AutoSize = true;
            this.ShowQuadsCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowQuadsCheckbox.Location = new System.Drawing.Point(281, 52);
            this.ShowQuadsCheckbox.Name = "ShowQuadsCheckbox";
            this.ShowQuadsCheckbox.Size = new System.Drawing.Size(119, 24);
            this.ShowQuadsCheckbox.TabIndex = 19;
            this.ShowQuadsCheckbox.Text = "Show Quads";
            this.ShowQuadsCheckbox.UseVisualStyleBackColor = true;
            this.ShowQuadsCheckbox.CheckedChanged += new System.EventHandler(this.ShowQuadsCheckbox_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "calibration.json";
            this.openFileDialog.Filter = "Json (*.json)|*.json";
            this.openFileDialog.InitialDirectory = "C:\\HTWCave\\UnityCave";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "calibration.json";
            this.saveFileDialog.Filter = "Json (*.json)|*.json";
            this.saveFileDialog.InitialDirectory = "C:\\HTWCave\\UnityCave";
            // 
            // DisplayNumber
            // 
            this.DisplayNumber.Location = new System.Drawing.Point(394, 428);
            this.DisplayNumber.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.DisplayNumber.Name = "DisplayNumber";
            this.DisplayNumber.Size = new System.Drawing.Size(50, 20);
            this.DisplayNumber.TabIndex = 21;
            this.DisplayNumber.ValueChanged += new System.EventHandler(this.DisplayNumber_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Display:";
            // 
            // OthersOffCheckbox
            // 
            this.OthersOffCheckbox.AutoSize = true;
            this.OthersOffCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OthersOffCheckbox.Location = new System.Drawing.Point(406, 52);
            this.OthersOffCheckbox.Name = "OthersOffCheckbox";
            this.OthersOffCheckbox.Size = new System.Drawing.Size(102, 24);
            this.OthersOffCheckbox.TabIndex = 20;
            this.OthersOffCheckbox.Text = "Others Off";
            this.OthersOffCheckbox.UseVisualStyleBackColor = true;
            this.OthersOffCheckbox.CheckedChanged += new System.EventHandler(this.OthersOffCheckbox_CheckedChanged);
            // 
            // CalibrationQuad
            // 
            this.CalibrationQuad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalibrationQuad.Image = global::CaveCalibrator.Properties.Resources.calibration_quad;
            this.CalibrationQuad.Location = new System.Drawing.Point(281, 137);
            this.CalibrationQuad.Name = "CalibrationQuad";
            this.CalibrationQuad.Size = new System.Drawing.Size(233, 232);
            this.CalibrationQuad.TabIndex = 8;
            this.CalibrationQuad.TabStop = false;
            this.CalibrationQuad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CalibrationQuad_MouseDown);
            this.CalibrationQuad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CalibrationQuad_MouseMove);
            this.CalibrationQuad.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CalibrationQuad_MouseUp);
            // 
            // JoyConRight
            // 
            this.JoyConRight.BackColor = System.Drawing.Color.Crimson;
            this.JoyConRight.Image = global::CaveCalibrator.Properties.Resources.joycon_r;
            this.JoyConRight.Location = new System.Drawing.Point(617, 42);
            this.JoyConRight.Name = "JoyConRight";
            this.JoyConRight.Size = new System.Drawing.Size(150, 396);
            this.JoyConRight.TabIndex = 4;
            this.JoyConRight.TabStop = false;
            // 
            // JoyConLeft
            // 
            this.JoyConLeft.BackColor = System.Drawing.Color.Crimson;
            this.JoyConLeft.Image = global::CaveCalibrator.Properties.Resources.joycon_l;
            this.JoyConLeft.Location = new System.Drawing.Point(29, 42);
            this.JoyConLeft.Name = "JoyConLeft";
            this.JoyConLeft.Size = new System.Drawing.Size(149, 396);
            this.JoyConLeft.TabIndex = 3;
            this.JoyConLeft.TabStop = false;
            // 
            // CalibratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DisplayNumber);
            this.Controls.Add(this.OthersOffCheckbox);
            this.Controls.Add(this.ShowQuadsCheckbox);
            this.Controls.Add(this.TopRight);
            this.Controls.Add(this.TopLeft);
            this.Controls.Add(this.BottomLeft);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.BottomRight);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.CalibrationQuad);
            this.Controls.Add(this.SelectedCorner);
            this.Controls.Add(this.SelectedDisplay);
            this.Controls.Add(this.JoyConRight);
            this.Controls.Add(this.JoyConLeft);
            this.Controls.Add(this.Dragger);
            this.Controls.Add(this.CloseApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalibratorForm";
            this.Text = "Cave Calibrator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalibratorForm_FormClosed);
            this.Dragger.ResumeLayout(false);
            this.Dragger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationQuad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoyConRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoyConLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CloseApp;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel Dragger;
        private System.Windows.Forms.PictureBox JoyConLeft;
        private System.Windows.Forms.PictureBox JoyConRight;
        private System.Windows.Forms.Label SelectedDisplay;
        private System.Windows.Forms.Label SelectedCorner;
        private System.Windows.Forms.PictureBox CalibrationQuad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button TopLeft;
        private System.Windows.Forms.Button TopRight;
        private System.Windows.Forms.Button BottomRight;
        private System.Windows.Forms.Button BottomLeft;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.CheckBox ShowQuadsCheckbox;
        private System.Windows.Forms.CheckBox OthersOffCheckbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.NumericUpDown DisplayNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button SearchButton;
    }
}

