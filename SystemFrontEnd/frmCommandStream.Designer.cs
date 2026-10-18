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
            components = new System.ComponentModel.Container();
            btnGoHome = new Button();
            panel1 = new Panel();
            btnTriggerPump = new Button();
            lblStatus = new Label();
            label1 = new Label();
            lblMocSensor = new Label();
            label3 = new Label();
            panel2 = new Panel();
            cmbFilterCategory = new ComboBox();
            lblFilter = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            chartPanel = new Panel();
            dgvDevices = new DataGridView();
            dgvTelemetry = new DataGridView();
            RefreshTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTelemetry).BeginInit();
            SuspendLayout();
            // 
            // btnGoHome
            // 
            btnGoHome.BackColor = SystemColors.ControlLight;
            btnGoHome.Location = new Point(1114, 610);
            btnGoHome.Name = "btnGoHome";
            btnGoHome.Size = new Size(206, 49);
            btnGoHome.TabIndex = 0;
            btnGoHome.Text = "Go Home";
            btnGoHome.UseVisualStyleBackColor = false;
            btnGoHome.Click += btnGoHome_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(btnTriggerPump);
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblMocSensor);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(1111, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 580);
            panel1.TabIndex = 1;
            // 
            // btnTriggerPump
            // 
            btnTriggerPump.Location = new Point(20, 114);
            btnTriggerPump.Name = "btnTriggerPump";
            btnTriggerPump.Size = new Size(75, 44);
            btnTriggerPump.TabIndex = 5;
            btnTriggerPump.Text = "Trigger Pump";
            btnTriggerPump.UseVisualStyleBackColor = true;
            btnTriggerPump.Click += btnTriggerPump_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(3, 157);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 114);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 3;
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
            panel2.Controls.Add(cmbFilterCategory);
            panel2.Controls.Add(lblFilter);
            panel2.Controls.Add(lblSearch);
            panel2.Controls.Add(txtSearch);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(320, 129);
            panel2.TabIndex = 2;
            // 
            // cmbFilterCategory
            // 
            cmbFilterCategory.FormattingEnabled = true;
            cmbFilterCategory.Location = new Point(149, 97);
            cmbFilterCategory.Name = "cmbFilterCategory";
            cmbFilterCategory.Size = new Size(161, 23);
            cmbFilterCategory.TabIndex = 5;
            cmbFilterCategory.Text = "Select Device Catagory";
            cmbFilterCategory.SelectedIndexChanged += cmbFilterCategory_SelectedIndexChanged;
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
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(12, 11);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(185, 32);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Search Function";
            lblSearch.Click += lblSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 51);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(298, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chartPanel
            // 
            chartPanel.Location = new Point(12, 147);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(602, 223);
            chartPanel.TabIndex = 3;
            // 
            // dgvDevices
            // 
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Location = new Point(12, 376);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.Size = new Size(602, 231);
            dgvDevices.TabIndex = 4;
            dgvDevices.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dgvTelemetry
            // 
            dgvTelemetry.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTelemetry.Location = new Point(620, 12);
            dgvTelemetry.Name = "dgvTelemetry";
            dgvTelemetry.Size = new Size(485, 595);
            dgvTelemetry.TabIndex = 5;
            // 
            // frmCommandStream
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 671);
            Controls.Add(dgvDevices);
            Controls.Add(dgvTelemetry);
            Controls.Add(chartPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnGoHome);
            Name = "frmCommandStream";
            Text = "frmCommandStream";
            FormClosing += frmCommandStream_FormClosing;
            FormClosed += frmCommandStream_FormClosed;
            Load += frmCommandStream_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTelemetry).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnGoHome;
        private Panel panel1;
        private Label label3;
        private Panel panel2;
        private Label lblSearch;
        private TextBox txtSearch;
        private ComboBox cmbFilterCategory;
        private Label lblFilter;
        private Label lblMocSensor;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel chartPanel;
        private DataGridView dgvDevices;
        private DataGridView dgvTelemetry;
        private System.Windows.Forms.Timer RefreshTimer;
        private Label label1;
        private Label lblStatus;
        private Button btnTriggerPump;
    }
}