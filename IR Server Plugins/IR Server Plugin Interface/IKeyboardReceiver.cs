using System;

namespace IRServerPluginInterface
{

  #region Delegates

  /// <summary>
  /// IR Server callback for keyboard key press handling.
  /// </summary>
  /// <param name="vKey">Virtual key code.</param>
  /// <param name="keyUp">Is this key coming up.</param>
  public delegate void KeyboardHandler(int vKey, bool keyUp);

  #endregion Delegates

  /// <summary>
  /// Plugins that implement this interface can receive keyboard button presses.
  /// </summary>
  public interface IKeyboardReceiver
  {

    /// <summary>
    /// Callback for keyboard presses.
    /// </summary>
    /// <value>The keyboard callback.</value>
    KeyboardHandler KeyboardCallback { get; set; }

  }

}