using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Chat_Client
{
	public partial class Form1 : Form
	{
		Socket mainClient = null;
		IPEndPoint ip;
		const int messageSize = 3024000;
		byte[] message= new byte[messageSize];
		string createdFilePath;
		public Form1()
		{
			InitializeComponent();
			ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"),5000);

			// connect signal
			connect_status.Text = "Waiting for connect...";
			connect_status.ReadOnly = true;
			connect_status.BackColor = Color.White;
			connect_status.ForeColor= Color.Red;

			/// list box direction
			server_listbox.DrawMode = DrawMode.OwnerDrawFixed;
			server_listbox.ItemHeight = 20;
			server_listbox.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);

			/// combobox
			combo_send_type.SelectedIndex = 0;
		}
		bool connect = true;
		private void connect_button_Click_1(object sender, EventArgs e)
		{
			if (connect)
			{
				mainClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				mainClient.BeginConnect(ip, new AsyncCallback(AsyncConnect), mainClient);
				connect= false;
			}
		}
		void AsyncConnect(IAsyncResult air)
		{
			Socket client = (Socket)air.AsyncState;
			try
			{
				client.EndConnect(air);
				Invoke((MethodInvoker)delegate
				{
					connect_status.Text = "Connected to: " + client.RemoteEndPoint.ToString();
					connect_status.ForeColor = Color.Green;
				});
				client.BeginReceive(message, 0, messageSize, SocketFlags.None, new AsyncCallback(AsyncReive), client);

			}
			catch (Exception)
			{
				Invoke((MethodInvoker)delegate
				{
					connect_status.Text = "Connection Error";
					connect_status.ForeColor = Color.Red;
				});
			}
		}

		private void send_button_Click(object sender, EventArgs e)
		{
			if (mainClient == null)
			{
				MessageBox.Show("Connect First");
			}
			else
			{
				string prefx = "%_T%";
				if (combo_send_type.Text == "Text")
				{
					prefx = "%_T%";
				} else if(combo_send_type.Text == "File")
				{
					prefx = "%_F%";
				}
				else if(combo_send_type.Text == "Image")
				{
					prefx = "%_I%";
				}
				else if(combo_send_type.Text == "Directory")
				{
					prefx = "%_D%";
				}
				byte[] data = Encoding.UTF8.GetBytes(prefx + " " + send_text.Text);
				try
				{
					if (send_text.Text.Trim() != "")
					{
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add(send_text.Text + " :Client");
							send_text.Clear();
						});
						mainClient.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(AsyncSend), mainClient);
					}
				}catch(Exception)
				{
					MessageBox.Show("connection Error");
				}
			}
		}
		void AsyncSend(IAsyncResult air)
		{
			Socket client = (Socket)air.AsyncState;
			client.EndSend(air);
		}
		void AsyncReive(IAsyncResult air)
		{
			Socket client = (Socket)(air.AsyncState);
			try
			{
				int numchar = client.EndReceive(air);
				string reciveMessage = Encoding.UTF8.GetString(message, 0, numchar);
				string prefx = reciveMessage.Substring(0, 4);
				reciveMessage = reciveMessage.Substring(4).Trim();
				if (prefx == "%_F%")
				{
					string[] allPathParts = reciveMessage.Split(' ');
					string fileName = allPathParts[0];

					if (allPathParts[1] == "isFound")
					{
						int messageTextLingth = Encoding.UTF8.GetBytes(prefx + " " + fileName + " isFound").Length; 
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: file=> \"" + fileName +"\" is found");
						});
						createdFilePath = @"E:\1-programming code\VS projects\c#\Chat-Client\recieved Files\" + fileName;
						using (FileStream file = new FileStream(createdFilePath, FileMode.Create))
						{
							file.Write(message, messageTextLingth+1, reciveMessage.Length);
						}
						using (StreamReader file = new StreamReader(createdFilePath))
						{
							string line;
							while((line = file.ReadLine())!= null)
							{
								Invoke((MethodInvoker)delegate
								{
									server_listbox.Items.Add("          " + line);
								});
							}
						}


					}
					else
					{
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: file=> " + fileName +" is not found");
						});
					}
				}
				else if (prefx == "%_I%")
				{
					// lost 221 byte 
					reciveMessage = Encoding.UTF8.GetString(message);
					reciveMessage = reciveMessage.Substring(4).Trim();
					string[] allPathParts = reciveMessage.Split(' ');
					string fileName = allPathParts[0];
					if (allPathParts[1] == "isFound")
					{
						int messageTextLingth = Encoding.UTF8.GetBytes("%_I% " + fileName + " isFound ").Length;
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: Image=> \"" + fileName + "\" is found");
						});
						createdFilePath = @"E:\1-programming code\VS projects\c#\Chat-Client\recieved images\" + fileName;
						using (FileStream img = new FileStream(createdFilePath, FileMode.Create))
						{
							img.Write(message, messageTextLingth , reciveMessage.Length);
						}
						// Convert byte array to Image
						picture_form_server.Image = System.Drawing.Image.FromFile(createdFilePath);
					}
					else
					{
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: Image=> " + fileName + " is not found");
						});
					}
				}
				else if (prefx == "%_D%")
				{
					string[] contectDirec = reciveMessage.Split('$');
					if (contectDirec[0].Trim()== "found")
					{
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: " + "Directory content from server");
							for(int i = 1;i < contectDirec.Length -1; i++)
							{
								server_listbox.Items.Add("          " + contectDirec[i].Trim());
							}
						});
					}
					else
					{
						Invoke((MethodInvoker)delegate
						{
							server_listbox.Items.Add("Server: " + "This Directory is not found");
						});
					}
				}
				else
				{
					Invoke((MethodInvoker)delegate
					{
						server_listbox.Items.Add("Server: "+reciveMessage);
					});
				}
				client.BeginReceive(message, 0, messageSize, SocketFlags.None, new AsyncCallback(AsyncReive), client);
			}
			catch(Exception )
			{
				Invoke((MethodInvoker)delegate
				{
					connect_status.Text = "Waiting for connect...";
				});
			}
		}
		private void disconnect_button_Click(object sender, EventArgs e)
		{
			try
			{
				if(mainClient == null)
					throw new Exception("");
				connect = true;
				mainClient.Close();
				connect_status.Text = "Waiting for connect...";
				connect_status.ForeColor = Color.Red;
				server_listbox.Items.Clear();
			}
			catch (Exception)
			{
				MessageBox.Show("We are not connected to disconnect");
			}
		}
		private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			// Get the ListBox and the item to draw
			if (e.Index < 0 || e.Index >= server_listbox.Items.Count)
			{
				return;
			}
			ListBox listBox = (ListBox)sender;
			string itemText = listBox.Items[e.Index].ToString();

			// Draw the background
			e.DrawBackground();

			// Create a StringFormat object to specify the alignment
			StringFormat sf = new StringFormat();
			Font itemFont;
			itemFont = new Font(e.Font, FontStyle.Bold);

			Brush itemBrush;
			if (itemText.EndsWith("Client"))
			{
				sf.Alignment = StringAlignment.Far; // Align text to the far right
				itemBrush = Brushes.BlueViolet;
			}
			else
			{
				sf.Alignment = StringAlignment.Near; // Align text to the far right
				itemBrush = Brushes.Blue;
			}
			sf.LineAlignment = StringAlignment.Center; // Center vertically
													   // Draw the text with the specified alignment
			e.Graphics.DrawString(
				itemText,
				itemFont,
				itemBrush,
				e.Bounds,
				sf);

			// Draw the focus rectangle if the item has focus
			e.DrawFocusRectangle();
		}


		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void server_listbox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void combo_send_type_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
