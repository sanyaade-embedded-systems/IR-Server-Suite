#region Copyright (C) 2005-2009 Team MediaPortal

// Copyright (C) 2005-2009 Team MediaPortal
// http://www.team-mediaportal.com
// 
// This Program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2, or (at your option)
// any later version.
// 
// This Program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with GNU Make; see the file COPYING.  If not, write to
// the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
// http://www.gnu.org/copyleft/gpl.html

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using IrssUtils;
using IrssUtils.Forms;

namespace Translator
{
  internal partial class ButtonMappingForm : Form
  {
    #region Constants

    private const string Parameters =
      @"\a = Alert (ascii 7)
\b = Backspace (ascii 8)
\f = Form Feed (ascii 12)
\n = Line Feed (ascii 10)
\r = Carriage Return (ascii 13)
\t = Tab (ascii 9)
\v = Vertical Tab (ascii 11)
\x = Hex Value (\x0Fh = ascii char 15, \x8h = ascii char 8)
\0 = Null (ascii 0)";

    #endregion Constants

    #region Variables

    private readonly string _keyCode;
    private string _command;
    private string _description;

    private LearnIR _learnIR;

    #endregion Variables

    #region Properties

    internal string KeyCode
    {
      get { return _keyCode; }
    }

    internal string Description
    {
      get { return _description; }
    }

    internal string Command
    {
      get { return _command; }
    }

    #endregion Properties

    #region Constructors

    public ButtonMappingForm(string keyCode, string description, string command)
    {
      InitializeComponent();
      SetImages();

      _keyCode = keyCode;
      _description = description;
      _command = command;
    }

    #endregion Constructors

    private void SetImages()
    {
      this.checkBoxMouseScrollDown.Image = IrssUtils.Properties.Resources.ScrollDown;
      this.checkBoxMouseScrollUp.Image = IrssUtils.Properties.Resources.ScrollUp;
      this.checkBoxMouseClickRight.Image = IrssUtils.Properties.Resources.ClickRight;
      this.checkBoxMouseClickMiddle.Image = IrssUtils.Properties.Resources.ClickMiddle;
      this.checkBoxMouseClickLeft.Image = IrssUtils.Properties.Resources.ClickLeft;
      this.checkBoxMouseMoveLeft.Image = IrssUtils.Properties.Resources.MoveLeft;
      this.checkBoxMouseMoveDown.Image = IrssUtils.Properties.Resources.MoveDown;
      this.checkBoxMouseMoveRight.Image = IrssUtils.Properties.Resources.MoveRight;
      this.checkBoxMouseMoveUp.Image = IrssUtils.Properties.Resources.MoveUp;
    }

    private void SetupIRList()
    {
      comboBoxIRCode.Items.Clear();

      string[] irList = Common.GetIRList(false);
      if (irList != null && irList.Length > 0)
      {
        comboBoxIRCode.Items.AddRange(irList);
        comboBoxIRCode.SelectedIndex = 0;
      }
    }

    private void SetupMacroList()
    {
      comboBoxMacro.Items.Clear();

      string[] macroList = IrssMacro.GetMacroList(Program.FolderMacros, false);
      if (macroList != null && macroList.Length > 0)
      {
        comboBoxMacro.Items.AddRange(macroList);
        comboBoxMacro.SelectedIndex = 0;
      }
    }

