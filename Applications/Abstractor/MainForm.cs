using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using IrssComms;
using IrssUtils;

namespace Abstractor
{

  public partial class MainForm : Form
  {

    #region Constants

    /*
    static readonly string[] AbstractButtons = new string[] {

      // Primary buttons ...
      "Up",
      "Down",
      "Left",
      "Right",
      "OK",      
      "Volume Up",
      "Volume Down",
      "Channel Up",
      "Channel Down",      
      "Start",
      "Back",
      "Info",
      "Mute",
      "Number 0",
      "Number 1",
      "Number 2",
      "Number 3",
      "Number 4",
      "Number 5",
      "Number 6",
      "Number 7",
      "Number 8",
      "Number 9",
      "Play",
      "Pause",
      "Play / Pause",
      "Stop",
      "Fast Forward",
      "Rewind",
      "Record",
      "Next Chapter",
      "Previous Chapter",
      "Power",
      
      // Secondary buttons ...
      "Power 2",
      "Power 3",
      "Teletext",
      "Teletext Red",
      "Teletext Green",
      "Teletext Yellow",
      "Teletext Blue",
      "Subtitles",
      "Menu",
      "Clear",
      "Enter",
      "#",
      "*",
      "Task Swap",
      "Fullscreen",
      "Aspect Ratio",
      "Setup",
      "Music",
      "Pictures",
      "Videos",
      "DVD",
      "TV",
      "Guide",
      "Live TV",
      "Radio",
      "Print",
      "Snapshot",
      "Open",
      "Close",
      "Eject",
      "Scroll Up",
      "Scroll Down",
      "Page Up",
      "Page Down"
    };*/

    static readonly string AbstractRemoteMapFolder = Path.Combine(Common.FolderAppData, "Input Service\\Abstract Remote Maps");
    static readonly string AbstractRemoteSchemaFile = Path.Combine(Common.FolderAppData, "Input Service\\Abstract Remote Maps\\RemoteTable.xsd");

    #endregion Constants

    #region Enumerations

    public enum AbstractButton
    {
      Up,
      Down,
      Left,
      Right,
      OK,
      VolumeUp,
      VolumeDown,
      ChannelUp,
      ChannelDown,
      Start,
      Back,
      Info,
      Mute,
      Number0,
      Number1,
      Number2,
      Number3,
      Number4,
      Number5,
      Number6,
      Number7,
      Number8,
      Number9,
      Play,
      Pause,
      PlayPause,
      Stop,
      FastForward,
      Rewind,
      Record,
      NextChapter,
      PreviousChapter,
      Power,
      Power2,
      Power3,
      Teletext,
      Red,
      Green,
      Yellow,
      Blue,
      Subtitles,
      Menu,
      Clear,
      Enter,
      Hash,
      Star,
      TaskSwap,
      Fullscreen,
      AspectRatio,
      Setup,
      Music,
      Pictures,
      Videos,
      DVD,
      TV,
      Guide,
      LiveTV,
      Radio,
      Print,
      Snapshot,
      Open,
      Close,
      Eject,
      ScrollUp,
      ScrollDown,
      PageUp,
      PageDown,
    }

    #endregion Enumerations

    #region Variables

    Client _client;

    string _serverHost = "localhost";

    bool _registered;

    //IRServerInfo _irServerInfo = new IRServerInfo();

    string[] _devices;
    string _selectedDevice;

    #endregion Variables

    delegate void DelegateAddStatusLine(string status);
    DelegateAddStatusLine _addStatusLine;
    void AddStatusLine(string status)
    {
      IrssLog.Info(status);

      listBoxStatus.Items.Add(status);

      listBoxStatus.SetSelected(listBoxStatus.Items.Count - 1, true);
    }


    delegate void DelegateSetDevices(string[] devices);
    DelegateSetDevices _setDevices;
    void SetDevices(string[] devices)
    {
      _devices = devices;

      comboBoxDevice.Items.Clear();
      comboBoxDevice.Items.AddRange(devices);
      comboBoxDevice.SelectedIndex = 0;

      if (String.IsNullOrEmpty(textBoxRemoteName.Text))
        textBoxRemoteName.Text = devices[0];
    }

    #region Constructor

    public MainForm()
    {
      IrssLog.LogLevel = IrssLog.Level.Debug;
      IrssLog.Open("Abstractor.log");

      InitializeComponent();

      _addStatusLine = new DelegateAddStatusLine(AddStatusLine);
      _setDevices = new DelegateSetDevices(SetDevices);

      comboBoxComputer.Items.Clear();
      comboBoxComputer.Items.Add("localhost");

      List<string> networkPCs = Network.GetComputers(false);
      if (networkPCs != null)
        comboBoxComputer.Items.AddRange(networkPCs.ToArray());

      comboBoxComputer.Text = _serverHost;

      ClearMap();
      /*
      DataTable table = new DataTable("RemoteTable");

      DataColumn column;

      column = new DataColumn("RawCode", typeof(string));
      column.Caption = "Raw Code";
      column.ColumnMapping = MappingType.Attribute;
      column.DefaultValue = String.Empty;
      column.ReadOnly = false;
      column.Unique = true;
      table.Columns.Add(column);

      column = new DataColumn("AbstractButton", typeof(string));
      column.Caption = "Abstract Button";
      column.ColumnMapping = MappingType.Attribute;
      column.DefaultValue = String.Empty;
      column.ReadOnly = false;
      column.Unique = false;
      table.Columns.Add(column);

      string[] names = Enum.GetNames(typeof(MceButton));
      foreach (string name in names)
      {
        int button = (int)Enum.Parse(typeof(MceButton), name);

        table.Rows.Add(button.ToString(), name);
      }

      //table.WriteXmlSchema(AbstractRemoteSchemaFile);

      table.WriteXml("Template.xml");
      */
    }

