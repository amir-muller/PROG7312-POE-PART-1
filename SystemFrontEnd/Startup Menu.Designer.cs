namespace SystemFrontEnd
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            btnNetworkTopology = new Button();
            btnRealtimeCommandandHistory = new Button();
            btnSensorData = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnNetworkTopology);
            panel1.Controls.Add(btnRealtimeCommandandHistory);
            panel1.Controls.Add(btnSensorData);
            panel1.Location = new Point(19, 9);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(543, 255);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Narrow", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 38);
            label1.Name = "label1";
            label1.Size = new Size(407, 57);
            label1.TabIndex = 4;
            label1.Text = "Welcome to Smart-X";
            // 
            // btnNetworkTopology
            // 
            btnNetworkTopology.Enabled = false;
            btnNetworkTopology.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNetworkTopology.Location = new Point(342, 124);
            btnNetworkTopology.Margin = new Padding(3, 2, 3, 2);
            btnNetworkTopology.Name = "btnNetworkTopology";
            btnNetworkTopology.Size = new Size(142, 77);
            btnNetworkTopology.TabIndex = 3;
            btnNetworkTopology.Text = "Network Topology and Mesh Routing";
            btnNetworkTopology.UseVisualStyleBackColor = true;
            // 
            // btnRealtimeCommandandHistory
            // 
            btnRealtimeCommandandHistory.Enabled = false;
            btnRealtimeCommandandHistory.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRealtimeCommandandHistory.Location = new Point(195, 124);
            btnRealtimeCommandandHistory.Margin = new Padding(3, 2, 3, 2);
            btnRealtimeCommandandHistory.Name = "btnRealtimeCommandandHistory";
            btnRealtimeCommandandHistory.Size = new Size(142, 77);
            btnRealtimeCommandandHistory.TabIndex = 2;
            btnRealtimeCommandandHistory.Text = "Real-Time Command Stream and history";
            btnRealtimeCommandandHistory.UseVisualStyleBackColor = true;
            btnRealtimeCommandandHistory.Click += btnRealtimeCommandandHistory_Click;
            // 
            // btnSensorData
            // 
            btnSensorData.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSensorData.Location = new Point(40, 124);
            btnSensorData.Margin = new Padding(3, 2, 3, 2);
            btnSensorData.Name = "btnSensorData";
            btnSensorData.Size = new Size(142, 77);
            btnSensorData.TabIndex = 1;
            btnSensorData.Text = "Sensor Data Ingestion and Telemetry";
            btnSensorData.UseVisualStyleBackColor = true;
            btnSensorData.Click += btnSensorData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 276);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Smart-X - Startup menu";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Button btnNetworkTopology;
        private Button btnRealtimeCommandandHistory;
        private Button btnSensorData;
    }
}
