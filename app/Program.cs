using System.Text;
using System.Net;
using System.Net.Sockets;
namespace appgui;


public class Form1 : Form
{
    public Button button;
    public TextBox ipBox;
    public TextBox portBox;
    public TextBox textInputTextBox;
    public TextBox licenseKeyBox;
    //public bool license_valid;
    //public MainMenu Menu;
    public Form1()
    {
        Size = new Size(280, 200);
        Label ip_label = new Label();
        ip_label.Text = "IP";
        ip_label.Location = new Point(18, 20);
        ip_label.AutoSize = true;
        ip_label.Font = new Font("Calibri", 10);
        ip_label.Padding = new Padding(6);
        this.Controls.Add(ip_label);

        ipBox = new TextBox();
        ipBox.Location = new Point(53, 23);
        ipBox.Size = new Size(90, 90);
        this.Controls.Add(ipBox);

        Label port_label = new Label();
        port_label.Text = "Port";
        port_label.Location = new Point(153, 20);
        port_label.AutoSize = true;
        port_label.Font = new Font("Calibri", 10);
        port_label.Padding = new Padding(6);
        this.Controls.Add(port_label);

        portBox = new TextBox();
        portBox.Location = new Point(198, 23);
        portBox.Size = new Size(40, 30);
        this.Controls.Add(portBox);

        Label license = new Label();
        license.Text = "License";
        license.Location = new Point(18, 70);
        license.AutoSize = true;
        license.Font = new Font("Calibri", 10);
        license.Padding = new Padding(6);
        this.Controls.Add(license);

        licenseKeyBox = new TextBox();
        licenseKeyBox.Location = new Point(80, 73);
        licenseKeyBox.Size = new Size(150, 90);
        this.Controls.Add(licenseKeyBox);

        button = new Button();
        button.Size = new Size(60, 20);
        button.Location = new Point(110, 120);
        button.Text = "Run";
        this.Controls.Add(button);
        button.Click += new EventHandler(license_click);

    }

    private void license_click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(ipBox.Text))
        {
            MessageBox.Show("Please enter a IP address");
            return;
        }
        else if (String.IsNullOrEmpty(portBox.Text))
        {
            MessageBox.Show("Please enter a port");
            return;
        }
        else if (String.IsNullOrEmpty(licenseKeyBox.Text))
        {
            MessageBox.Show("Please enter a license key");
            return;
        }
        else if (Convert.ToBase64String(Encoding.ASCII.GetBytes(licenseKeyBox.Text)) == "MTIzNDcyMzA5NTcyMzkwNTM=")
        {
            IPAddress address = IPAddress.Parse(ipBox.Text);
            IPEndPoint endPoint = new IPEndPoint(address, Convert.ToInt32(portBox.Text));
            Socket Sock = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Sock.Connect(endPoint);
            byte[] snd_buffer = new byte[1];
            //snd_buffer[0] = this.license_hash;
            snd_buffer[0] = 0xfe;
            Sock.Send(snd_buffer, 1, 0);
            byte[] buffer = new byte[1024];
            int received = Sock.Receive(buffer);
            Console.WriteLine("Received: {0}", Encoding.ASCII.GetString(buffer));
            MessageBox.Show(Encoding.ASCII.GetString(buffer));
            //Console.WriteLine(text);
            //MessageBox.Show(Encoding.ASCII.GetString(data));
            //MessageBox.Show("6");
            Sock.Close();
            return;
        }
        else
        {
            MessageBox.Show("Please enter a valid license key");
            return;
        }
    }
}

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