    #endregion Constructor


    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      buttonDisconnect_Click(null, null);

      _addStatusLine = null;
      _setDevices = null;

      IrssLog.Close();
    }


    void ReceivedMessage(IrssMessage received)
    {
      this.Invoke(_addStatusLine, new Object[] { String.Format("Received Message: \"{0}, {1}\"", received.Type, received.Flags) });

      try
      {
        switch (received.Type)
        {
          case MessageType.RegisterClient:
            if ((received.Flags & MessageFlags.Success) == MessageFlags.Success)
            {
              _registered = true;
              //_irServerInfo = IRServerInfo.FromBytes(received.GetDataAsBytes());

              _client.Send(new IrssMessage(MessageType.ActiveReceivers, MessageFlags.Request));
              _client.Send(new IrssMessage(MessageType.ActiveBlasters, MessageFlags.Request));
            }
            else if ((received.Flags & MessageFlags.Failure) == MessageFlags.Failure)
            {
              _registered = false;
            }
            return;

          case MessageType.ActiveBlasters:
            this.Invoke(_addStatusLine, new Object[] { received.GetDataAsString() });
            break;

          case MessageType.ActiveReceivers:
            this.Invoke(_addStatusLine, new Object[] { received.GetDataAsString() });

            string[] receivers = received.GetDataAsString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            this.Invoke(_setDevices, new object[] { receivers });
            break;

          case MessageType.RemoteEvent:
            byte[] data = received.GetDataAsBytes();
            int deviceNameSize = BitConverter.ToInt32(data, 0);
            string deviceName = Encoding.ASCII.GetString(data, 4, deviceNameSize);
            int keyCodeSize = BitConverter.ToInt32(data, 4 + deviceNameSize);
            string keyCode = Encoding.ASCII.GetString(data, 8 + deviceNameSize, keyCodeSize);

            RemoteHandlerCallback(deviceName, keyCode);
            return;

          case MessageType.ServerShutdown:
            _registered = false;
            return;

          case MessageType.Error:
            this.Invoke(_addStatusLine, new Object[] { received.GetDataAsString() });
            return;
        }
      }
      catch (Exception ex)
      {
        this.Invoke(_addStatusLine, new Object[] { ex.Message });
      }
    }

    void RemoteHandlerCallback(string deviceName, string keyCode)
    {
      string text = String.Format("Remote Event \"{0}\", \"{1}\"", deviceName, keyCode);
      this.Invoke(_addStatusLine, text);

      bool foundDevice = false;
      foreach (string device in _devices)
      {
        if (device.Equals(deviceName, StringComparison.OrdinalIgnoreCase))
        {
          foundDevice = true;
          break;
        }
      }

      if (!foundDevice)
      {
        List<string> newDevices = new List<string>(_devices);
        newDevices.Add(deviceName);
        this.Invoke(_setDevices, new object[] { newDevices.ToArray() });
      }

      // If this remote event matches the criteria then set it to an abstract button in the list view ...
      if (deviceName.Equals(_selectedDevice, StringComparison.OrdinalIgnoreCase))
      {
        if (listViewButtonMap.SelectedItems.Count == 1)
        {
          bool found = false;
          foreach (ListViewItem item in listViewButtonMap.Items)
            if (item.SubItems[1].Text.Equals(keyCode, StringComparison.OrdinalIgnoreCase))
              found = true;

          if (!found)
          {
            int index = listViewButtonMap.SelectedIndices[0];
            listViewButtonMap.SelectedItems[0].SubItems[1].Text = keyCode;
            listViewButtonMap.SelectedIndices.Clear();

            if (listViewButtonMap.Items.Count > index + 1)
            {
              listViewButtonMap.SelectedIndices.Add(index + 1);
              listViewButtonMap.SelectedItems[0].Focused = true;
              listViewButtonMap.EnsureVisible(index + 1);
            }
          }
        }
      }
    }

    void CommsFailure(object obj)
    {
      Exception ex = obj as Exception;
      
      if (ex != null)
        this.Invoke(_addStatusLine, new Object[] { String.Format("Communications failure: {0}", ex.Message) });
      else
        this.Invoke(_addStatusLine, new Object[] { "Communications failure" });

      StopClient();
    }
    void Connected(object obj)
    {
      IrssLog.Info("Connected to server");

      IrssMessage message = new IrssMessage(MessageType.RegisterClient, MessageFlags.Request);
      _client.Send(message);
    }
    void Disconnected(object obj)
    {
      IrssLog.Warn("Communications with server has been lost");

      Thread.Sleep(1000);
    }

