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
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using IrssUtils;

namespace WebRemote
{
  /// <summary>
  /// Setup Form.
  /// </summary>
  internal partial class Setup : Form
  {
    #region Properties

    /// <summary>
    /// Gets or sets the server host.
    /// </summary>
    /// <value>The server host.</value>
    public string ServerHost
    {
      get { return comboBoxComputer.Text; }
      set { comboBoxComputer.Text = value; }
    }

    /// <summary>
    /// Gets or sets the remote skin.
    /// </summary>
    /// <value>The remote skin.</value>
    public string RemoteSkin
    {
      get { return comboBoxSkin.Text; }
      set { comboBoxSkin.Text = value; }
    }

    /// <summary>
    /// Gets or sets the web server port.
    /// </summary>
    /// <value>The web server port.</value>
    public int WebPort
    {
      get { return Decimal.ToInt32(numericUpDownWebPort.Value); }
      set { numericUpDownWebPort.Value = new Decimal(value); }
    }

    /// <summary>
    /// Gets the password hash.
    /// </summary>
    /// <value>The password hash.</value>
    public string PasswordHash
    {
      get
      {
        string text = textBoxPassword.Text;

        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(text));

        return BitConverter.ToString(hash);
      }
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Setup"/> class.
    /// </summary>
    public Setup()
    {
      InitializeComponent();

      UpdateComputerList();

      UpdateSkinList();
    }

    #endregion Constructor

    #region Implementation

    private void UpdateComputerList()
    {
      comboBoxComputer.Items.Clear();
      comboBoxComputer.Items.Add("localhost");

      List<string> networkPCs = Network.GetComputers(false);
      if (networkPCs != null)
        comboBoxComputer.Items.AddRange(networkPCs.ToArray());
    }

    private void UpdateSkinList()
    {
      try
      {
        string[] skins = Directory.GetFiles(Program.SkinsFolder, "*.png", SearchOption.TopDirectoryOnly);
        for (int index = 0; index < skins.Length; index++)
          skins[index] = Path.GetFileNameWithoutExtension(skins[index]);

        comboBoxSkin.Items.Clear();
        comboBoxSkin.Items.AddRange(skins);

        comboBoxSkin.SelectedItem = Program.RemoteSkin;
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex);
      }
    }

    #endregion Implementation

    private void buttonOK_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }
  }
}