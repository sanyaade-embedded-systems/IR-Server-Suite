using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace InputService.Plugin
{
  /// <summary>
  /// Driver class for the Windows Vista eHome driver.
  /// </summary>
  internal class DriverVista : Driver
  {
    #region Interop

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeviceIoControl(
      SafeFileHandle handle,
      [MarshalAs(UnmanagedType.U4)] IoCtrl ioControlCode,
      IntPtr inBuffer, int inBufferSize,
      IntPtr outBuffer, int outBufferSize,
      out int bytesReturned,
      IntPtr overlapped);

    #endregion Interop

    #region Structures

    #region Notes

    // This is really weird and I don't know why this works, but apparently on
    // 64-bit systems the following structures require 64-bit integers.
    // The easiest way to do this is to use an IntPtr because it is 32-bits
    // wide on 32-bit systems, and 64-bits wide on 64-bit systems.
    // Given that it is exactly the same data on 32-bit or 64-bit systems it
    // makes no sense (to me) why Microsoft would do it this way ...

    // I couldn't find any reference to this in the WinHEC or other
    // documentation I have seen.  When 64-bit users started reporting
    // "The data area passed to a system call is too small." errors (122) the
    // only thing I could think of was that the structures were differenly
    // sized on 64-bit systems.  And the only thing in C# that sizes
    // differently on 64-bit systems is the IntPtr.

    #endregion Notes

    #region Nested type: AvailableBlasters

    /// <summary>
    /// Available Blasters data structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct AvailableBlasters
    {
      /// <summary>
      /// Blaster bit-mask.
      /// </summary>
      public IntPtr Blasters;
    }

    #endregion

    #region Nested type: DeviceCapabilities

    /// <summary>
    /// Device Capabilities data structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct DeviceCapabilities
    {
      /// <summary>
      /// Device protocol version.
      /// </summary>
      public IntPtr ProtocolVersion;

      /// <summary>
      /// Number of transmit ports � 0-32.
      /// </summary>
      public IntPtr TransmitPorts;

      /// <summary>
      /// Number of receive ports � 0-32. For beanbag, this is two (one for learning, one for normal receiving).
      /// </summary>
      public IntPtr ReceivePorts;

      /// <summary>
      /// Bitmask identifying which receivers are learning receivers � low bit is the first receiver, second-low bit is the second receiver, etc ...
      /// </summary>
      public IntPtr LearningMask;

      /// <summary>
      /// Device flags.
      /// </summary>
      public IntPtr DetailsFlags;
    }

    #endregion

    #region Nested type: ReceiveParams

    /// <summary>
    /// Receive parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct ReceiveParams
    {
      /// <summary>
      /// Last packet in block?
      /// </summary>
      public IntPtr DataEnd;

      /// <summary>
      /// Number of bytes in block.
      /// </summary>
      public IntPtr ByteCount;

      /// <summary>
      /// Carrier frequency of IR received.
      /// </summary>
      public IntPtr CarrierFrequency;
    }

    #endregion

    #region Nested type: StartReceiveParams

    /// <summary>
    /// Parameters for StartReceive.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct StartReceiveParams
    {
      /// <summary>
      /// Index of the receiver to use.
      /// </summary>
      public IntPtr Receiver;

      /// <summary>
      /// Receive timeout, in milliseconds.
      /// </summary>
      public IntPtr Timeout;
    }

    #endregion

    #region Nested type: TransmitChunk

    /// <summary>
    /// Information for transmitting IR.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct TransmitChunk
    {
      /// <summary>
      /// Next chunk offset.
      /// </summary>
      public IntPtr OffsetToNextChunk;

      /// <summary>
      /// Repeat count.
      /// </summary>
      public IntPtr RepeatCount;

      /// <summary>
      /// Number of bytes.
      /// </summary>
      public IntPtr ByteCount;
    }

    #endregion

    #region Nested type: TransmitParams

    /// <summary>
    /// Parameters for transmitting IR.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct TransmitParams
    {
      /// <summary>
      /// Bitmask containing ports to transmit on.
      /// </summary>
      public IntPtr TransmitPortMask;

      /// <summary>
      /// Carrier period.
      /// </summary>
      public IntPtr CarrierPeriod;

      /// <summary>
      /// Transmit Flags.
      /// </summary>
      public IntPtr Flags;

      /// <summary>
      /// Pulse Size.  If Pulse Mode Flag set.
      /// </summary>
      public IntPtr PulseSize;
    }

    #endregion

    #endregion Structures

    #region Enumerations

    //#region Nested type: DeviceCapabilityFlags

    ///// <summary>
    ///// IR Device Capability Flags.
    ///// </summary>
    //[Flags]
    //private enum DeviceCapabilityFlags
    //{
    //  /// <summary>
    //  /// Hardware supports legacy key signing.
    //  /// </summary>
    //  LegacySigning = 0x0001,
    //  /// <summary>
    //  /// Hardware has unique serial number.
    //  /// </summary>
    //  SerialNumber = 0x0002,
    //  /// <summary>
    //  /// Can hardware flash LED to identify receiver? 
    //  /// </summary>
    //  FlashLed = 0x0004,
    //  /// <summary>
    //  /// Is this a legacy device?
    //  /// </summary>
    //  Legacy = 0x0008,
    //  /// <summary>
    //  /// Device can wake from S1.
    //  /// </summary>
    //  WakeS1 = 0x0010,
    //  /// <summary>
    //  /// Device can wake from S2.
    //  /// </summary>
    //  WakeS2 = 0x0020,
    //  /// <summary>
    //  /// Device can wake from S3.
    //  /// </summary>
    //  WakeS3 = 0x0040,
    //  /// <summary>
    //  /// Device can wake from S4.
    //  /// </summary>
    //  WakeS4 = 0x0080,
    //  /// <summary>
    //  /// Device can wake from S5.
    //  /// </summary>
    //  WakeS5 = 0x0100,
    //}

    //#endregion

    #region Nested type: IoCtrl

    /// <summary>
    /// Device IO Control details.
    /// </summary>
    private enum IoCtrl
    {
      /// <summary>
      /// Start receiving IR.
      /// </summary>
      StartReceive = 0x0F608028,
      /// <summary>
      /// Stop receiving IR.
      /// </summary>
      StopReceive = 0x0F60802C,
      /// <summary>
      /// Get IR device details.
      /// </summary>
      GetDetails = 0x0F604004,
      /// <summary>
      /// Get IR blasters
      /// </summary>
      GetBlasters = 0x0F604008,
      /// <summary>
      /// Receive IR.
      /// </summary>
      Receive = 0x0F604022,
      /// <summary>
      /// Transmit IR.
      /// </summary>
      Transmit = 0x0F608015,
    }

    #endregion

    #region Nested type: ReadThreadMode

    /// <summary>
    /// Read Thread Mode.
    /// </summary>
    private enum ReadThreadMode
    {
      Receiving,
      Learning,
      LearningDone,
      LearningFailed,
      Stop,
    }

    #endregion

    #region Nested type: TransmitMode

    /// <summary>
    /// Used to set the carrier mode for IR blasting.
    /// </summary>
    private enum TransmitMode
    {
      /// <summary>
      /// Carrier Mode.
      /// </summary>
      CarrierMode = 0,
      /// <summary>
      /// DC Mode.
      /// </summary>
      DCMode = 1,
    }

    #endregion

    #endregion Enumerations

    #region Constants

    private const int DeviceBufferSize = 100;
    private const int PacketTimeout = 100;
    //private const int WriteSyncTimeout = 10000;

    const int ReadThreadTimeout = 200;
    const int MaxReadThreadTries = 10;

    const int ErrorBadCommand = 22;
    const int ErrorOperationAborted = 995;

    #endregion Constants

    #region Variables

    #region Device Details

    private int _learnPort;
    private int _learnPortMask;
    private int _numTxPorts;

    private int _receivePort;
    private int _txPortMask;

    #endregion Device Details

    private bool _deviceAvailable;
    private SafeFileHandle _eHomeHandle;
    private IrCode _learningCode;
    private NotifyWindow _notifyWindow;

    private Thread _readThread;
    private ReadThreadMode _readThreadMode;
	private ReadThreadMode _readThreadModeNext;
	private bool _deviceReceiveStarted;

    #endregion Variables

    #region Constructor

    public DriverVista(Guid deviceGuid, string devicePath, RemoteCallback remoteCallback,
                       KeyboardCallback keyboardCallback, MouseCallback mouseCallback)
      : base(deviceGuid, devicePath, remoteCallback, keyboardCallback, mouseCallback)
    {
    }

    #endregion Constructor

    #region Device Control Functions

    private void StartReceive(int receivePort, int timeout)
    {
      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      StartReceiveParams structure;
      structure.Receiver = new IntPtr(receivePort);
      structure.Timeout = new IntPtr(timeout);

      IntPtr structPtr = IntPtr.Zero;

      try
      {
        structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(structure));

        Marshal.StructureToPtr(structure, structPtr, false);

        int bytesReturned;
        IoControl(IoCtrl.StartReceive, structPtr, Marshal.SizeOf(structure), IntPtr.Zero, 0, out bytesReturned);
      }
      finally
      {
        if (structPtr != IntPtr.Zero)
          Marshal.FreeHGlobal(structPtr);
      }
    }

    private void StopReceive()
    {
      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      int bytesReturned;
      IoControl(IoCtrl.StopReceive, IntPtr.Zero, 0, IntPtr.Zero, 0, out bytesReturned);
    }

    private void GetDeviceCapabilities()
    {
      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      DeviceCapabilities structure = new DeviceCapabilities();

      IntPtr structPtr = IntPtr.Zero;

      try
      {
        structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(structure));

        Marshal.StructureToPtr(structure, structPtr, false);

        int bytesReturned;
        IoControl(IoCtrl.GetDetails, IntPtr.Zero, 0, structPtr, Marshal.SizeOf(structure), out bytesReturned);

        structure = (DeviceCapabilities) Marshal.PtrToStructure(structPtr, typeof (DeviceCapabilities));
      }
      finally
      {
        if (structPtr != IntPtr.Zero)
          Marshal.FreeHGlobal(structPtr);
      }

      _numTxPorts = structure.TransmitPorts.ToInt32();
      //_numRxPorts = structure.ReceivePorts.ToInt32();
      _learnPortMask = structure.LearningMask.ToInt32();

      int receivePort = FirstLowBit(_learnPortMask);
      if (receivePort != -1)
        _receivePort = receivePort;

      int learnPort = FirstHighBit(_learnPortMask);
      if (learnPort != -1)
        _learnPort = learnPort;
      else
        _learnPort = _receivePort;

      //DeviceCapabilityFlags flags = (DeviceCapabilityFlags)structure.DetailsFlags.ToInt32();
      //_legacyDevice = (int)(flags & DeviceCapabilityFlags.Legacy) != 0;
      //_canFlashLed = (int)(flags & DeviceCapabilityFlags.FlashLed) != 0;

#if DEBUG
      DebugWriteLine("Device Capabilities:");
      DebugWriteLine("NumTxPorts:     {0}", _numTxPorts);
      DebugWriteLine("NumRxPorts:     {0}", structure.ReceivePorts.ToInt32());
      DebugWriteLine("LearnPortMask:  {0}", _learnPortMask);
      DebugWriteLine("ReceivePort:    {0}", _receivePort);
      DebugWriteLine("LearnPort:      {0}", _learnPort);
      DebugWriteLine("DetailsFlags:   {0}", structure.DetailsFlags.ToInt32());
#endif
    }

    private void GetBlasters()
    {
      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      if (_numTxPorts <= 0)
        return;

      AvailableBlasters structure = new AvailableBlasters();

      IntPtr structPtr = IntPtr.Zero;

      try
      {
        structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(structure));

        Marshal.StructureToPtr(structure, structPtr, false);

        int bytesReturned;
        IoControl(IoCtrl.GetBlasters, IntPtr.Zero, 0, structPtr, Marshal.SizeOf(structure), out bytesReturned);

        structure = (AvailableBlasters) Marshal.PtrToStructure(structPtr, typeof (AvailableBlasters));
      }
      finally
      {
        if (structPtr != IntPtr.Zero)
          Marshal.FreeHGlobal(structPtr);
      }

      _txPortMask = structure.Blasters.ToInt32();