    bool StartClient(IPEndPoint endPoint)
    {
      if (_client != null)
        return false;

      ClientMessageSink sink = new ClientMessageSink(ReceivedMessage);

      _client = new Client(endPoint, sink);
      _client.CommsFailureCallback  = new WaitCallback(CommsFailure);
      _client.ConnectCallback       = new WaitCallback(Connected);
      _client.DisconnectCallback    = new WaitCallback(Disconnected);
      
      if (_client.Start())
      {
        return true;
      }
      else
      {
        _client = null;
        return false;
      }
    }
    void StopClient()
    {
      if (_client == null)
        return;

      _client.Dispose();
      _client = null;
    }

    private void buttonConnect_Click(object sender, EventArgs e)
    {
      try
      {
        AddStatusLine("Connect");
        listBoxStatus.Update();

        if (_client != null)
        {
          AddStatusLine("Already connected/connecting");
          return;
        }

        _serverHost = comboBoxComputer.Text;

        IPAddress serverIP = Client.GetIPFromName(_serverHost);
        IPEndPoint endPoint = new IPEndPoint(serverIP, IrssComms.Server.DefaultPort);

        StartClient(endPoint);
      }
      catch (Exception ex)
      {
        AddStatusLine(ex.Message);
      }
    }
    private void buttonDisconnect_Click(object sender, EventArgs e)
    {
      AddStatusLine("Disconnect");

      try
      {
        if (_client == null)
        {
          AddStatusLine(" - Not connected");
          return;
        }

        if (_registered)
        {
          IrssMessage message = new IrssMessage(MessageType.UnregisterClient, MessageFlags.Request);
          _client.Send(message);
        }

        StopClient();
      }
      catch (Exception ex)
      {
        AddStatusLine(ex.Message);
      }
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      ClearMap();
    }
    private void buttonLoad_Click(object sender, EventArgs e)
    {
      LoadMap();
    }
    private void buttonSave_Click(object sender, EventArgs e)
    {
      SaveMap();
    }


    void ClearMap()
    {
      string[] abstractButtons = Enum.GetNames(typeof(AbstractButton));
      
      listViewButtonMap.Items.Clear();
      foreach (string abstractButton in abstractButtons)
        listViewButtonMap.Items.Add(new ListViewItem(new string[] { abstractButton, String.Empty }));
    }
    void SaveMap()
    {
      if (String.IsNullOrEmpty(textBoxRemoteName.Text))
      {
        MessageBox.Show(this, "You must include a remote name before saving", "Missing remote name", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string directory = Path.Combine(AbstractRemoteMapFolder, _selectedDevice);

      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);

      string fileName = Path.ChangeExtension(textBoxRemoteName.Text, ".xml");
      string path = Path.Combine(directory, fileName);

      this.Invoke(_addStatusLine, String.Format("Writing to file \"{0}\"", path));

      DataTable table = new DataTable("RemoteTable");
      table.ReadXmlSchema(AbstractRemoteSchemaFile);

      foreach (ListViewItem item in listViewButtonMap.Items)
      {
        if (!String.IsNullOrEmpty(item.SubItems[1].Text))
          table.Rows.Add(item.SubItems[1].Text, item.SubItems[0].Text);
      }

      table.WriteXml(path);
    }
    void LoadMap()
    {
      if (String.IsNullOrEmpty(textBoxRemoteName.Text))
      {
        MessageBox.Show(this, "You must include a remote name to load", "Missing remote name", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string fileName = Path.ChangeExtension(textBoxRemoteName.Text, ".xml");
      string directory = Path.Combine(AbstractRemoteMapFolder, _selectedDevice);
      string path = Path.Combine(directory, fileName);

      if (!File.Exists(path))
      {
        MessageBox.Show(this, String.Format("Remote file not found ({0}) in device folder ({1})", fileName, _selectedDevice), "Remote file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      this.Invoke(_addStatusLine, String.Format("Reading remote from file \"{0}\" (device: {1})", fileName, _selectedDevice));

      DataTable table = new DataTable("RemoteTable");
      table.ReadXmlSchema(AbstractRemoteSchemaFile);
      table.ReadXml(path);

      string[] abstractButtons = Enum.GetNames(typeof(AbstractButton));

      listViewButtonMap.Items.Clear();
      foreach (string abstractButton in abstractButtons)
      {
        string[] subitems = new string[] { abstractButton, String.Empty };

        DataRow[] rows = table.Select(String.Format("AbstractButton = '{0}'", abstractButton));

        if (rows.Length == 1)
          subitems[1] = rows[0]["RawCode"].ToString();

        ListViewItem item = new ListViewItem(subitems);
        listViewButtonMap.Items.Add(item);
      }
    }

    private void listViewButtonMap_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
      {
        if (listViewButtonMap.SelectedItems.Count == 1)
        {
          listViewButtonMap.SelectedItems[0].SubItems[1].Text = String.Empty;
        }
      }
    }

    private void comboBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
      _selectedDevice = comboBoxDevice.Text;
    }


  }

}
