namespace SystemFrontEnd
{
    partial class frmNetworkTopology
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
            panel1 = new Panel();
            panel2 = new Panel();
            lblNodeandEdge = new Label();
            lblNodeName = new Label();
            label2 = new Label();
            label3 = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ScrollBar;
            panel1.Location = new Point(12, 85);
            panel1.Name = "panel1";
            panel1.Size = new Size(593, 540);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblNodeName);
            panel2.Controls.Add(lblNodeandEdge);
            panel2.Location = new Point(712, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(353, 315);
            panel2.TabIndex = 1;
            // 
            // lblNodeandEdge
            // 
            lblNodeandEdge.AutoSize = true;
            lblNodeandEdge.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNodeandEdge.Location = new Point(13, 14);
            lblNodeandEdge.Name = "lblNodeandEdge";
            lblNodeandEdge.Size = new Size(226, 25);
            lblNodeandEdge.TabIndex = 0;
            lblNodeandEdge.Text = "Selected Node and Edges";
            // 
            // lblNodeName
            // 
            lblNodeName.AutoSize = true;
            lblNodeName.Location = new Point(13, 48);
            lblNodeName.Name = "lblNodeName";
            lblNodeName.Size = new Size(77, 15);
            lblNodeName.TabIndex = 2;
            lblNodeName.Text = "Node Name: ";
            lblNodeName.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 73);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 3;
            label2.Text = "Node Mac: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 99);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 4;
            label3.Text = "Node Type: ";
            label3.Click += label3_Click;
            // 
            // frmNetworkTopology
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 640);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmNetworkTopology";
            Text = "frmNetworkTopology";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblNodeandEdge;
        private Label label2;
        private Label lblNodeName;
        private Label label3;
    }
}