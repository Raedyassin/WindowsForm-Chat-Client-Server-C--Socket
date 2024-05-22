namespace Chat_Server
{
	partial class Server
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.connect_status = new System.Windows.Forms.TextBox();
			this.send_to_client = new System.Windows.Forms.TextBox();
			this.send = new System.Windows.Forms.Button();
			this.client_listbox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "From client:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Connect Status:";
			// 
			// connect_status
			// 
			this.connect_status.Location = new System.Drawing.Point(124, 12);
			this.connect_status.Name = "connect_status";
			this.connect_status.Size = new System.Drawing.Size(286, 24);
			this.connect_status.TabIndex = 3;
			// 
			// send_to_client
			// 
			this.send_to_client.Font = new System.Drawing.Font("Tahoma", 10F);
			this.send_to_client.Location = new System.Drawing.Point(12, 350);
			this.send_to_client.Multiline = true;
			this.send_to_client.Name = "send_to_client";
			this.send_to_client.Size = new System.Drawing.Size(619, 52);
			this.send_to_client.TabIndex = 4;
			// 
			// send
			// 
			this.send.Location = new System.Drawing.Point(637, 350);
			this.send.Name = "send";
			this.send.Size = new System.Drawing.Size(151, 52);
			this.send.TabIndex = 5;
			this.send.Text = "Send to client";
			this.send.UseVisualStyleBackColor = true;
			this.send.Click += new System.EventHandler(this.send_Click);
			// 
			// client_listbox
			// 
			this.client_listbox.FormattingEnabled = true;
			this.client_listbox.ItemHeight = 16;
			this.client_listbox.Location = new System.Drawing.Point(18, 61);
			this.client_listbox.Name = "client_listbox";
			this.client_listbox.Size = new System.Drawing.Size(770, 276);
			this.client_listbox.TabIndex = 6;
			// 
			// Server
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.client_listbox);
			this.Controls.Add(this.send);
			this.Controls.Add(this.send_to_client);
			this.Controls.Add(this.connect_status);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Server";
			this.Text = "Chat-Server";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox connect_status;
		private System.Windows.Forms.TextBox send_to_client;
		private System.Windows.Forms.Button send;
		private System.Windows.Forms.ListBox client_listbox;
	}
}

