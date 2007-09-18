using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using IrssComms;
using IrssUtils;

namespace TrayLauncher
{

  public class Tray
  {

    #region Constants

    const string DefaultKeyCode = "31730";

    static readonly string ConfigurationFile = Common.FolderAppData + "Tray Launcher\\Tray Launcher.xml";

    #endregion Constants

    #region Variables

    static ClientMessageSink _handleMessage = null;

    Client _client;

    static bool _registered = false;
    int _echoID = -1;

    string _serverHost;
    string _programFile;
    bool _autoRun;
    bool _launchOnLoad;
    string _launchKeyCode;

    NotifyIcon _notifyIcon;

    #endregion Variables

    #region Properties

    internal static ClientMessageSink HandleMessage
    {
      get { return _handleMessage; }
      set { _handleMessage = value; }
    }

    internal static bool Registered
    {
      get { return _registered; }
    }

    #endregion Properties
    
    #region Constructor

    public Tray()
    {
      ContextMenuStrip contextMenu = new ContextMenuStrip();
      contextMenu.Items.Add(new ToolStripMenuItem("&Launch", null, new EventHandler(ClickLaunch)));
      contextMenu.Items.Add(new ToolStripMenuItem("&Setup", null, new EventHandler(ClickSetup)));
      contextMenu.Items.Add(new ToolStripMenuItem("&Quit", null, new EventHandler(ClickQuit)));

      _notifyIcon = new NotifyIcon();
      _notifyIcon.ContextMenuStrip = contextMenu;
      _notifyIcon.Icon = Properties.Resources.Icon16Connecting;
      _notifyIcon.Text = "Tray Launcher - Connecting ...";
      _notifyIcon.DoubleClick += new EventHandler(ClickSetup);
    }

    #endregion Constructor

    #region Implementation

    internal bool Start()
    {
      try
      {
        LoadSettings();

        bool didSetup = false;
        if (String.IsNullOrEmpty(_programFile) || String.IsNullOrEmpty(_serverHost))
        {
          if (!Configure())
            return false;

          didSetup = true;
        }

        if (StartClient())
        {
          _notifyIcon.Visible = true;

          if (!didSetup && _launchOnLoad)
            ClickLaunch(null, null);

          return true;
        }
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.ToString());
      }

      return false;
    }

    void Stop()
    {
      _notifyIcon.Visible = false;

      try
      {
        if (_registered)
        {
          _registered = false;

          IrssMessage message = new IrssMessage(MessageType.UnregisterClient, MessageFlags.Request);
          _client.Send(message);
        }
      }
      catch { }
      
      StopClient();
    }

    void LoadSettings()
    {
      try
      {
        _autoRun = SystemRegistry.GetAutoRun("Tray Launcher");
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.ToString());

        _autoRun = false;
      }

      try
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(ConfigurationFile);