#if DEBUG
      DebugWriteLine("TxPortMask:     {0}", _txPortMask);
#endif
    }

    private void TransmitIR(byte[] irData, int carrier, int transmitPortMask)
    {
#if DEBUG
      DebugWriteLine("TransmitIR({0} bytes, carrier: {1}, port: {2})", irData.Length, carrier, transmitPortMask);
#endif

      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      TransmitParams transmitParams = new TransmitParams();
      transmitParams.TransmitPortMask = new IntPtr(transmitPortMask);

      if (carrier == IrCode.CarrierFrequencyUnknown)
        carrier = IrCode.CarrierFrequencyDefault;

      TransmitMode mode = GetTransmitMode(carrier);
      if (mode == TransmitMode.CarrierMode)
        transmitParams.CarrierPeriod = new IntPtr(GetCarrierPeriod(carrier));
      else
        transmitParams.PulseSize = new IntPtr(carrier);

      transmitParams.Flags = new IntPtr((int) mode);

      TransmitChunk transmitChunk = new TransmitChunk();
      transmitChunk.OffsetToNextChunk = new IntPtr(0);
      transmitChunk.RepeatCount = new IntPtr(1);
      transmitChunk.ByteCount = new IntPtr(irData.Length);

      int bufferSize = irData.Length + Marshal.SizeOf(typeof (TransmitChunk)) + 8;
      byte[] buffer = new byte[bufferSize];

      byte[] rawTransmitChunk = RawSerialize(transmitChunk);
      Array.Copy(rawTransmitChunk, buffer, rawTransmitChunk.Length);

      Array.Copy(irData, 0, buffer, rawTransmitChunk.Length, irData.Length);

      IntPtr structurePtr = IntPtr.Zero;
      IntPtr bufferPtr = IntPtr.Zero;

      try
      {
        structurePtr = Marshal.AllocHGlobal(Marshal.SizeOf(transmitParams));
        bufferPtr = Marshal.AllocHGlobal(buffer.Length);

        Marshal.StructureToPtr(transmitParams, structurePtr, true);

        Marshal.Copy(buffer, 0, bufferPtr, buffer.Length);

        int bytesReturned;
        IoControl(IoCtrl.Transmit, structurePtr, Marshal.SizeOf(typeof (TransmitParams)), bufferPtr, bufferSize,
                  out bytesReturned);
      }
      finally
      {
        if (structurePtr != IntPtr.Zero)
          Marshal.FreeHGlobal(structurePtr);

        if (bufferPtr != IntPtr.Zero)
          Marshal.FreeHGlobal(bufferPtr);
      }

      // Force a delay between blasts (hopefully solves back-to-back blast errors) ...
      Thread.Sleep(PacketTimeout);
    }

    private void IoControl(IoCtrl ioControlCode, IntPtr inBuffer, int inBufferSize, IntPtr outBuffer, int outBufferSize,
                           out int bytesReturned)
    {
      if (!_deviceAvailable)
        throw new InvalidOperationException("Device not available");

      using (WaitHandle waitHandle = new ManualResetEvent(false))
      {
        SafeHandle safeWaitHandle = waitHandle.SafeWaitHandle;

        bool success = false;
        safeWaitHandle.DangerousAddRef(ref success);
        if (!success)
          throw new InvalidOperationException("Failed to initialize safe wait handle");

        try
        {
          int lastError;

          IntPtr dangerousWaitHandle = safeWaitHandle.DangerousGetHandle();

          DeviceIoOverlapped overlapped = new DeviceIoOverlapped();
          overlapped.ClearAndSetEvent(dangerousWaitHandle);

          bool deviceIoControl = DeviceIoControl(_eHomeHandle, ioControlCode, inBuffer, inBufferSize, outBuffer,
                                                 outBufferSize, out bytesReturned, overlapped.Overlapped);
          lastError = Marshal.GetLastWin32Error();

          if (!deviceIoControl)
          {
            // Now also handles Operation Aborted and Bad Command errors.
            switch (lastError)
            {
              case ErrorIoPending:
            waitHandle.WaitOne();

            bool getOverlapped = GetOverlappedResult(_eHomeHandle, overlapped.Overlapped, out bytesReturned, false);
            lastError = Marshal.GetLastWin32Error();

            if (!getOverlapped)
                {
                  if (lastError == ErrorBadCommand)
                    goto case ErrorBadCommand;
                  if (lastError == ErrorOperationAborted)
                    goto case ErrorOperationAborted;
                  throw new Win32Exception(lastError);
                }
                break;

              case ErrorBadCommand:
                if (Thread.CurrentThread == _readThread)
                  //Cause receive restart
                  _deviceReceiveStarted = false;
                break;

              case ErrorOperationAborted:
                if (Thread.CurrentThread != _readThread)
                  throw new Win32Exception(lastError);

                //Cause receive restart
                _deviceReceiveStarted = false;
                break;

              default:
              throw new Win32Exception(lastError);
          }
        }
        }
        catch
        {
          if (_eHomeHandle != null)
            CancelIo(_eHomeHandle);

          throw;
        }
        finally
        {
          safeWaitHandle.DangerousRelease();
        }
      }
    }

    #endregion Device Control Functions

    #region Driver overrides

    /// <summary>
    /// Start using the device.
    /// </summary>
    public override void Start()
    {
      try
      {
#if DEBUG
        DebugOpen("MicrosoftMceTransceiver_DriverVista.log");
        DebugWriteLine("Start()");
        DebugWriteLine("Device Guid: {0}", _deviceGuid);
        DebugWriteLine("Device Path: {0}", _devicePath);
#endif

        _notifyWindow = new NotifyWindow();
        _notifyWindow.Create();
        _notifyWindow.Class = _deviceGuid;
        //_notifyWindow.RegisterDeviceArrival();

        OpenDevice();
        InitializeDevice();

        StartReadThread(ReadThreadMode.Receiving);

        _notifyWindow.DeviceArrival += OnDeviceArrival;
        _notifyWindow.DeviceRemoval += OnDeviceRemoval;
      }
      catch
      {
#if DEBUG
        DebugClose();
#endif
        throw;
      }
    }

    /// <summary>
    /// Stop access to the device.
    /// </summary>
    public override void Stop()
    {
#if DEBUG
      DebugWriteLine("Stop()");
#endif

      try
      {
        _notifyWindow.DeviceArrival -= OnDeviceArrival;
        _notifyWindow.DeviceRemoval -= OnDeviceRemoval;

        StopReadThread();
        CloseDevice();
      }
#if DEBUG
      catch (Exception ex)
      {
        DebugWriteLine(ex.ToString());
        throw;
      }
#else
      catch
      {
        throw;
      }
#endif
      finally
      {
        _notifyWindow.UnregisterDeviceArrival();
        _notifyWindow.Dispose();
        _notifyWindow = null;

#if DEBUG
        DebugClose();
#endif
      }
    }

    /// <summary>
    /// Computer is entering standby, suspend device.
    /// </summary>
    public override void Suspend()
    {
#if DEBUG
      DebugWriteLine("Suspend()");
#endif
    }

    /// <summary>
    /// Computer is returning from standby, resume device.
    /// </summary>
    public override void Resume()
    {
#if DEBUG
      DebugWriteLine("Resume()");
#endif

      try
      {
        if (String.IsNullOrEmpty(Find(_deviceGuid)))
        {
#if DEBUG
          DebugWriteLine("Device not found");
#endif
          return;
        }
      }
#if DEBUG
      catch (Exception ex)
      {
        DebugWriteLine(ex.ToString());
        throw;
      }
#else
      catch
      {
        throw;
      }
#endif
    }

    /// <summary>
    /// Learn an IR Command.
    /// </summary>
    /// <param name="learnTimeout">How long to wait before aborting learn.</param>
    /// <param name="learned">Newly learned IR Command.</param>
    /// <returns>Learn status.</returns>
    public override LearnStatus Learn(int learnTimeout, out IrCode learned)
    {
#if DEBUG
      DebugWriteLine("Learn()");
#endif

      RestartReadThread(ReadThreadMode.Learning);

      learned = null;
      _learningCode = new IrCode();

      int learnStartTick = Environment.TickCount;

      // Wait for the learning to finish ...
      while (_readThreadMode == ReadThreadMode.Learning && Environment.TickCount < learnStartTick + learnTimeout)
        Thread.Sleep(PacketTimeout);

#if DEBUG
      DebugWriteLine("End Learn");
#endif

      ReadThreadMode modeWas = _readThreadMode;

      RestartReadThread(ReadThreadMode.Receiving);

      LearnStatus status = LearnStatus.Failure;

      switch (modeWas)
      {
        case ReadThreadMode.Learning:
          status = LearnStatus.Timeout;
          break;

        case ReadThreadMode.LearningFailed:
          status = LearnStatus.Failure;
          break;

        case ReadThreadMode.LearningDone:
#if DEBUG
          DebugDump(_learningCode.TimingData);
#endif
          if (_learningCode.FinalizeData())
          {
            learned = _learningCode;
            status = LearnStatus.Success;
          }
          break;
      }

      _learningCode = null;
      return status;
    }

    /// <summary>
    /// Send an IR Command.
    /// </summary>
    /// <param name="code">IR Command data to send.</param>
    /// <param name="port">IR port to send to.</param>
    public override void Send(IrCode code, int port)
    {
#if DEBUG
      DebugWrite("Send(): ");
      DebugDump(code.TimingData);
#endif

      byte[] data = DataPacket(code);

      int portMask = 0;
      // Hardware ports map to bits in mask with Port 1 at left, ascending to right
      switch ((BlasterPort) port)
      {
        case BlasterPort.Both:
          portMask = _txPortMask;
          break;
        case BlasterPort.Port_1:
          portMask = GetHighBit(_txPortMask, _numTxPorts);
          break;
        case BlasterPort.Port_2:
          portMask = GetHighBit(_txPortMask, _numTxPorts - 1);
          break;
      }

      TransmitIR(data, code.Carrier, portMask);
    }

    #endregion Driver overrides

    #region Implementation

    /// <summary>
    /// Initializes the device.
    /// </summary>
    private void InitializeDevice()
    {
#if DEBUG
      DebugWriteLine("InitializeDevice()");
#endif

      GetDeviceCapabilities();
      GetBlasters();
    }

    /// <summary>
    /// Converts an IrCode into raw data for the device.
    /// </summary>
    /// <param name="code">IrCode to convert.</param>
    /// <returns>Raw device data.</returns>
    private static byte[] DataPacket(IrCode code)
    {
#if DEBUG
      DebugWriteLine("DataPacket()");
#endif

      if (code.TimingData.Length == 0)
        return null;

      byte[] data = new byte[code.TimingData.Length*4];

      int dataIndex = 0;
      for (int timeIndex = 0; timeIndex < code.TimingData.Length; timeIndex++)
      {
        uint time = (uint) (50*(int) Math.Round((double) code.TimingData[timeIndex]/50));

        for (int timeShift = 0; timeShift < 4; timeShift++)
        {
          data[dataIndex++] = (byte) (time & 0xFF);
          time >>= 8;
        }
      }

      return data;
    }

    /// <summary>
    /// Start the device read thread.
    /// </summary>
    private void StartReadThread(ReadThreadMode mode)
    {
#if DEBUG
      DebugWriteLine("StartReadThread({0})", Enum.GetName(typeof (ReadThreadMode), mode));
#endif

      if (_readThread != null)
      {
#if DEBUG
        DebugWriteLine("Read thread already started");
#endif
        return;
      }

      _deviceReceiveStarted = false;
      _readThreadModeNext = mode;

      _readThread = new Thread(ReadThread);
      _readThread.Name = "MicrosoftMceTransceiver.DriverVista.ReadThread";
      _readThread.IsBackground = true;
      _readThread.Start();
    }

    /// <summary>
    /// Restart the device read thread.
    /// </summary>
    void RestartReadThread(ReadThreadMode mode)
    {
      // Alternative to StopReadThread() ... StartReadThread(). Avoids Thread.Abort.
      int numTriesLeft;

      _readThreadModeNext = mode;
      numTriesLeft = MaxReadThreadTries;

      // Simple, optimistic wait for read thread to respond. Has room for improvement, but tends to work first time in practice.
      while (_readThreadMode != _readThreadModeNext && numTriesLeft-- != 0)
      {
        // Unblocks read thread, typically with Operation Aborted error. May cause Bad Command error in either thread.
        StopReceive();
        Thread.Sleep(ReadThreadTimeout);
      }

      if (numTriesLeft == 0)
        throw new InvalidOperationException("Failed to cycle read thread");
    }

    /// <summary>
    /// Stop the device read thread.
    /// </summary>
    private void StopReadThread()
    {
#if DEBUG
      DebugWriteLine("StopReadThread()");
#endif

      if (_readThread == null)
      {
#if DEBUG
        DebugWriteLine("Read thread already stopped");
#endif
        return;
      }

      //if (_eHomeHandle != null)
      //  CancelIo(_eHomeHandle);

      if (_readThread.IsAlive)
      {
        _readThread.Abort();

        if (Thread.CurrentThread != _readThread)
          _readThread.Join();
      }

      _readThreadMode = ReadThreadMode.Stop;

      _readThread = null;
    }

    /// <summary>
    /// Opens the device handles and registers for device removal notification.
    /// </summary>
    private void OpenDevice()
    {
#if DEBUG
      DebugWriteLine("OpenDevice()");
#endif

      if (_eHomeHandle != null)
      {
#if DEBUG
        DebugWriteLine("Device already open");
#endif
        return;
      }

      int lastError;

      _eHomeHandle = CreateFile(_devicePath, CreateFileAccessTypes.GenericRead | CreateFileAccessTypes.GenericWrite,
                                CreateFileShares.None, IntPtr.Zero, CreateFileDisposition.OpenExisting,
                                CreateFileAttributes.Overlapped, IntPtr.Zero);
      lastError = Marshal.GetLastWin32Error();
      if (_eHomeHandle.IsInvalid)
      {
        _eHomeHandle = null;
        throw new Win32Exception(lastError);
      }

      bool success = false;
      _eHomeHandle.DangerousAddRef(ref success);
      if (success)
      {
        //_notifyWindow.UnregisterDeviceArrival();  // If the device is present then we don't want to monitor arrival.
        _notifyWindow.RegisterDeviceRemoval(_eHomeHandle.DangerousGetHandle());
      }
#if DEBUG
      else
      {
        DebugWriteLine("Warning: Failed to initialize device removal notification");
      }
#endif

      Thread.Sleep(PacketTimeout);
      // Hopefully improves compatibility with Zalman remote which times out retrieving device capabilities. (2008-01-01)

      _deviceAvailable = true;
    }

    /// <summary>
    /// Close all handles to the device and unregisters device removal notification.
    /// </summary>
    private void CloseDevice()
    {
#if DEBUG
      DebugWriteLine("CloseDevice()");
#endif

      _deviceAvailable = false;

      if (_eHomeHandle == null)
      {
#if DEBUG
        DebugWriteLine("Device already closed");
#endif
        return;
      }

      _notifyWindow.UnregisterDeviceRemoval();

      _eHomeHandle.DangerousRelease();

      _eHomeHandle.Dispose();
      _eHomeHandle = null;
    }

    /// <summary>
    /// Called when device arrival is notified.
    /// </summary>
    private void OnDeviceArrival()
    {
#if DEBUG
      DebugWriteLine("OnDeviceArrival()");
#endif

      OpenDevice();
      InitializeDevice();

      StartReadThread(ReadThreadMode.Receiving);
    }

    /// <summary>
    /// Called when device removal is notified.
    /// </summary>
    private void OnDeviceRemoval()
    {
#if DEBUG
      DebugWriteLine("OnDeviceRemoval()");
#endif

      StopReadThread();
      CloseDevice();
    }

    /// <summary>
    /// Device read thread method.
    /// </summary>
    private void ReadThread()
    {
      IntPtr receiveParamsPtr = IntPtr.Zero;

      try
      {
        int receiveParamsSize = Marshal.SizeOf(typeof (ReceiveParams)) + DeviceBufferSize + 8;
        receiveParamsPtr = Marshal.AllocHGlobal(receiveParamsSize);

        ReceiveParams receiveParams = new ReceiveParams();
        receiveParams.ByteCount = new IntPtr(DeviceBufferSize);
        Marshal.StructureToPtr(receiveParams, receiveParamsPtr, false);

        while (_readThreadMode != ReadThreadMode.Stop)
        {
          // Cycle thread if device stopped reading.
          if (!_deviceReceiveStarted)
          {
            if (_readThreadModeNext == ReadThreadMode.Receiving)
              StartReceive(_receivePort, PacketTimeout);
            else
            {
              StartReceive(_learnPort, PacketTimeout);
            }
            _readThreadMode = _readThreadModeNext;
            _deviceReceiveStarted = true;
          }

          int bytesRead;
          IoControl(IoCtrl.Receive, IntPtr.Zero, 0, receiveParamsPtr, receiveParamsSize, out bytesRead);

          if (bytesRead > Marshal.SizeOf(receiveParams))
          {
            int dataSize = bytesRead;

            bytesRead -= Marshal.SizeOf(receiveParams);

            byte[] packetBytes = new byte[bytesRead];
            byte[] dataBytes = new byte[dataSize];

            Marshal.Copy(receiveParamsPtr, dataBytes, 0, dataSize);
            Array.Copy(dataBytes, dataSize - bytesRead, packetBytes, 0, bytesRead);

            int[] timingData = GetTimingDataFromPacket(packetBytes);

#if DEBUG
            DebugWrite("Received timing:    ");
            DebugDump(timingData);
#endif

            if (_readThreadMode == ReadThreadMode.Learning)
              _learningCode.AddTimingData(timingData);
            else
              IrDecoder.DecodeIR(timingData, _remoteCallback, _keyboardCallback, _mouseCallback);
          }

          // Determine carrier frequency when learning ...
          if (_readThreadMode == ReadThreadMode.Learning && bytesRead >= Marshal.SizeOf(receiveParams))
          {
            ReceiveParams receiveParams2 =
              (ReceiveParams) Marshal.PtrToStructure(receiveParamsPtr, typeof (ReceiveParams));

            if (receiveParams2.DataEnd.ToInt32() != 0)
            {
              _learningCode.Carrier = receiveParams2.CarrierFrequency.ToInt32();
              _readThreadMode = ReadThreadMode.LearningDone;
            }
          }
        }
      }
#if DEBUG
      catch (Exception ex)
      {
        DebugWriteLine(ex.ToString());
#else
      catch (Exception)
      {
#endif

        if (_eHomeHandle != null)
          CancelIo(_eHomeHandle);
      }
      finally
      {
        if (receiveParamsPtr != IntPtr.Zero)
          Marshal.FreeHGlobal(receiveParamsPtr);

        try
        {
          if (_eHomeHandle != null)
            StopReceive();
        }
#if DEBUG
        catch (Exception ex)
        {
          DebugWriteLine(ex.ToString());
        }
#else
        catch
        {
          // Ignore this exception, we're closing it down anyway.
        }
#endif
      }

#if DEBUG
      DebugWriteLine("Read Thread Ended");
#endif
    }

    #endregion Implementation

    #region Misc Methods

    private static byte[] RawSerialize(object anything)
    {
      int rawSize = Marshal.SizeOf(anything);
      byte[] rawData = new byte[rawSize];

      GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

      try
      {
        IntPtr buffer = handle.AddrOfPinnedObject();

        Marshal.StructureToPtr(anything, buffer, false);
      }
      finally
      {
        handle.Free();
      }

      return rawData;
    }

    private static int GetHighBit(int mask, int bitCount)
    {
      int count = 0;
      for (int i = 0; i < 32; i++)
      {
        int bitMask = 1 << i;

        if ((mask & bitMask) != 0)
          if (++count == bitCount)
            return bitMask;
      }

      return 0;
    }

    private static int FirstHighBit(int mask)
    {
      for (int i = 0; i < 32; i++)
        if ((mask & (1 << i)) != 0)
          return i;

      return -1;
    }

    private static int FirstLowBit(int mask)
    {
      for (int i = 0; i < 32; i++)
        if ((mask & (1 << i)) == 0)
          return i;

      return -1;
    }

    private static int GetCarrierPeriod(int carrier)
    {
      return (int) Math.Round(1000000.0/carrier);
    }

    private static TransmitMode GetTransmitMode(int carrier)
    {
      if (carrier > 100)
        return TransmitMode.CarrierMode;

      return TransmitMode.DCMode;
    }

    private static int[] GetTimingDataFromPacket(byte[] packetBytes)
    {
      int[] timingData = new int[packetBytes.Length/4];

      int timingDataIndex = 0;

      for (int index = 0; index < packetBytes.Length; index += 4)
        timingData[timingDataIndex++] =
          (packetBytes[index] +
           (packetBytes[index + 1] << 8) +
           (packetBytes[index + 2] << 16) +
           (packetBytes[index + 3] << 24));

      return timingData;
    }

    #endregion Misc Methods
  }
}