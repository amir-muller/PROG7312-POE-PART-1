# PROG7312-POE
## Smart-X: Contents

| Project | Technology | Role |
|---|---|---|
| 'Smart-X.Api' | ASP.NET core minimul api running on .NET8, Entity framework core, and SQLite | Store and Simulate Live telemetry data, Runns the command stream queue, and handles the mesh topology graphing |
| 'SystemFrontEnd' | Windows Forms Application in .NET8  | Desktop UI that communicates with the API over HTTP  |

---

## How to *Download* the application suite
Download the File from the following GitHub repo: https://github.com/amir-muller/PROG7312-POE-PART-1.git

## How to **Compile** the application suite
1. Open up the file containing the .sln file with in **PowerShell**
2. Then Restore and build the suite with the following commands

| Powershell |
| --- |
| dotnet restore |
| dotnet build |

---

## How to **Run** the application suite for the **Frist Time**
1. Open up Visual Studio 2022 / Launch the Visual Studio 2022 from the '.sln' file.
2. Navigate to the **Drop Down Box** between **Any CPU** and the **Green Run Button**
3. Select **Configure Startup Projects**
4. Then select **Multiple startup projects**
5. Set both 'Smart-X.Api' and 'SystemFrontEnd' to start

After this is setup, press **F5** or **Start**

---

## Using the application suite
When opening up the application suite would start you at the 'Smart-X - Startup menu'

---

### 'Smart-X - Startup menu' Contains:

1. Sensor Data Ingestion and Telemetry
2. Real-Time Command Stream and History
3. Network Topology and Mesh Routing
4. Close

#### Sensor Data Ingestion and Telemetry
This page allows for data entry and viewing of that data

When **Registering a new sensor**
1. Enter the sensor **Sensor MAC Address**
2. Enter the **Sensor Name**
3. Enter the sensor **Sensor Location**
4. Enter the sensor's **Category** between tempreture and moisture
5. Optional **File Upload**

When **Entering a Manual Reading**
1. Enter a Sensors **Excisting MAC Adress**
2. Enter a Numerical Value

When **Viewing data**
Use the dropdown box to select whether you want to inspect the **sensor data**, or **sensor details**

Click **Refresh** to reload
Click **Go Back** to navigate to the startup page

---

### 'Real-Time Command Stream and History' Contains
A Live dashboard that updates every 3 seconds
1. **Search Box** and **Catagory Combo Box** To search by any data field, and by catogoty
2. **Line chart** that displays as the relevent data regarding the sensors, as the user is searching.
3. **Tabels** that displays the **Telemetry Data** as well as the **Sensor Details**

Command Buttons:
| Button | Function |
| --- | --- |
| **Trigger water pump** | Records a Manual override for selected sensors within the sensor list |
| **Undo** | Tacks the most recent command from the undo stack |
| **Next** | Processing waiting **Critical Items** |

---

### 'Network Topology and Mesh Routing'

Shows the sensor network as a graph. The green circle is the **Gateway Router**, and each red circle is a sensor.

1. **Click a node.** The panel shows its name, MAC address, type and location, and the node is outlined in bold.
2. Click **Search**. The output box then shows:
   - **Graph traversal (DFS from gateway):** every device connected to the gateway.
   - **Minimum spanning tree (Prim's):** the hop path from the selected node to the gateway and the total number of hops.
3. The table at the bottom shows historical readings for the selected node.

When you add a sensor on the Sensor Data page, reopen this page to see it in the graph.

