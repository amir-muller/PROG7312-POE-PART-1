using SystemFrontEnd.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SystemFrontEnd;

public partial class SensorData : Form
{
    private readonly ApiClient _apiClient = new ApiClient();

    public SensorData()
    {
        InitializeComponent();
    }

    private async void SensorData_Load(object sender, EventArgs e)
    {
        if (cmbSensorDetailType.SelectedIndex == -1 && cmbSensorDetailType.Items.Count > 0)
        {
            cmbSensorDetailType.SelectedIndex = 0;
        }

        await LoadSelectedDataAsync();
    }

    private async void cmbSensorDetailType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateUiState();
        await LoadSelectedDataAsync();
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        await LoadSelectedDataAsync();
    }

    private void UpdateUiState()
    {
        string selectedMode = cmbSensorDetailType.SelectedItem?.ToString() ?? "Sensor Data";

        if (selectedMode == "Sensor Data")
        {
            panel1.Enabled = true;
            panel2.Enabled = false;
        }
        else
        {
            panel1.Enabled = false;
            panel2.Enabled = true;
        }
    }

    private async Task LoadSelectedDataAsync()
    {
        string selectedMode = cmbSensorDetailType.SelectedItem?.ToString() ?? "Sensor Data";

        try
        {
            if (selectedMode == "Sensor Data")
            {
                var records = await _apiClient.GetAllDataAsync();
                dataGridView1.DataSource = records;
            }
            else
            {
                var details = await _apiClient.GetAllSensorDetailsAsync();
                dataGridView1.DataSource = details;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading {selectedMode}: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSubmit_Click(object sender, EventArgs e)
    {
        string sensorMAC = txtSensorMAC.Text.Trim();
        string rawValue = txtSensorValue.Text.Trim();

        if (string.IsNullOrWhiteSpace(sensorMAC))
        {
            MessageBox.Show("Sensor MAC address cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!double.TryParse(rawValue, out double sensorValue))
        {
            MessageBox.Show("Sensor Value must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            btnSubmit.Enabled = false;
            var createdRecord = await _apiClient.PostDataAsync(sensorMAC, sensorValue);

            MessageBox.Show($"Data reading saved! ID: {createdRecord?.Id}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtSensorMAC.Clear();
            txtSensorValue.Clear();

            await LoadSelectedDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSubmit.Enabled = true;
        }
    }


    private async void btnSubmitDetails_Click(object sender, EventArgs e)
    {
        string sensorMAC = txtSensorMAC2.Text.Trim();
        string location = txtSensorLocation.Text.Trim();
        string name = txtSensorName.Text.Trim();
        string category = cmbSensorCategories.SelectedItem?.ToString() ?? cmbSensorCategories.Text.Trim();

        if (string.IsNullOrWhiteSpace(sensorMAC))
        {
            MessageBox.Show("Sensor MAC Address cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Sensor Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            btnSubmitDetails.Enabled = false;
            var createdDetails = await _apiClient.PostSensorDetailsAsync(sensorMAC, location, name, category);

            MessageBox.Show($"Sensor details saved! ID: {createdDetails?.Id}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtSensorMAC2.Clear();
            txtSensorLocation.Clear();
            txtSensorName.Clear();
            cmbSensorCategories.SelectedIndex = -1;

            await LoadSelectedDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSubmitDetails.Enabled = true;
        }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        this.Hide();
        Form1 menuForm = new Form1();
        menuForm.Show();
    }

    private async void btnUploadFile_Click(object sender, EventArgs e)
    {
        string mac = txtSensorMAC2.Text.Trim();

        if (string.IsNullOrWhiteSpace(mac))
        {
            MessageBox.Show("Please enter \' sensor mac address \' first", "Validation Error", MessageBoxButtons.OK);
            return;
        }

        using OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Supported Files|*.png;*.jpg;*.jpeg;*.json;*.txt;*.xml;*.ini|Images|*.png;*.jpg;*.jpeg|Configs|*.json;*.txt;*.xml;*.ini",
            Title = "Select Setup Photo or Config File"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK) //dotnet-bot (2026).
        {
            string extension = Path.GetExtension(openFileDialog.FileName).ToLower();
            string fileType = (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                ? "DeploymentPhoto"
                : "ConfigFile";

            try
            {
                btnUploadFile.Enabled = false;
                var result = await _apiClient.UploadFileAsync(mac, fileType, openFileDialog.FileName);

                MessageBox.Show($"File '{result?.FileName}' uploaded successfully as {fileType}!", "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUploadFile.Enabled = true;
            }
        }

    }
}

// referancing
//dotnet-bot. (2026). OpenFileDialog class (system.Windows.Forms). 
//  Microsoft.Com.https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-10.0