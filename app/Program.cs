using System.Text;
using System.Net;
using System.Net.Sockets;
namespace appgui;


public class Form1 : Form
{
    public Button button1;
    public Button button2;
    public TextBox ipBox;
    public TextBox portBox;
    public TextBox textInputTextBox;
    public Button textInputeButton;
    //public bool license_valid;
    //public MainMenu Menu;
    public Form1()
    {
        Label ipLabel = new Label();
        ipLabel.Text = "IP Address";
        ipLabel.Location = new Point(10, 10);
        ipLabel.AutoSize = true;
        ipLabel.Font = new Font("Calibri", 10);
        this.Controls.Add(ipLabel);

        ipBox = new TextBox();
        ipBox.Location = new Point(90, 9);
        ipBox.Size = new Size(150, 20);
        this.Controls.Add(ipBox);

        Label portLabel = new Label();
        portLabel.Text = "Port";
        portLabel.Location = new Point(35, 40);
        portLabel.AutoSize = true;
        portLabel.Font = new Font("Calibri", 10);
        this.Controls.Add(portLabel);

        portBox = new TextBox();
        portBox.Location = new Point(90, 45);
        portBox.Size = new Size(120, 20);
        this.Controls.Add(portBox);

        Label key_label = new Label();
        key_label.Text = "License Key";
        key_label.Location = new Point(10, 50);
        key_label.AutoSize = true;
        key_label.Font = new Font("Calibri", 10);
        key_label.Padding = new Padding(6);
        this.Controls.Add(key_label);

        textInputTextBox = new TextBox();
        textInputTextBox.Location = new Point(110, 50);
        textInputTextBox.Size = new Size(120, 20);
        this.Controls.Add(textInputTextBox);

        Size = new Size(350, 300);
        button2 = new Button();
        button2.Size = new Size(60, 25);
        button2.Location = new Point(120, 90);
        button2.Text = "Run";
        this.Controls.Add(button2);
        button2.Click += new EventHandler(license_click);
    }

    private void license_click(object sender, EventArgs e)
    {
        MessageBox.Show("ip address " + ipBox.Text);
        if (String.IsNullOrEmpty(textInputTextBox.Text))
        {
            MessageBox.Show("Please enter a valid license key");
            return;
        }
        else if (Convert.ToBase64String(Encoding.ASCII.GetBytes(textInputTextBox.Text)) == "MTIzNDcyMzA5NTcyMzkwNTM=")
        {
            var ip = "127.0.0.1";
            byte[] msg = Encoding.ASCII.GetBytes("1");
            //MessageBox.Show("1");
            IPAddress address = IPAddress.Parse(ip);
            //MessageBox.Show(ip);
            //MessageBox.Show(address.ToString());
            //MessageBox.Show("2");
            IPEndPoint endPoint = new IPEndPoint(address, 8080);
            //MessageBox.Show(endPoint.ToString());
            //MessageBox.Show("3");
            Socket Sock = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //MessageBox.Show("4");
            Sock.Connect(endPoint);
            //MessageBox.Show(endPoint.ToString());
            //MessageBox.Show("5");
            Sock.Send(msg, msg.Length, 0);
            byte[] buffer = new byte[1024];
            int recieved = Sock.Receive(buffer);
            byte[] data = new byte[recieved];
            Array.Copy(buffer, data, recieved);
            MessageBox.Show(Encoding.ASCII.GetString(data));
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
