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
            redOutput = new RichTextBox();
            label5 = new Label();
            label4 = new Label();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            btnSearchNodes = new Button();
            label3 = new Label();
            label2 = new Label();
            lblNodeName = new Label();
            lblNodeandEdge = new Label();
            panel3 = new Panel();
            label1 = new Label();
            btnHome = new Button();
            dataGridView1 = new DataGridView();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Location = new Point(12, 85);
            panel1.Name = "panel1";
            panel1.Size = new Size(519, 543);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(redOutput);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(dtpEnd);
            panel2.Controls.Add(dtpStart);
            panel2.Controls.Add(btnSearchNodes);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblNodeName);
            panel2.Controls.Add(lblNodeandEdge);
            panel2.Location = new Point(537, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(596, 323);
            panel2.TabIndex = 1;
            // 
            // redOutput
            // 
            redOutput.Location = new Point(287, 11);
            redOutput.Name = "redOutput";
            redOutput.Size = new Size(295, 295);
            redOutput.TabIndex = 5;
            redOutput.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 218);
            label5.Name = "label5";
            label5.Size = new Size(97, 15);
            label5.TabIndex = 10;
            label5.Text = "Select range: End";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 174);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 9;
            label4.Text = "Select range: Start";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(13, 236);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(251, 23);
            dtpEnd.TabIndex = 8;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(13, 192);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(251, 23);
            dtpStart.TabIndex = 7;
            // 
            // btnSearchNodes
            // 
            btnSearchNodes.Location = new Point(13, 265);
            btnSearchNodes.Name = "btnSearchNodes";
            btnSearchNodes.Size = new Size(251, 44);
            btnSearchNodes.TabIndex = 6;
            btnSearchNodes.Text = "Search";
            btnSearchNodes.UseVisualStyleBackColor = true;
            btnSearchNodes.Click += btnSearchNodes_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 97);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 4;
            label3.Text = "Node Type: ";
            label3.Click += label3_Click;
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
            // panel3
            // 
            panel3.BackColor = SystemColors.ScrollBar;
            panel3.Controls.Add(label1);
            panel3.Location = new Point(12, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(519, 67);
            panel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 11);
            label1.Name = "label1";
            label1.Size = new Size(485, 47);
            label1.TabIndex = 0;
            label1.Text = "Mesh Network topology page";
            // 
            // btnHome
            // 
            btnHome.Location = new Point(537, 570);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(596, 58);
            btnHome.TabIndex = 3;
            btnHome.Text = "Go Back Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(537, 341);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(596, 223);
            dataGridView1.TabIndex = 4;
            // 
            // frmNetworkTopology
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1144, 640);
            Controls.Add(dataGridView1);
            Controls.Add(btnHome);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Name = "frmNetworkTopology";
            Text = "frmNetworkTopology";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblNodeandEdge;
        private Label label2;
        private Label lblNodeName;
        private Label label3;
        private RichTextBox redOutput;
        private Panel panel3;
        private Label label1;
        private Button btnHome;
        private Label label5;
        private Label label4;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Button btnSearchNodes;
        private DataGridView dataGridView1;
    }
}