        _serverHost     = doc.DocumentElement.Attributes["ServerHost"].Value;
        _programFile    = doc.DocumentElement.Attributes["ProgramFile"].Value;
        _launchOnLoad   = bool.Parse(doc.DocumentElement.Attributes["LaunchOnLoad"].Value);
        _launchKeyCode  = doc.DocumentElement.Attributes["LaunchKeyCode"].Value;
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.ToString());

        _serverHost     = String.Empty;
        _programFile    = String.Empty;
        _launchOnLoad   = false;
        _launchKeyCode  = DefaultKeyCode;
      }
    }
    void SaveSettings()
    {
      try
      {
        if (_autoRun)
          SystemRegistry.SetAutoRun("Tray Launcher", Application.ExecutablePath);
        else
          SystemRegistry.RemoveAutoRun("Tray Launcher");
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.ToString());
      }

      try
      {
        XmlTextWriter writer = new XmlTextWriter(ConfigurationFile, System.Text.Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 1;
        writer.IndentChar = (char)9;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("settings"); // <settings>

        writer.WriteAttributeString("ServerHost", _serverHost);
        writer.WriteAttributeString("ProgramFile", _programFile);
        writer.WriteAttributeString("LaunchOnLoad", _launchOnLoad.ToString());
        writer.WriteAttributeString("LaunchKeyCode", _launchKeyCode);

        writer.WriteEndElement(); // </settings>
        writer.WriteEndDocument();
        writer.Close();
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.ToString());
      }
    }

    void CommsFailure(object obj)
    {
      Exception ex = obj as Exception;

      if (ex != null)
        IrssLog.Error("Communications failure: {0}", ex.Message);
      else
        IrssLog.Error("Communications failure");

      StopClient();

      MessageBox.Show("Please report this error.", "Tray Launcher - Communications failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    bool StartClient()
    {
      if (_client != null)
        return false;

      ClientMessageSink sink = new ClientMessageSink(ReceivedMessage);

      IPAddress serverAddress = Client.GetIPFromName(_serverHost);

      _client = new Client(serverAddress, 24000, sink);
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

      _client.Stop();
      _client = null;
    }

    void ReceivedMessage(IrssMessage received)
    {
      IrssLog.Debug("Received Message \"{0}\"", received.Type);

      try
      {
        switch (received.Type)
        {
          case MessageType.RegisterClient:
            if ((received.Flags & MessageFlags.Success) == MessageFlags.Success)
            {
              //_irServerInfo = IRServerInfo.FromBytes(received.DataAsBytes);
              _registered = true;

              IrssLog.Info("Registered to IR Server");
            }
            else if ((received.Flags & MessageFlags.Failure) == MessageFlags.Failure)
            {
              _registered = false;
              IrssLog.Warn("IR Server refused to register");
            }
            break;

          case MessageType.RemoteEvent:
            RemoteHandlerCallback(received.DataAsString);
            break;

          case MessageType.ServerShutdown:
            IrssLog.Warn("IR Server Shutdown - Tray Launcher disabled until IR Server returns");
            _registered = false;
            break;

          case MessageType.Echo:
            _echoID = BitConverter.ToInt32(received.DataAsBytes, 0);
            break;

          case MessageType.Error:
            IrssLog.Error("Received error: {0}", received.DataAsString);
            break;
        }

        // If another module of the program has registered to receive messages too ...
        if (_handleMessage != null)
          _handleMessage(received);
      }
      catch (Exception ex)
      {
        IrssLog.Error("ReceivedMessage - {0}", ex.Message);
      }
    }

    void RemoteHandlerCallback(string keyCode)
    {
      IrssLog.Info("Remote Event: {0}", keyCode);

      if (keyCode == _launchKeyCode)
        ClickLaunch(null, null);
    }

    bool Configure()
    {
      Setup setup = new Setup();

      setup.AutoRun       = _autoRun;
      setup.ServerHost    = _serverHost;
      setup.ProgramFile   = _programFile;
      setup.LaunchOnLoad  = _launchOnLoad;
      setup.LaunchKeyCode = _launchKeyCode;

      if (setup.ShowDialog() == DialogResult.OK)
      {
        _autoRun        = setup.AutoRun;
        _serverHost     = setup.ServerHost;
        _programFile    = setup.ProgramFile;
        _launchOnLoad   = setup.LaunchOnLoad;
        _launchKeyCode  = setup.LaunchKeyCode;

        SaveSettings();
        
        return true;
      }

      return false;
    }

    void ClickSetup(object sender, EventArgs e)
    {
      IrssLog.Info("Setup");

      if (Configure())
      {
        Stop();
        Thread.Sleep(500);
        Start();
      }
    }
    void ClickLaunch(object sender, EventArgs e)
    {
      IrssLog.Info("Launch");

      try
      {
        // Check for multiple instances
        foreach (Process process in Process.GetProcesses())
        {
          try
          {
            if (Path.GetFileName(process.MainModule.ModuleName).Equals(Path.GetFileName(_programFile), StringComparison.InvariantCultureIgnoreCase))
            {
              IrssLog.Info("Program already running, attempting to give focus.");

              Win32.SetForegroundWindow(process.MainWindowHandle, true);

              return;
            }
          }
          catch { }
        }

        // Launch program
        Process launch = new Process();
        launch.StartInfo.FileName = _programFile;
        launch.StartInfo.UseShellExecute = false;
        launch.Start();
      }
      catch (Exception ex)
      {
        IrssLog.Error(ex.Message);
        MessageBox.Show(ex.Message, "Tray Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    void ClickQuit(object sender, EventArgs e)
    {
      IrssLog.Info("Quit");

      Stop();

      Application.Exit();
    }

    #endregion Implementation

  }

}
