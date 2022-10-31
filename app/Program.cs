﻿using System.Text;
using System.Net;
using System.Net.Sockets;
namespace appgui;


public class Form1 : Form
{
    public Button button;
    public TextBox ipBox;
    public TextBox portBox;
    public TextBox textInputTextBox;
    public Button textInputeButton;
    //public bool license_valid;
    //public MainMenu Menu;
    public Form1()
    {
        Size = new Size(280, 250);
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
        license.Location = new Point(18, 50);
        license.AutoSize = true;
        license.Font = new Font("Calibri", 10);
        license.Padding = new Padding(6);
        this.Controls.Add(license);

        button = new Button();
        button.Size = new Size(60, 20);
        button.Location = new Point(140, 142);
        button.Text = "Run";
        this.Controls.Add(button);
        button.Click += new EventHandler(license_click);

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
