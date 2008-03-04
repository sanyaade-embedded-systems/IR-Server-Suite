using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using IrssUtils;

namespace DboxTuner
{

  partial class SetupForm : Form
  {

    #region Variables

    StbBoxType _boxType = StbBoxType.Unknown;

    #endregion Variables

    #region Properties

    public StbBoxType BoxType
    {
      get { return _boxType; }
      set { _boxType = value; }
    }

    public string Address
    {
      get { return textBoxIpAddress.Text; }
      set { textBoxIpAddress.Text = value; }
    }
    public string UserName
    {
      get { return textBoxUserName.Text; }
      set { textBoxUserName.Text = value; }
    }
    public string Password
    {
      get { return textBoxPassword.Text; }
      set { textBoxPassword.Text = value; }
    }

    #endregion Properties

    #region Constructor

    public SetupForm()
    {
      InitializeComponent();
    }

    #endregion Constructor

    private void InitializeComponent()
    {
      this.buttonGetData = new System.Windows.Forms.Button();
      this.textBoxPassword = new System.Windows.Forms.TextBox();
      this.labelPassword = new System.Windows.Forms.Label();
      this.textBoxUserName = new System.Windows.Forms.TextBox();
      this.labelUserName = new System.Windows.Forms.Label();
      this.textBoxIpAddress = new System.Windows.Forms.TextBox();
      this.labelIpAddress = new System.Windows.Forms.Label();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.buttonDetectBoxType = new System.Windows.Forms.Button();
      this.statusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonGetData
      // 
      this.buttonGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonGetData.Location = new System.Drawing.Point(8, 88);
      this.buttonGetData.Name = "buttonGetData";
      this.buttonGetData.Size = new System.Drawing.Size(104, 24);
      this.buttonGetData.TabIndex = 6;
      this.buttonGetData.Text = "Get channel list";
      this.buttonGetData.UseVisualStyleBackColor = true;
      this.buttonGetData.Click += new System.EventHandler(this.buttonGetData_Click);
      // 
      // textBoxPassword
      // 
      this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxPassword.Location = new System.Drawing.Point(88, 56);
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.Size = new System.Drawing.Size(176, 20);
      this.textBoxPassword.TabIndex = 5;
      // 
      // labelPassword
      // 
      this.labelPassword.Location = new System.Drawing.Point(8, 56);
      this.labelPassword.Name = "labelPassword";
      this.labelPassword.Size = new System.Drawing.Size(80, 20);
      this.labelPassword.TabIndex = 4;
      this.labelPassword.Text = "Password:";
      this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textBoxUserName
      // 
      this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxUserName.Location = new System.Drawing.Point(88, 32);
      this.textBoxUserName.Name = "textBoxUserName";
      this.textBoxUserName.Size = new System.Drawing.Size(176, 20);
      this.textBoxUserName.TabIndex = 3;
      // 
      // labelUserName
      // 
      this.labelUserName.Location = new System.Drawing.Point(8, 32);
      this.labelUserName.Name = "labelUserName";
      this.labelUserName.Size = new System.Drawing.Size(80, 20);
      this.labelUserName.TabIndex = 2;
      this.labelUserName.Text = "User name:";
      this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textBoxIpAddress
      // 
      this.textBoxIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxIpAddress.Location = new System.Drawing.Point(88, 8);
      this.textBoxIpAddress.Name = "textBoxIpAddress";
      this.textBoxIpAddress.Size = new System.Drawing.Size(176, 20);
      this.textBoxIpAddress.TabIndex = 1;
      // 
      // labelIpAddress
      // 
      this.labelIpAddress.Location = new System.Drawing.Point(8, 8);
      this.labelIpAddress.Name = "labelIpAddress";
      this.labelIpAddress.Size = new System.Drawing.Size(80, 20);
      this.labelIpAddress.TabIndex = 0;
      this.labelIpAddress.Text = "IP address:";
      this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(128, 120);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(64, 24);
      this.buttonOK.TabIndex = 7;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(200, 120);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(64, 24);
      this.buttonCancel.TabIndex = 8;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
      this.statusStrip.Location = new System.Drawing.Point(0, 153);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(272, 22);
      this.statusStrip.TabIndex = 9;
      // 
      // toolStripStatusLabel
      // 
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
      // 
      // buttonDetectBoxType
      // 
      this.buttonDetectBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonDetectBoxType.Location = new System.Drawing.Point(8, 120);
      this.buttonDetectBoxType.Name = "buttonDetectBoxType";
      this.buttonDetectBoxType.Size = new System.Drawing.Size(104, 24);
      this.buttonDetectBoxType.TabIndex = 10;
      this.buttonDetectBoxType.Text = "Redetect box";
      this.buttonDetectBoxType.UseVisualStyleBackColor = true;
      this.buttonDetectBoxType.Click += new System.EventHandler(this.buttonDetectBoxType_Click);
      // 
      // SetupForm
      // 
      this.AcceptButton = this.buttonOK;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(272, 175);
      this.Controls.Add(this.buttonDetectBoxType);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.buttonGetData);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.labelIpAddress);
      this.Controls.Add(this.textBoxIpAddress);
      this.Controls.Add(this.labelUserName);
      this.Controls.Add(this.textBoxUserName);
      this.Controls.Add(this.textBoxPassword);
      this.Controls.Add(this.labelPassword);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(278, 200);
      this.Name = "SetupForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Dbox Tuner Setup";
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private void buttonGetData_Click(object sender, EventArgs e)
    {
      StatusMessage("Attempting to read channel list ...");

      try
      {
        string url = Program.UrlPrefix + Address;

        // Detect box type ...
        if (BoxType == StbBoxType.Unknown)
          BoxType = Program.DetectBoxType(url, UserName, Password);

        if (BoxType == StbBoxType.Unknown)
        {
          StatusMessage("ERROR - No STB or unknown type detected!");
        }
        else
        {
          StatusMessage("Detected box type: {0}", _boxType);

          DataTable bouquets = Program.GetData(url, UserName, Password, BoxType).Tables[0];
          
          if (bouquets.Rows.Count != 0)
            StatusMessage("{0} channels found", bouquets.Rows.Count);
          else
            StatusMessage("ERROR - No channels found!");

          if (File.Exists(Program.DataFile))
            File.Delete(Program.DataFile);

          bouquets.WriteXml(Program.DataFile, XmlWriteMode.WriteSchema);
        }
      }
      catch (Exception ex)
      {
        StatusMessage("ERROR - Cannot read bouquet!");
        IrssLog.Error(ex);

        MessageBox.Show(this, ex.ToString(), "Dbox Tuner - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    
    void StatusMessage(string format, params object[] args)
    {
      string message = String.Format(format, args);
      toolStripStatusLabel.Text = message;
      statusStrip.Update();

      if (message.StartsWith("ERROR", StringComparison.OrdinalIgnoreCase))
        IrssLog.Error(message);
      else
        IrssLog.Info(message);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonDetectBoxType_Click(object sender, EventArgs e)
    {
      BoxType = Program.DetectBoxType(Program.UrlPrefix + Address, UserName, Password);
    }

  }

}