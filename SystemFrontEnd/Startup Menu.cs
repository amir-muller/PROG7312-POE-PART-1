namespace SystemFrontEnd;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        btnRealtimeCommandandHistory.Enabled = true;
    }

    private void btnSensorData_Click(object sender, EventArgs e)
    {
        //hide this
        this.Hide();

        //open Sensor "page"
        SensorData sensorData = new SensorData();
        sensorData.ShowDialog();

    }

    private void btnRealtimeCommandandHistory_Click(object sender, EventArgs e)
    {
        //hide this
        this.Hide();

        //open page
        frmCommandStream commandStream = new frmCommandStream();
        commandStream.ShowDialog();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        //close
        this.Close();
    }
}
