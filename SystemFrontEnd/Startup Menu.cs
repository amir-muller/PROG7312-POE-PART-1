namespace SystemFrontEnd;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnSensorData_Click(object sender, EventArgs e)
    {
        //hide this
        this.Hide();

        //open Sensor "page"
        SensorData sensorData = new SensorData();
        sensorData.ShowDialog();

    }
}
