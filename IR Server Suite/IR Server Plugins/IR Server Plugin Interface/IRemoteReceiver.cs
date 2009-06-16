namespace InputService.Plugin
{

  #region Delegates

  /// <summary>
  /// IR Server callback for remote button press handling.
  /// </summary>
  /// <param name="deviceName">The device that detected the remote button.</param>
  /// <param name="keyCode">Remote button code.</param>
  public delegate void RemoteHandler(string deviceName, string keyCode);

  #endregion Delegates

  /// <summary>
  /// Plugins that implement this interface can receive remote control button presses.
  /// </summary>
  public interface IRemoteReceiver
  {
    /// <summary>
    /// Callback for remote button presses.
    /// </summary>
    /// <value>The remote callback.</value>
    RemoteHandler RemoteCallback { get; set; }
  }
}