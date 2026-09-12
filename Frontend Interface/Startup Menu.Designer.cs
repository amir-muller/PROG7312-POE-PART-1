namespace Frontend_Interface
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnRealTimeHistory = new System.Windows.Forms.Button();
            this.btnSensorDataTelemotry = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.btnRealTimeHistory);
            this.panel1.Controls.Add(this.btnSensorDataTelemotry);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Location = new System.Drawing.Point(13, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 326);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(367, 156);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 86);
            this.button3.TabIndex = 5;
            this.button3.Text = "Network Topology and Mesh Routing";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnRealTimeHistory
            // 
            this.btnRealTimeHistory.Enabled = false;
            this.btnRealTimeHistory.Location = new System.Drawing.Point(228, 156);
            this.btnRealTimeHistory.Name = "btnRealTimeHistory";
            this.btnRealTimeHistory.Size = new System.Drawing.Size(112, 86);
            this.btnRealTimeHistory.TabIndex = 4;
            this.btnRealTimeHistory.Text = "Real-Time Command Stream ans History";
            this.btnRealTimeHistory.UseVisualStyleBackColor = true;
            // 
            // btnSensorDataTelemotry
            // 
            this.btnSensorDataTelemotry.Location = new System.Drawing.Point(90, 156);
            this.btnSensorDataTelemotry.Name = "btnSensorDataTelemotry";
            this.btnSensorDataTelemotry.Size = new System.Drawing.Size(112, 86);
            this.btnSensorDataTelemotry.TabIndex = 3;
            this.btnSensorDataTelemotry.Text = "Sensor Data Integration and Telemetry";
            this.btnSensorDataTelemotry.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Arial Narrow", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(80, 39);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(399, 55);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Welcome to Smart-X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 354);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Smart-X: Startup Menu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnRealTimeHistory;
        private System.Windows.Forms.Button btnSensorDataTelemotry;
    }
}

