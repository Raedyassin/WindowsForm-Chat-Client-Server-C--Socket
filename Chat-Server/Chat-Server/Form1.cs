using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Server
{
	public partial class Server : Form
	{
		Socket ServerSocket;
		Socket client;
		IPEndPoint ip;
		string reciveMessage="";
		string prefx = "%_T%";
		byte[] message= new byte[1024];
		public Server()
		{
			InitializeComponent();
			ServerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
			ip = new IPEndPoint(IPAddress.Any, 5000);
			ServerSocket.Bind(ip);
			ServerSocket.Listen(10);

			// connect signal d
			connect_status.Text = "Waiting for client...";
			connect_status.ReadOnly= true;
			connect_status.ForeColor = Color.Red;
			connect_status.BackColor= Color.White;
			
			// get the connection
			ServerSocket.BeginAccept(new AsyncCallback(AsyncConnect), ServerSocket);

			/// direction of listbox
			client_listbox.DrawMode = DrawMode.OwnerDrawFixed;
			client_listbox.ItemHeight = 20;
			client_listbox.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);
		}

		void AsyncConnect(IAsyncResult iar)
		{
			Socket wellcomingSocket = (Socket)iar.AsyncState;
			client = wellcomingSocket.EndAccept(iar);
			Invoke((MethodInvoker) delegate
			{
				connect_status.Text = "Connected";
				connect_status.ForeColor = Color.Green;
			});
			client.BeginReceive(message, 0, 1024, SocketFlags.None, new AsyncCallback(AsyncReive), client);
		}

		void AsyncReive(IAsyncResult air)
		{
			Socket client = (Socket)air.AsyncState;
			try
			{
				int numchar = client.EndReceive(air);
				reciveMessage = Encoding.UTF8.GetString(message, 0, numchar);
				prefx = reciveMessage.Substring(0,4);
				reciveMessage = reciveMessage.Substring(4).Trim();
				if (prefx == "%_F%")
				{

					string resultOfRequstTheFile = "File not found";
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add("Client: " + "Send this file => \"" + reciveMessage +"\"" );
					});
					byte[] textBytes ;
					string filePath = reciveMessage;
					string[] allPathParts = filePath.Split('\\');
					string fileName = allPathParts[allPathParts.Length - 1];
					if (File.Exists(filePath))
					{
						textBytes = Encoding.UTF8.GetBytes("%_F% " + fileName + " isFound ");	
						using (FileStream file = new FileStream(filePath, FileMode.Open))
						{
							byte []fileBytes = new byte[file.Length];
							file.Read(fileBytes, 0, fileBytes.Length);
							fileBytes = textBytes.Concat(fileBytes).ToArray();
							client.BeginSend(fileBytes, 0, fileBytes.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
						}
						resultOfRequstTheFile = "File is found";
					}
					else
					{
						textBytes = Encoding.UTF8.GetBytes("%_F% " + fileName + " notFound");
						client.BeginSend(textBytes, 0, textBytes.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
					}
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add(resultOfRequstTheFile+ " :Server");
					});

				}
				else if (prefx == "%_I%")
				{
					byte[] textBytes;
					string resultOfRequstTheImage = "Image not found";
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add("Client: " + "Send this Image => \"" + reciveMessage + "\"");
					});
					string[] imageParts = reciveMessage.Split('\\');
					string imageName = imageParts[imageParts.Length - 1];
					if(File.Exists(reciveMessage))
					{
						textBytes = Encoding.UTF8.GetBytes("%_I% " + imageName + " isFound ");
						using (FileStream image = new FileStream(reciveMessage, FileMode.Open))
						{
							byte[] img = new byte[image.Length];
							image.Read(img, 0, img.Length);
							img = textBytes.Concat(img).ToArray();
							client.BeginSend(img, 0, img.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
						}
						resultOfRequstTheImage = "Image is found";
					}
					else
					{
						textBytes = Encoding.UTF8.GetBytes("%_I% " + imageName + " notFound ");
						client.BeginSend(textBytes, 0, textBytes.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
					}
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add(resultOfRequstTheImage + " :Server");
					});
				}
				else if (prefx == "%_D%")
				{
					string DirectoryInformation = "";
					string status = "not found";
					//The * wildcard means "match any directory name".
					if(Directory.Exists(reciveMessage))
					{
						DirectoryInformation = "**** Direcoties **** $";
						foreach (string dir in Directory.GetDirectories(reciveMessage))
						{
							DirectoryInformation += "=> "+dir + " $ ";
						}
						DirectoryInformation+= "**** Files **** $";
						foreach (string file in Directory.GetFiles(reciveMessage))
						{
							DirectoryInformation += "=>" + file + " $ ";
						}
						status = "found";
					}
					else
					{
						DirectoryInformation = "The Directory not found";
					}
					byte[] data = Encoding.UTF8.GetBytes("%_D% "+ status + " $ "+ DirectoryInformation);
					client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add("Client: " +"get this directory info \""+ reciveMessage  + "\"");
						client_listbox.Items.Add("Directory is "+ status +" :Server" );
					});
				}
				else
				{
					// then the send is text
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add("Client: "+reciveMessage);
					});
				}
				client.BeginReceive(message, 0, 1024, SocketFlags.None, new AsyncCallback(AsyncReive), client);
				
			}
			catch (Exception ex)
			{
				Invoke((MethodInvoker)delegate
				{
					connect_status.Text = "Waiting for client...";
					connect_status.ForeColor = Color.Red;
					client_listbox.Items.Clear();
				});
				ServerSocket.BeginAccept(new AsyncCallback(AsyncConnect), ServerSocket);
			}

		}
		private void send_Click(object sender, EventArgs e)
		{
			byte[] data = Encoding.UTF8.GetBytes("%_T%"+send_to_client.Text);
			try
			{
				if (send_to_client.Text.Trim() != "")
				{
					client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(AsyncSend), client);
					Invoke((MethodInvoker)delegate
					{
						client_listbox.Items.Add( send_to_client.Text + " :Server");
						send_to_client.Clear();
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There is no client Connected");
				ServerSocket.BeginAccept(new AsyncCallback(AsyncConnect), ServerSocket);
			}
		}

		void AsyncSend(IAsyncResult air)
		{
			
			Socket client = (Socket)air.AsyncState;
			int numchar = client.EndSend(air);
		}
		private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			// Get the ListBox and the item to draw
			ListBox listBox = (ListBox)sender;
			if (e.Index < 0 || e.Index >= client_listbox.Items.Count)
			{
				return;
			}
			string itemText = listBox.Items[e.Index].ToString();

			// Draw the background
			e.DrawBackground();

			// Create a StringFormat object to specify the alignment
			StringFormat sf = new StringFormat();
			Font itemFont;
			itemFont = new Font(e.Font, FontStyle.Bold);

			Brush itemBrush;
			if (itemText.EndsWith("Server"))
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
		/*
7		private String deCompressFile(string filePath)
		{
			try
			{
				String fileNameWithoutExtension=filePath.Substring(0,filePath.Length-compressExtSize);
				string decompressedFilePath = Path.Combine(myDirectory,fileNameWithoutExtension);

				using (FileStream compressedFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				{
					using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
					{
						using (FileStream decompressedFileStream = new FileStream(decompressedFilePath, FileMode.Create, FileAccess.Write))
						{
							decompressionStream.CopyTo(decompressedFileStream);
						}
					}
				}
				addItem("SENT: DECOMPRESSED SUCCESSFULLY");
				return decompressedFilePath;
			}
			catch (Exception)
			{
				updateStatus("ERROR IN DECOMPRESSING FILE");
				return null;
			}
		} 
		*/











		private void Form1_Load(object sender, EventArgs e)
		{

		}

		
	}
}
