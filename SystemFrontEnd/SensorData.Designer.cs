namespace SystemFrontEnd;

partial class SensorData
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
        btnRefresh = new Button();
        dataGridView1 = new DataGridView();
        btnBack = new Button();
        panel1 = new Panel();
        btnSubmit = new Button();
        label2 = new Label();
        label1 = new Label();
        txtSensorMAC = new TextBox();
        txtSensorValue = new TextBox();
        cmbSensorDetailType = new ComboBox();
        panel2 = new Panel();
        btnSubmitDetails = new Button();
        cmbSensorCategories = new ComboBox();
        txtSensorName = new TextBox();
        txtSensorLocation = new TextBox();
        txtSensorMAC2 = new TextBox();
        label6 = new Label();
        label5 = new Label();
        label4 = new Label();
        label3 = new Label();
        btnUploadFile = new Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(635, 388);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(157, 50);
        btnRefresh.TabIndex = 4;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(12, 44);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new Size(617, 394);
        dataGridView1.TabIndex = 5;
        // 
        // btnBack
        // 
        btnBack.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnBack.ForeColor = Color.Red;
        btnBack.Location = new Point(798, 388);
        btnBack.Name = "btnBack";
        btnBack.Size = new Size(157, 50);
        btnBack.TabIndex = 6;
        btnBack.Text = "Go Back";
        btnBack.UseVisualStyleBackColor = true;
        btnBack.Click += btnBack_Click;
        // 
        // panel1
        // 
        panel1.Controls.Add(btnSubmit);
        panel1.Controls.Add(label2);
        panel1.Controls.Add(label1);
        panel1.Controls.Add(txtSensorMAC);
        panel1.Controls.Add(txtSensorValue);
        panel1.Location = new Point(635, 44);
        panel1.Name = "panel1";
        panel1.Size = new Size(157, 185);
        panel1.TabIndex = 7;
        // 
        // btnSubmit
        // 
        btnSubmit.Location = new Point(22, 126);
        btnSubmit.Name = "btnSubmit";
        btnSubmit.Size = new Size(117, 37);
        btnSubmit.TabIndex = 7;
        btnSubmit.Text = "Submit";
        btnSubmit.UseVisualStyleBackColor = true;
        btnSubmit.Click += btnSubmit_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(22, 67);
        label2.Name = "label2";
        label2.Size = new Size(70, 15);
        label2.TabIndex = 6;
        label2.Text = "SensorValue";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(22, 14);
        label1.Name = "label1";
        label1.Size = new Size(117, 15);
        label1.TabIndex = 5;
        label1.Text = "Sensor MAC Address";
        // 
        // txtSensorMAC
        // 
        txtSensorMAC.Location = new Point(22, 32);
        txtSensorMAC.Name = "txtSensorMAC";
        txtSensorMAC.Size = new Size(117, 23);
        txtSensorMAC.TabIndex = 4;
        // 
        // txtSensorValue
        // 
        txtSensorValue.Location = new Point(22, 85);
        txtSensorValue.Name = "txtSensorValue";
        txtSensorValue.Size = new Size(117, 23);
        txtSensorValue.TabIndex = 3;
        // 
        // cmbSensorDetailType
        // 
        cmbSensorDetailType.FormattingEnabled = true;
        cmbSensorDetailType.Items.AddRange(new object[] { "Sensor Data", "Sensor Details" });
        cmbSensorDetailType.Location = new Point(12, 12);
        cmbSensorDetailType.Name = "cmbSensorDetailType";
        cmbSensorDetailType.Size = new Size(221, 23);
        cmbSensorDetailType.TabIndex = 8;
        // 
        // panel2
        // 
        panel2.Controls.Add(btnUploadFile);
        panel2.Controls.Add(btnSubmitDetails);
        panel2.Controls.Add(cmbSensorCategories);
        panel2.Controls.Add(txtSensorName);
        panel2.Controls.Add(txtSensorLocation);
        panel2.Controls.Add(txtSensorMAC2);
        panel2.Controls.Add(label6);
        panel2.Controls.Add(label5);
        panel2.Controls.Add(label4);
        panel2.Controls.Add(label3);
        panel2.Location = new Point(798, 44);
        panel2.Name = "panel2";
        panel2.Size = new Size(157, 338);
        panel2.TabIndex = 9;
        // 
        // btnSubmitDetails
        // 
        btnSubmitDetails.Location = new Point(19, 290);
        btnSubmitDetails.Name = "btnSubmitDetails";
        btnSubmitDetails.Size = new Size(117, 36);
        btnSubmitDetails.TabIndex = 11;
        btnSubmitDetails.Text = "Submit Details";
        btnSubmitDetails.UseVisualStyleBackColor = true;
        btnSubmitDetails.Click += btnSubmitDetails_Click;
        // 
        // cmbSensorCategories
        // 
        cmbSensorCategories.FormattingEnabled = true;
        cmbSensorCategories.Items.AddRange(new object[] { "Ground Moisture ", "Valve State", "Tempreture " });
        cmbSensorCategories.Location = new Point(19, 203);
        cmbSensorCategories.Name = "cmbSensorCategories";
        cmbSensorCategories.Size = new Size(117, 23);
        cmbSensorCategories.TabIndex = 10;
        // 
        // txtSensorName
        // 
        txtSensorName.Location = new Point(19, 144);
        txtSensorName.Name = "txtSensorName";
        txtSensorName.Size = new Size(117, 23);
        txtSensorName.TabIndex = 6;
        // 
        // txtSensorLocation
        // 
        txtSensorLocation.Location = new Point(19, 85);
        txtSensorLocation.Name = "txtSensorLocation";
        txtSensorLocation.Size = new Size(117, 23);
        txtSensorLocation.TabIndex = 5;
        // 
        // txtSensorMAC2
        // 
        txtSensorMAC2.Location = new Point(19, 32);
        txtSensorMAC2.Name = "txtSensorMAC2";
        txtSensorMAC2.Size = new Size(117, 23);
        txtSensorMAC2.TabIndex = 4;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(19, 185);
        label6.Name = "label6";
        label6.Size = new Size(93, 15);
        label6.TabIndex = 3;
        label6.Text = "Sensor Category";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(19, 126);
        label5.Name = "label5";
        label5.Size = new Size(77, 15);
        label5.TabIndex = 2;
        label5.Text = "Sensor Name";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(19, 67);
        label4.Name = "label4";
        label4.Size = new Size(91, 15);
        label4.TabIndex = 1;
        label4.Text = "Sensor Location";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(19, 14);
        label3.Name = "label3";
        label3.Size = new Size(117, 15);
        label3.TabIndex = 0;
        label3.Text = "Sensor MAC Address";
        // 
        // btnUploadFile
        // 
        btnUploadFile.Location = new Point(19, 248);
        btnUploadFile.Name = "btnUploadFile";
        btnUploadFile.Size = new Size(115, 36);
        btnUploadFile.TabIndex = 12;
        btnUploadFile.Text = "Upload File";
        btnUploadFile.UseVisualStyleBackColor = true;
        btnUploadFile.Click += btnUploadFile_Click;
        // 
        // SensorData
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(970, 450);
        Controls.Add(panel2);
        Controls.Add(cmbSensorDetailType);
        Controls.Add(panel1);
        Controls.Add(btnBack);
        Controls.Add(dataGridView1);
        Controls.Add(btnRefresh);
        Name = "SensorData";
        Text = "SensorDataintegrationAndTelemetry";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        panel2.ResumeLayout(false);
        panel2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private Button btnRefresh;
    private DataGridView dataGridView1;
    private Button btnBack;
    private Panel panel1;
    private Label label2;
    private Label label1;
    private TextBox txtSensorMAC;
    private TextBox txtSensorValue;
    private ComboBox cmbSensorDetailType;
    private Button btnSubmit;
    private Panel panel2;
    private Label label6;
    private Label label5;
    private Label label4;
    private Label label3;
    private ComboBox cmbSensorCategories;
    private TextBox txtSensorName;
    private TextBox txtSensorLocation;
    private TextBox txtSensorMAC2;
    private Button btnSubmitDetails;
    private Button btnUploadFile;
}