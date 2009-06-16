using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IrssUtils;
using TrayLauncher.Properties;

namespace TrayLauncher
{
  internal partial class Setup : Form
  {
    #region Variables

    private string _launchKeyCode;
    private OpenFileDialog openFileDialog;

    #endregion Variables

    #region Properties

    public string ServerHost
    {
      get { return comboBoxComputer.Text; }
      set { comboBoxComputer.Text = value; }
    }

    public string ProgramFile
    {
      get { return textBoxApplication.Text; }
      set { textBoxApplication.Text = value; }
    }

    public bool AutoRun
    {
      get { return checkBoxAuto.Checked; }
      set { checkBoxAuto.Checked = value; }
    }

    public bool LaunchOnLoad
    {
      get { return checkBoxLaunchOnLoad.Checked; }
      set { checkBoxLaunchOnLoad.Checked = value; }
    }

    public bool OneInstanceOnly
    {
      get { return checkBoxOneInstance.Checked; }
      set
      {
        checkBoxOneInstance.Checked = value;
        checkBoxRepeatsFocus.Enabled = value;
      }
    }

    public bool RepeatsFocus
    {
      get { return checkBoxRepeatsFocus.Checked; }
      set { checkBoxRepeatsFocus.Checked = value; }
    }

    public string LaunchKeyCode
    {
      get { return _launchKeyCode; }
      set { _launchKeyCode = value; }
    }

    #endregion Properties

    #region Constructor

    public Setup()
    {
      InitializeComponent();

      Icon = Resources.Icon16;

      openFileDialog.Filter = "All files|*.*";
      openFileDialog.Title = "Select Application to Launch";

      comboBoxComputer.Items.Clear();
      comboBoxComputer.Items.Add("localhost");

      List<string> networkPCs = Network.GetComputers(false);
      if (networkPCs != null)
        comboBoxComputer.Items.AddRange(networkPCs.ToArray());
    }

    #endregion Constructor

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(textBoxApplication.Text))
      {
        MessageBox.Show("You must specify an application to launch", "No application", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return;
      }

      DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void buttonFind_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        textBoxApplication.Text = openFileDialog.FileName;
    }

    private void buttonRemoteButton_Click(object sender, EventArgs e)
    {
      if (!Tray.Registered)
      {
        MessageBox.Show(this, "Cannot learn a new launch button without being connected to an active IR Server",
                        "Can't learn button", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      GetKeyCodeForm getKeyCode = new GetKeyCodeForm();
      getKeyCode.ShowDialog(this);

      string keyCode = getKeyCode.KeyCode;

      if (String.IsNullOrEmpty(keyCode))
        return;

      _launchKeyCode = keyCode;
    }

    private void checkBoxOneInstance_CheckedChanged(object sender, EventArgs e)
    {
      checkBoxRepeatsFocus.Enabled = checkBoxOneInstance.Checked;
    }
  }
}