    private void ButtonMappingForm_Load(object sender, EventArgs e)
    {
      textBoxKeyCode.Text = _keyCode;
      textBoxButtonDesc.Text = _description;
      textBoxCommand.Text = _command;

      // Setup IR Blast tab
      SetupIRList();

      // Setup macro tab
      SetupMacroList();

      comboBoxPort.Items.Clear();
      comboBoxPort.Items.AddRange(Program.TransceiverInformation.Ports);
      if (comboBoxPort.Items.Count > 0)
        comboBoxPort.SelectedIndex = 0;

      // Setup Serial tab
      comboBoxComPort.Items.Clear();
      comboBoxComPort.Items.AddRange(SerialPort.GetPortNames());
      if (comboBoxComPort.Items.Count > 0)
        comboBoxComPort.SelectedIndex = 0;

      comboBoxParity.Items.Clear();
      comboBoxParity.Items.AddRange(Enum.GetNames(typeof (Parity)));
      comboBoxParity.SelectedIndex = 0;

      comboBoxStopBits.Items.Clear();
      comboBoxStopBits.Items.AddRange(Enum.GetNames(typeof (StopBits)));
      comboBoxStopBits.SelectedIndex = 1;

      // Setup Run tab
      comboBoxWindowStyle.Items.Clear();
      comboBoxWindowStyle.Items.AddRange(Enum.GetNames(typeof (ProcessWindowStyle)));
      comboBoxWindowStyle.SelectedIndex = 0;

      // Setup Windows Message tab
      radioButtonActiveWindow.Checked = true;

      // Setup Misc tab
      comboBoxMiscCommand.Items.Clear();
      comboBoxMiscCommand.Items.Add(Common.UITextTranslator);
      comboBoxMiscCommand.Items.Add(Common.UITextVirtualKB);
      comboBoxMiscCommand.Items.Add(Common.UITextSmsKB);
      comboBoxMiscCommand.Items.Add(Common.UITextTcpMsg);
      comboBoxMiscCommand.Items.Add(Common.UITextHttpMsg);
      comboBoxMiscCommand.Items.Add(Common.UITextEject);
      comboBoxMiscCommand.Items.Add(Common.UITextPopup);
      comboBoxMiscCommand.Items.Add(Common.UITextStandby);
      comboBoxMiscCommand.Items.Add(Common.UITextHibernate);
      comboBoxMiscCommand.Items.Add(Common.UITextReboot);
      comboBoxMiscCommand.Items.Add(Common.UITextShutdown);
      comboBoxMiscCommand.Items.Add(Common.UITextBeep);
      comboBoxMiscCommand.Items.Add(Common.UITextSound);

      if (!String.IsNullOrEmpty(_command))
      {
        string prefix = _command;
        string suffix = String.Empty;

        int find = _command.IndexOf(": ", StringComparison.OrdinalIgnoreCase);

        if (find != -1)
        {
          prefix = _command.Substring(0, find + 2);
          suffix = _command.Substring(find + 2);
        }

        switch (prefix)
        {
          case Common.CmdPrefixBlast:
            {
              string[] commands = Common.SplitBlastCommand(suffix);

              tabControl.SelectTab(tabPageBlastIR);
              comboBoxIRCode.SelectedItem = commands[0];
              comboBoxPort.SelectedItem = commands[1];
              break;
            }

          case Common.CmdPrefixMacro:
            {
              tabControl.SelectTab(tabPageMacro);
              comboBoxMacro.SelectedItem = suffix;
              break;
            }

          case Common.CmdPrefixRun:
            {
              string[] commands = Common.SplitRunCommand(suffix);

              tabControl.SelectTab(tabPageProgram);
              textBoxApp.Text = commands[0];
              textBoxAppStartFolder.Text = commands[1];
              textBoxApplicationParameters.Text = commands[2];
              comboBoxWindowStyle.SelectedItem = commands[3];
              checkBoxNoWindow.Checked = bool.Parse(commands[4]);
              checkBoxShellExecute.Checked = bool.Parse(commands[5]);
              break;
            }

          case Common.CmdPrefixSerial:
            {
              string[] commands = Common.SplitSerialCommand(suffix);

              tabControl.SelectTab(tabPageSerial);
              textBoxSerialCommand.Text = commands[0];
              comboBoxComPort.SelectedItem = commands[1];
              numericUpDownBaudRate.Value = decimal.Parse(commands[2]);
              comboBoxParity.SelectedItem = commands[3];
              numericUpDownDataBits.Value = decimal.Parse(commands[4]);
              comboBoxStopBits.SelectedItem = commands[5];
              checkBoxWaitForResponse.Checked = bool.Parse(commands[6]);

              break;
            }

          case Common.CmdPrefixWindowMsg:
            {
              string[] commands = Common.SplitWindowMessageCommand(suffix);

              tabControl.SelectTab(tabPageMessage);
              switch (commands[0].ToUpperInvariant())
              {
                case Common.TargetActive:
                  radioButtonActiveWindow.Checked = true;
                  break;
                case Common.TargetApplication:
                  radioButtonApplication.Checked = true;
                  break;
                case Common.TargetClass:
                  radioButtonClass.Checked = true;
                  break;
                case Common.TargetWindow:
                  radioButtonWindowTitle.Checked = true;
                  break;
              }

              textBoxMsgTarget.Text = commands[1];
              numericUpDownMsg.Value = decimal.Parse(commands[2]);
              numericUpDownWParam.Value = decimal.Parse(commands[3]);
              numericUpDownLParam.Value = decimal.Parse(commands[4]);
              break;
            }

          case Common.CmdPrefixKeys:
            {
              tabControl.SelectTab(tabPageKeystrokes);
              keystrokeCommandPanel.CommandString = suffix;
              break;
            }

          case Common.CmdPrefixMouse:
            {
              tabControl.SelectTab(tabPageMouse);
              switch (suffix)
              {
                case Common.MouseClickLeft:
                  checkBoxMouseClickLeft.Checked = true;
                  break;
                case Common.MouseClickMiddle:
                  checkBoxMouseClickMiddle.Checked = true;
                  break;
                case Common.MouseClickRight:
                  checkBoxMouseClickRight.Checked = true;
                  break;
                case Common.MouseScrollDown:
                  checkBoxMouseScrollDown.Checked = true;
                  break;
                case Common.MouseScrollUp:
                  checkBoxMouseScrollUp.Checked = true;
                  break;

                default:
                  if (suffix.StartsWith(Common.MouseMoveDown, StringComparison.OrdinalIgnoreCase))
                    checkBoxMouseMoveDown.Checked = true;
                  else if (suffix.StartsWith(Common.MouseMoveLeft, StringComparison.OrdinalIgnoreCase))
                    checkBoxMouseMoveLeft.Checked = true;
                  else if (suffix.StartsWith(Common.MouseMoveRight, StringComparison.OrdinalIgnoreCase))
                    checkBoxMouseMoveRight.Checked = true;
                  else if (suffix.StartsWith(Common.MouseMoveUp, StringComparison.OrdinalIgnoreCase))
                    checkBoxMouseMoveUp.Checked = true;

                  numericUpDownMouseMove.Value = Decimal.Parse(suffix.Substring(suffix.IndexOf(' ')));
                  break;
              }
              break;
            }

          default:
            {
              tabControl.SelectTab(tabPageMisc);
              switch (_command)
              {
                case Common.CmdPrefixHibernate:
                  comboBoxMiscCommand.SelectedItem = Common.UITextHibernate;
                  break;

                case Common.CmdPrefixReboot:
                  comboBoxMiscCommand.SelectedItem = Common.UITextReboot;
                  break;

                case Common.CmdPrefixShutdown:
                  comboBoxMiscCommand.SelectedItem = Common.UITextShutdown;
                  break;

                case Common.CmdPrefixStandby:
                  comboBoxMiscCommand.SelectedItem = Common.UITextStandby;
                  break;

                case Common.CmdPrefixTranslator:
                  comboBoxMiscCommand.SelectedItem = Common.UITextTranslator;
                  break;

                case Common.CmdPrefixVirtualKB:
                  comboBoxMiscCommand.SelectedItem = Common.UITextVirtualKB;
                  break;

                case Common.CmdPrefixSmsKB:
                  comboBoxMiscCommand.SelectedItem = Common.UITextSmsKB;
                  break;

                default:
                  if (prefix.Equals(Common.CmdPrefixEject, StringComparison.OrdinalIgnoreCase))
                    comboBoxMiscCommand.SelectedItem = Common.UITextEject;
                  break;
              }
              break;
            }
        }
      }
    }

