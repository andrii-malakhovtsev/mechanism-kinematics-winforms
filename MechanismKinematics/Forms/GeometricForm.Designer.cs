namespace MechanismKinematics
{
    partial class GeometricForm
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
            this.LabelFirstWheelRadius = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownFirstWheelRadius = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondWheelRadius = new System.Windows.Forms.NumericUpDown();
            this.LabelSecondWheelRadius = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFirstWheelRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondWheelRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelFirstWheelRadius
            // 
            this.LabelFirstWheelRadius.AutoSize = true;
            this.LabelFirstWheelRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelFirstWheelRadius.Location = new System.Drawing.Point(12, 25);
            this.LabelFirstWheelRadius.Name = "LabelFirstWheelRadius";
            this.LabelFirstWheelRadius.Size = new System.Drawing.Size(132, 16);
            this.LabelFirstWheelRadius.TabIndex = 0;
            this.LabelFirstWheelRadius.Text = "First wheel radius:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(325, 15);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(92, 37);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(325, 63);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 34);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownFirstWheelRadius
            // 
            this.numericUpDownFirstWheelRadius.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownFirstWheelRadius.Location = new System.Drawing.Point(178, 25);
            this.numericUpDownFirstWheelRadius.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDownFirstWheelRadius.Name = "numericUpDownFirstWheelRadius";
            this.numericUpDownFirstWheelRadius.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownFirstWheelRadius.TabIndex = 5;
            this.numericUpDownFirstWheelRadius.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // numericUpDownSecondWheelRadius
            // 
            this.numericUpDownSecondWheelRadius.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownSecondWheelRadius.Location = new System.Drawing.Point(178, 63);
            this.numericUpDownSecondWheelRadius.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownSecondWheelRadius.Minimum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.numericUpDownSecondWheelRadius.Name = "numericUpDownSecondWheelRadius";
            this.numericUpDownSecondWheelRadius.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownSecondWheelRadius.TabIndex = 6;
            this.numericUpDownSecondWheelRadius.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            // 
            // LabelSecondWheelRadius
            // 
            this.LabelSecondWheelRadius.AutoSize = true;
            this.LabelSecondWheelRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelSecondWheelRadius.Location = new System.Drawing.Point(13, 63);
            this.LabelSecondWheelRadius.Name = "LabelSecondWheelRadius";
            this.LabelSecondWheelRadius.Size = new System.Drawing.Size(155, 16);
            this.LabelSecondWheelRadius.TabIndex = 7;
            this.LabelSecondWheelRadius.Text = "Second wheel radius:";
            // 
            // GeometricForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(429, 110);
            this.Controls.Add(this.LabelSecondWheelRadius);
            this.Controls.Add(this.numericUpDownSecondWheelRadius);
            this.Controls.Add(this.numericUpDownFirstWheelRadius);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.LabelFirstWheelRadius);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeometricForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mechanism geometry measurements";
            this.Load += new System.EventHandler(this.GeometricForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFirstWheelRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondWheelRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelFirstWheelRadius;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownFirstWheelRadius;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondWheelRadius;
        private System.Windows.Forms.Label LabelSecondWheelRadius;
    }
}