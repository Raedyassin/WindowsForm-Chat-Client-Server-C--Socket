namespace Chat_Client
{
	partial class Form1
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
			this.connect_status = new System.Windows.Forms.TextBox();
			this.connect_button = new System.Windows.Forms.Button();
			this.send_button = new System.Windows.Forms.Button();
			this.disconnect_button = new System.Windows.Forms.Button();
			this.send_text = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.server_listbox = new System.Windows.Forms.ListBox();
			this.combo_send_type = new System.Windows.Forms.ComboBox();
			this.picture_form_server = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picture_form_server)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Connect Status:";
			// 
			// connect_status
			// 
			this.connect_status.Location = new System.Drawing.Point(127, 15);
			this.connect_status.Name = "connect_status";
			this.connect_status.Size = new System.Drawing.Size(213, 24);
			this.connect_status.TabIndex = 2;
			// 
			// connect_button
			// 
			this.connect_button.Location = new System.Drawing.Point(346, 15);
			this.connect_button.Name = "connect_button";
			this.connect_button.Size = new System.Drawing.Size(174, 51);
			this.connect_button.TabIndex = 3;
			this.connect_button.Text = "Connect";
			this.connect_button.UseVisualStyleBackColor = true;
			this.connect_button.Click += new System.EventHandler(this.connect_button_Click_1);
			// 
			// send_button
			// 
			this.send_button.Location = new System.Drawing.Point(547, 384);
			this.send_button.Name = "send_button";
			this.send_button.Size = new System.Drawing.Size(145, 51);
			this.send_button.TabIndex = 4;
			this.send_button.Text = "Send to server";
			this.send_button.UseVisualStyleBackColor = true;
			this.send_button.Click += new System.EventHandler(this.send_button_Click);
			// 
			// disconnect_button
			// 
			this.disconnect_button.Location = new System.Drawing.Point(526, 15);
			this.disconnect_button.Name = "disconnect_button";
			this.disconnect_button.Size = new System.Drawing.Size(166, 51);
			this.disconnect_button.TabIndex = 5;
			this.disconnect_button.Text = "Disconnect";
			this.disconnect_button.UseVisualStyleBackColor = true;
			this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
			// 
			// send_text
			// 
			this.send_text.Font = new System.Drawing.Font("Tahoma", 10F);
			this.send_text.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.send_text.Location = new System.Drawing.Point(16, 384);
			this.send_text.Multiline = true;
			this.send_text.Name = "send_text";
			this.send_text.Size = new System.Drawing.Size(525, 51);
			this.send_text.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "From Server:";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// server_listbox
			// 
			this.server_listbox.Font = new System.Drawing.Font("Tahoma", 8F);
			this.server_listbox.FormattingEnabled = true;
			this.server_listbox.ItemHeight = 16;
			this.server_listbox.Location = new System.Drawing.Point(16, 72);
			this.server_listbox.Name = "server_listbox";
			this.server_listbox.Size = new System.Drawing.Size(676, 308);
			this.server_listbox.TabIndex = 8;
			this.server_listbox.SelectedIndexChanged += new System.EventHandler(this.server_listbox_SelectedIndexChanged);
			// 
			// combo_send_type
			// 
			this.combo_send_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo_send_type.FormattingEnabled = true;
			this.combo_send_type.Items.AddRange(new object[] {
            "Text",
            "Directory",
            "File",
            "Image"});
			this.combo_send_type.Location = new System.Drawing.Point(127, 42);
			this.combo_send_type.Name = "combo_send_type";
			this.combo_send_type.Size = new System.Drawing.Size(213, 24);
			this.combo_send_type.TabIndex = 9;
			this.combo_send_type.Tag = "Text";
			this.combo_send_type.SelectedIndexChanged += new System.EventHandler(this.combo_send_type_SelectedIndexChanged);
			// 
			// picture_form_server
			// 
			this.picture_form_server.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.picture_form_server.Location = new System.Drawing.Point(698, 48);
			this.picture_form_server.Name = "picture_form_server";
			this.picture_form_server.Size = new System.Drawing.Size(460, 392);
			this.picture_form_server.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picture_form_server.TabIndex = 10;
			this.picture_form_server.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(695, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 17);
			this.label3.TabIndex = 11;
			this.label3.Text = "Image from server:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1177, 452);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.picture_form_server);
			this.Controls.Add(this.combo_send_type);
			this.Controls.Add(this.server_listbox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.send_text);
			this.Controls.Add(this.disconnect_button);
			this.Controls.Add(this.send_button);
			this.Controls.Add(this.connect_button);
			this.Controls.Add(this.connect_status);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Chat-Client";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.picture_form_server)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox connect_status;
		private System.Windows.Forms.Button connect_button;
		private System.Windows.Forms.Button send_button;
		private System.Windows.Forms.Button disconnect_button;
		private System.Windows.Forms.TextBox send_text;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox server_listbox;
		private System.Windows.Forms.ComboBox combo_send_type;
		private System.Windows.Forms.PictureBox picture_form_server;
		private System.Windows.Forms.Label label3;
	}
}