    #region Controls

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(_keyCode))
      {
        MessageBox.Show(this, "You must provide a valid button key code to create a button mapping", "KeyCode Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBoxKeyCode.Focus();
        return;
      }

      if (String.IsNullOrEmpty(_command))
      {
        MessageBox.Show(this, "You must click SET to confirm the command you want to assign to this button mapping",
                        "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        buttonSet.Focus();
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

    private void buttonParamQuestion_Click(object sender, EventArgs e)
    {
      MessageBox.Show(this, Parameters, "Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void buttonSet_Click(object sender, EventArgs e)
    {
      switch (tabControl.SelectedTab.Name)
      {
        case "tabPageBlastIR":
          {
            textBoxCommand.Text = _command =
                                  String.Format("{0}{1}|{2}",
                                                Common.CmdPrefixBlast,
                                                comboBoxIRCode.SelectedItem as string,
                                                comboBoxPort.SelectedItem as string);
            break;
          }

        case "tabPageMacro":
          {
            textBoxCommand.Text = _command = Common.CmdPrefixMacro + comboBoxMacro.SelectedItem;
            break;
          }

        case "tabPageSerial":
          {
            textBoxCommand.Text = _command =
                                  String.Format("{0}{1}|{2}|{3}|{4}|{5}|{6}|{7}",
                                                Common.CmdPrefixSerial,
                                                textBoxSerialCommand.Text,
                                                comboBoxComPort.SelectedItem as string,
                                                numericUpDownBaudRate.Value,
                                                comboBoxParity.SelectedItem as string,
                                                numericUpDownDataBits.Value,
                                                comboBoxStopBits.SelectedItem as string,
                                                checkBoxWaitForResponse.Checked);
            break;
          }

        case "tabPageProgram":
          {
            textBoxCommand.Text = _command =
                                  String.Format("{0}{1}|{2}|{3}|{4}|{5}|{6}|False|{7}",
                                                Common.CmdPrefixRun,
                                                textBoxApp.Text,
                                                textBoxAppStartFolder.Text,
                                                textBoxApplicationParameters.Text,
                                                comboBoxWindowStyle.SelectedItem as string,
                                                checkBoxNoWindow.Checked,
                                                checkBoxShellExecute.Checked,
                                                checkBoxForceFocus.Checked);
            break;
          }

        case "tabPageMessage":
          {
            string target = "ERROR";

            if (radioButtonActiveWindow.Checked)
            {
              target = Common.TargetActive;
              textBoxMsgTarget.Text = "*";
            }
            else if (radioButtonApplication.Checked)
            {
              target = Common.TargetApplication;
            }
            else if (radioButtonClass.Checked)
            {
              target = Common.TargetClass;
            }
            else if (radioButtonWindowTitle.Checked)
            {
              target = Common.TargetWindow;
            }

            textBoxCommand.Text = _command =
                                  String.Format("{0}{1}|{2}|{3}|{4}|{5}",
                                                Common.CmdPrefixWindowMsg,
                                                target,
                                                textBoxMsgTarget.Text,
                                                numericUpDownMsg.Value,
                                                numericUpDownWParam.Value,
                                                numericUpDownLParam.Value);
            break;
          }

        case "tabPageKeystrokes":
          {
            textBoxCommand.Text = _command = Common.CmdPrefixKeys + keystrokeCommandPanel.CommandString;
            break;
          }

        case "tabPageMouse":
          {
            StringBuilder newCommand = new StringBuilder();
            newCommand.Append(Common.CmdPrefixMouse);

            if (checkBoxMouseClickLeft.Checked) newCommand.Append(Common.MouseClickLeft);
            else if (checkBoxMouseClickRight.Checked) newCommand.Append(Common.MouseClickRight);
            else if (checkBoxMouseClickMiddle.Checked) newCommand.Append(Common.MouseClickMiddle);
            else if (checkBoxMouseScrollUp.Checked) newCommand.Append(Common.MouseScrollUp);
            else if (checkBoxMouseScrollDown.Checked) newCommand.Append(Common.MouseScrollDown);
            else
            {
              if (checkBoxMouseMoveUp.Checked) newCommand.Append(Common.MouseMoveUp);
              else if (checkBoxMouseMoveDown.Checked) newCommand.Append(Common.MouseMoveDown);
              else if (checkBoxMouseMoveLeft.Checked) newCommand.Append(Common.MouseMoveLeft);
              else if (checkBoxMouseMoveRight.Checked) newCommand.Append(Common.MouseMoveRight);
              else break;

              newCommand.Append(numericUpDownMouseMove.Value.ToString());
            }

            textBoxCommand.Text = _command = newCommand.ToString();
            break;
          }

        case "tabPageMisc":
          {
            switch (comboBoxMiscCommand.SelectedItem as string)
            {
              case Common.UITextTranslator:
                textBoxCommand.Text = _command = Common.CmdPrefixTranslator;
                break;

              case Common.UITextVirtualKB:
                textBoxCommand.Text = _command = Common.CmdPrefixVirtualKB;
                break;

              case Common.UITextSmsKB:
                textBoxCommand.Text = _command = Common.CmdPrefixSmsKB;
                break;

              case Common.UITextTcpMsg:
                TcpMessageCommand tcpMessageCommand = new TcpMessageCommand();
                if (tcpMessageCommand.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixTcpMsg + tcpMessageCommand.CommandString;
                break;

              case Common.UITextHttpMsg:
                HttpMessageCommand httpMessageCommand = new HttpMessageCommand();
                if (httpMessageCommand.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixHttpMsg + httpMessageCommand.CommandString;
                break;

              case Common.UITextEject:
                EjectCommand ejectCommand = new EjectCommand();
                if (ejectCommand.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixEject + ejectCommand.CommandString;
                break;

              case Common.UITextPopup:
                PopupMessage popupMessage = new PopupMessage();
                if (popupMessage.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixPopup + popupMessage.CommandString;
                break;

              case Common.UITextStandby:
                textBoxCommand.Text = _command = Common.CmdPrefixStandby;
                break;

              case Common.UITextHibernate:
                textBoxCommand.Text = _command = Common.CmdPrefixHibernate;
                break;

              case Common.UITextReboot:
                textBoxCommand.Text = _command = Common.CmdPrefixReboot;
                break;

              case Common.UITextShutdown:
                textBoxCommand.Text = _command = Common.CmdPrefixShutdown;
                break;

              case Common.UITextBeep:
                BeepCommand beepCommand = new BeepCommand();
                if (beepCommand.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixBeep + beepCommand.CommandString;
                break;

              case Common.UITextSound:
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Wave Files|*.wav";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                  textBoxCommand.Text = Common.CmdPrefixSound + openFileDialog.FileName;
                break;
            }

            break;
          }
      }
    }

    private void buttonTest_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(_command))
      {
        MessageBox.Show(this, "You must Set the command before you can Test it", "No command Set", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        buttonSet.Focus();
        return;
      }

      if (_command.StartsWith(Common.CmdPrefixKeys, StringComparison.OrdinalIgnoreCase))
      {
        MessageBox.Show(this, "Keystroke commands cannot be tested here", "Cannot test Keystroke command",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      else
      {
        try
        {
          Program.ProcessCommand(_command, false);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, ex.ToString(), "Test failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void buttonFindMsgTarget_Click(object sender, EventArgs e)
    {
      if (radioButtonApplication.Checked)
      {
        OpenFileDialog find = new OpenFileDialog();
        find.Filter = "All files|*.*";
        find.Multiselect = false;
        find.Title = "Application to send message to";

        if (find.ShowDialog(this) == DialogResult.OK)
          textBoxMsgTarget.Text = find.FileName;
      }
      else if (radioButtonClass.Checked)
      {
        WindowList windowList = new WindowList(true);
        if (windowList.ShowDialog(this) == DialogResult.OK)
          textBoxMsgTarget.Text = windowList.SelectedItem;
      }
      else if (radioButtonWindowTitle.Checked)
      {
        WindowList windowList = new WindowList(false);
        if (windowList.ShowDialog(this) == DialogResult.OK)
          textBoxMsgTarget.Text = windowList.SelectedItem;
      }
    }

    private void radioButtonActiveWindow_CheckedChanged(object sender, EventArgs e)
    {
      buttonFindMsgTarget.Enabled = false;
      textBoxMsgTarget.Enabled = false;
    }

    private void radioButtonApplication_CheckedChanged(object sender, EventArgs e)
    {
      buttonFindMsgTarget.Enabled = true;
      textBoxMsgTarget.Enabled = true;
    }

    private void radioButtonClass_CheckedChanged(object sender, EventArgs e)
    {
      buttonFindMsgTarget.Enabled = true;
      textBoxMsgTarget.Enabled = true;
    }

    private void radioButtonWindowTitle_CheckedChanged(object sender, EventArgs e)
    {
      buttonFindMsgTarget.Enabled = true;
      textBoxMsgTarget.Enabled = true;
    }

    private void buttonLocate_Click(object sender, EventArgs e)
    {
      OpenFileDialog find = new OpenFileDialog();
      find.Filter = "All files|*.*";
      find.Multiselect = false;
      find.Title = "Application to launch";

      if (find.ShowDialog(this) == DialogResult.OK)
      {
        textBoxApp.Text = find.FileName;
        if (String.IsNullOrEmpty(textBoxAppStartFolder.Text))
          textBoxAppStartFolder.Text = Path.GetDirectoryName(find.FileName);
      }
    }

    private void buttonStartupFolder_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog find = new FolderBrowserDialog();
      find.Description = "Please specify the starting folder for the application";
      find.ShowNewFolderButton = true;
      if (find.ShowDialog(this) == DialogResult.OK)
        textBoxAppStartFolder.Text = find.SelectedPath;
    }

    private void buttonLearnIR_Click(object sender, EventArgs e)
    {
      _learnIR = new LearnIR(
        Program.LearnIR,
        Program.BlastIR,
        Program.TransceiverInformation.Ports);

      _learnIR.ShowDialog(this);

      _learnIR = null;

      SetupIRList();
    }

    private void buttonNewMacro_Click(object sender, EventArgs e)
    {
      MacroEditor macroEditor = new MacroEditor();
      macroEditor.ShowDialog(this);

      SetupMacroList();
    }

    private void textBoxButtonDesc_TextChanged(object sender, EventArgs e)
    {
      _description = textBoxButtonDesc.Text;
    }

    private void checkBoxMouse_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox origin = (CheckBox) sender;

      if (!origin.Checked)
        return;

      if (origin != checkBoxMouseClickLeft) checkBoxMouseClickLeft.Checked = false;
      if (origin != checkBoxMouseClickRight) checkBoxMouseClickRight.Checked = false;
      if (origin != checkBoxMouseClickMiddle) checkBoxMouseClickMiddle.Checked = false;
      if (origin != checkBoxMouseMoveUp) checkBoxMouseMoveUp.Checked = false;
      if (origin != checkBoxMouseMoveDown) checkBoxMouseMoveDown.Checked = false;
      if (origin != checkBoxMouseMoveLeft) checkBoxMouseMoveLeft.Checked = false;
      if (origin != checkBoxMouseMoveRight) checkBoxMouseMoveRight.Checked = false;
      if (origin != checkBoxMouseScrollUp) checkBoxMouseScrollUp.Checked = false;
      if (origin != checkBoxMouseScrollDown) checkBoxMouseScrollDown.Checked = false;
    }

    #endregion Controls
  }
}