namespace SystemFrontEnd
{
    partial class frmCommandStream
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
            btnGoHome = new Button();
            panel1 = new Panel();
            lblMocSensor = new Label();
            label3 = new Label();
            panel2 = new Panel();
            cmbFilter = new ComboBox();
            lblFilter = new Label();
            btnFilter = new Button();
            btnSearch = new Button();
            lblSearch = new Label();
            txbSearch = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            chartPanel = new Panel();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // btnGoHome
            // 
            btnGoHome.BackColor = SystemColors.ControlLight;
            btnGoHome.Location = new Point(1102, 579);
            btnGoHome.Name = "btnGoHome";
            btnGoHome.Size = new Size(218, 49);
            btnGoHome.TabIndex = 0;
            btnGoHome.Text = "Go Home";
            btnGoHome.UseVisualStyleBackColor = false;
            btnGoHome.Click += btnGoHome_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(lblMocSensor);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(1102, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(218, 183);
            panel1.TabIndex = 1;
            // 
            // lblMocSensor
            // 
            lblMocSensor.AutoSize = true;
            lblMocSensor.Location = new Point(3, 47);
            lblMocSensor.Name = "lblMocSensor";
            lblMocSensor.Size = new Size(92, 15);
            lblMocSensor.TabIndex = 2;
            lblMocSensor.Text = "Selected Sensor:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Underline);
            label3.Location = new Point(3, 9);
            label3.Name = "label3";
            label3.Size = new Size(211, 21);
            label3.TabIndex = 1;
            label3.Text = "Manual Override Commands";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(cmbFilter);
            panel2.Controls.Add(lblFilter);
            panel2.Controls.Add(btnFilter);
            panel2.Controls.Add(btnSearch);
            panel2.Controls.Add(lblSearch);
            panel2.Controls.Add(txbSearch);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(428, 129);
            panel2.TabIndex = 2;
            // 
            // cmbFilter
            // 
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(149, 97);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(121, 23);
            cmbFilter.TabIndex = 5;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilter.Location = new Point(12, 97);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(107, 15);
            lblFilter.TabIndex = 4;
            lblFilter.Text = "Filter by Catagory:";
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(213, 10);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(97, 33);
            btnFilter.TabIndex = 3;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(316, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(99, 64);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(12, 11);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(185, 32);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Search Function";
            // 
            // txbSearch
            // 
            txbSearch.Location = new Point(12, 51);
            txbSearch.Name = "txbSearch";
            txbSearch.Size = new Size(298, 23);
            txbSearch.TabIndex = 0;
            // 
            // chartPanel
            // 
            chartPanel.Location = new Point(12, 147);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(428, 223);
            chartPanel.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 376);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(428, 231);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(446, 12);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(428, 595);
            dataGridView2.TabIndex = 5;
            // 
            // frmCommandStream
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1332, 640);
            Controls.Add(dataGridView1);
            Controls.Add(dataGridView2);
            Controls.Add(chartPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnGoHome);
            Name = "frmCommandStream";
            Text = "frmCommandStream";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnGoHome;
        private Panel panel1;
        private Label label3;
        private Panel panel2;
        private Button btnSearch;
        private Label lblSearch;
        private TextBox txbSearch;
        private Button btnFilter;
        private ComboBox cmbFilter;
        private Label lblFilter;
        private Label lblMocSensor;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel chartPanel;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
    }
}