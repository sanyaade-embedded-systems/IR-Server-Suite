namespace InputService.Plugin
{
  partial class Configure
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
      this.components = new System.ComponentModel.Container();
      this.labelButtonRepeatDelay = new System.Windows.Forms.Label();
      this.labelButtonHeldDelay = new System.Windows.Forms.Label();
      this.numericUpDownButtonRepeatDelay = new System.Windows.Forms.NumericUpDown();
      this.numericUpDownButtonHeldDelay = new System.Windows.Forms.NumericUpDown();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.groupBoxRemoteTiming = new System.Windows.Forms.GroupBox();
      this.toolTips = new System.Windows.Forms.ToolTip(this.components);
      this.numericUpDownLearnTimeout = new System.Windows.Forms.NumericUpDown();
      this.numericUpDownKeyHeldDelay = new System.Windows.Forms.NumericUpDown();
      this.numericUpDownKeyRepeatDelay = new System.Windows.Forms.NumericUpDown();
      this.checkBoxHandleKeyboardLocal = new System.Windows.Forms.CheckBox();
      this.checkBoxHandleMouseLocal = new System.Windows.Forms.CheckBox();
      this.numericUpDownMouseSensitivity = new System.Windows.Forms.NumericUpDown();
      this.checkBoxDisableMCEServices = new System.Windows.Forms.CheckBox();
      this.checkBoxEnableRemote = new System.Windows.Forms.CheckBox();
      this.checkBoxEnableKeyboard = new System.Windows.Forms.CheckBox();
      this.checkBoxEnableMouse = new System.Windows.Forms.CheckBox();
      this.checkBoxUseSystemRatesRemote = new System.Windows.Forms.CheckBox();
      this.checkBoxUseSystemRatesKeyboard = new System.Windows.Forms.CheckBox();
      this.checkBoxDisableAutomaticButtons = new System.Windows.Forms.CheckBox();
      this.labelLearnIRTimeout = new System.Windows.Forms.Label();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.tabPageBasic = new System.Windows.Forms.TabPage();
      this.tabPageRemote = new System.Windows.Forms.TabPage();
      this.tabPageKeyboard = new System.Windows.Forms.TabPage();
      this.groupBoxKeypressTiming = new System.Windows.Forms.GroupBox();
      this.labelKeyRepeatDelay = new System.Windows.Forms.Label();
      this.labelKeyHeldDelay = new System.Windows.Forms.Label();
      this.tabPageMouse = new System.Windows.Forms.TabPage();
      this.labelMouseSensitivity = new System.Windows.Forms.Label();
      this.radioButtonStopAtStartup = new System.Windows.Forms.RadioButton();
      this.groupBoxMceServices = new System.Windows.Forms.GroupBox();
      this.radioButtonDoNothing = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.checkBoxKeyboardQwertz = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButtonRepeatDelay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButtonHeldDelay)).BeginInit();
      this.groupBoxRemoteTiming.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearnTimeout)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyHeldDelay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyRepeatDelay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMouseSensitivity)).BeginInit();
      this.tabControl.SuspendLayout();
      this.tabPageBasic.SuspendLayout();
      this.tabPageRemote.SuspendLayout();
      this.tabPageKeyboard.SuspendLayout();
      this.groupBoxKeypressTiming.SuspendLayout();
      this.tabPageMouse.SuspendLayout();
      this.groupBoxMceServices.SuspendLayout();
      this.SuspendLayout();
      // 
      // labelButtonRepeatDelay
      // 
      this.labelButtonRepeatDelay.Location = new System.Drawing.Point(8, 24);
      this.labelButtonRepeatDelay.Name = "labelButtonRepeatDelay";
      this.labelButtonRepeatDelay.Size = new System.Drawing.Size(128, 20);
      this.labelButtonRepeatDelay.TabIndex = 0;
      this.labelButtonRepeatDelay.Text = "Button repeat delay:";
      this.labelButtonRepeatDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelButtonHeldDelay
      // 
      this.labelButtonHeldDelay.Location = new System.Drawing.Point(8, 56);
      this.labelButtonHeldDelay.Name = "labelButtonHeldDelay";
      this.labelButtonHeldDelay.Size = new System.Drawing.Size(128, 20);
      this.labelButtonHeldDelay.TabIndex = 2;
      this.labelButtonHeldDelay.Text = "Button held delay:";
      this.labelButtonHeldDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // numericUpDownButtonRepeatDelay
      // 
      this.numericUpDownButtonRepeatDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericUpDownButtonRepeatDelay.Location = new System.Drawing.Point(144, 24);
      this.numericUpDownButtonRepeatDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.numericUpDownButtonRepeatDelay.Name = "numericUpDownButtonRepeatDelay";
      this.numericUpDownButtonRepeatDelay.Size = new System.Drawing.Size(80, 20);
      this.numericUpDownButtonRepeatDelay.TabIndex = 1;
      this.numericUpDownButtonRepeatDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.numericUpDownButtonRepeatDelay.ThousandsSeparator = true;
      this.toolTips.SetToolTip(this.numericUpDownButtonRepeatDelay, "When the button is held this is the time between the first press and the first re" +
              "peat");
      this.numericUpDownButtonRepeatDelay.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      // 
      // numericUpDownButtonHeldDelay
      // 
      this.numericUpDownButtonHeldDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericUpDownButtonHeldDelay.Location = new System.Drawing.Point(144, 56);
      this.numericUpDownButtonHeldDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.numericUpDownButtonHeldDelay.Name = "numericUpDownButtonHeldDelay";
      this.numericUpDownButtonHeldDelay.Size = new System.Drawing.Size(80, 20);
      this.numericUpDownButtonHeldDelay.TabIndex = 3;
      this.numericUpDownButtonHeldDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.numericUpDownButtonHeldDelay.ThousandsSeparator = true;
      this.toolTips.SetToolTip(this.numericUpDownButtonHeldDelay, "When the button is held this is the time between repeats");
      this.numericUpDownButtonHeldDelay.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.Location = new System.Drawing.Point(128, 264);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(64, 24);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(200, 264);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(64, 24);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // groupBoxRemoteTiming
      // 
      this.groupBoxRemoteTiming.Controls.Add(this.labelButtonRepeatDelay);
      this.groupBoxRemoteTiming.Controls.Add(this.numericUpDownButtonHeldDelay);
      this.groupBoxRemoteTiming.Controls.Add(this.labelButtonHeldDelay);
      this.groupBoxRemoteTiming.Controls.Add(this.numericUpDownButtonRepeatDelay);
      this.groupBoxRemoteTiming.Location = new System.Drawing.Point(8, 88);
      this.groupBoxRemoteTiming.Name = "groupBoxRemoteTiming";
      this.groupBoxRemoteTiming.Size = new System.Drawing.Size(232, 88);
      this.groupBoxRemoteTiming.TabIndex = 3;
      this.groupBoxRemoteTiming.TabStop = false;
      this.groupBoxRemoteTiming.Text = "Remote button timing (in milliseconds)";
      // 
      // numericUpDownLearnTimeout
      // 
      this.numericUpDownLearnTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.numericUpDownLearnTimeout.Location = new System.Drawing.Point(152, 8);
      this.numericUpDownLearnTimeout.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
      this.numericUpDownLearnTimeout.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.numericUpDownLearnTimeout.Name = "numericUpDownLearnTimeout";
      this.numericUpDownLearnTimeout.Size = new System.Drawing.Size(88, 20);
      this.numericUpDownLearnTimeout.TabIndex = 1;
      this.numericUpDownLearnTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.numericUpDownLearnTimeout.ThousandsSeparator = true;
      this.toolTips.SetToolTip(this.numericUpDownLearnTimeout, "When teaching IR commands this is how long before the process times out");
      this.numericUpDownLearnTimeout.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
      // 
      // numericUpDownKeyHeldDelay
      // 
      this.numericUpDownKeyHeldDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericUpDownKeyHeldDelay.Location = new System.Drawing.Point(144, 56);
      this.numericUpDownKeyHeldDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.numericUpDownKeyHeldDelay.Name = "numericUpDownKeyHeldDelay";
      this.numericUpDownKeyHeldDelay.Size = new System.Drawing.Size(80, 20);
      this.numericUpDownKeyHeldDelay.TabIndex = 3;
      this.numericUpDownKeyHeldDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.numericUpDownKeyHeldDelay.ThousandsSeparator = true;
      this.toolTips.SetToolTip(this.numericUpDownKeyHeldDelay, "When a key is held this is the time between repeats");
      this.numericUpDownKeyHeldDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      // 
      // numericUpDownKeyRepeatDelay
      // 
      this.numericUpDownKeyRepeatDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericUpDownKeyRepeatDelay.Location = new System.Drawing.Point(144, 24);
      this.numericUpDownKeyRepeatDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.numericUpDownKeyRepeatDelay.Name = "numericUpDownKeyRepeatDelay";
      this.numericUpDownKeyRepeatDelay.Size = new System.Drawing.Size(80, 20);
      this.numericUpDownKeyRepeatDelay.TabIndex = 1;
      this.numericUpDownKeyRepeatDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.numericUpDownKeyRepeatDelay.ThousandsSeparator = true;
      this.toolTips.SetToolTip(this.numericUpDownKeyRepeatDelay, "When a key is held this is the time between the first press and the first repeat");
      this.numericUpDownKeyRepeatDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      // 
      // checkBoxHandleKeyboardLocal
      // 
      this.checkBoxHandleKeyboardLocal.AutoSize = true;
      this.checkBoxHandleKeyboardLocal.Checked = true;
      this.checkBoxHandleKeyboardLocal.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxHandleKeyboardLocal.Location = new System.Drawing.Point(8, 40);
      this.checkBoxHandleKeyboardLocal.Name = "checkBoxHandleKeyboardLocal";
      this.checkBoxHandleKeyboardLocal.Size = new System.Drawing.Size(139, 17);
      this.checkBoxHandleKeyboardLocal.TabIndex = 1;
      this.checkBoxHandleKeyboardLocal.Text = "Handle keyboard locally";
      this.toolTips.SetToolTip(this.checkBoxHandleKeyboardLocal, "Act on key presses locally (on the machine Input Servie is running on)");
      this.checkBoxHandleKeyboardLocal.UseVisualStyleBackColor = true;
      // 
      // checkBoxHandleMouseLocal
      // 
      this.checkBoxHandleMouseLocal.AutoSize = true;
      this.checkBoxHandleMouseLocal.Checked = true;
      this.checkBoxHandleMouseLocal.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxHandleMouseLocal.Location = new System.Drawing.Point(8, 40);
      this.checkBoxHandleMouseLocal.Name = "checkBoxHandleMouseLocal";
      this.checkBoxHandleMouseLocal.Size = new System.Drawing.Size(126, 17);
      this.checkBoxHandleMouseLocal.TabIndex = 1;
      this.checkBoxHandleMouseLocal.Text = "Handle mouse locally";
      this.toolTips.SetToolTip(this.checkBoxHandleMouseLocal, "Act on mouse locally (on the machine Input Service is running on)");
      this.checkBoxHandleMouseLocal.UseVisualStyleBackColor = true;
      // 
      // numericUpDownMouseSensitivity
      // 
      this.numericUpDownMouseSensitivity.DecimalPlaces = 1;
      this.numericUpDownMouseSensitivity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.numericUpDownMouseSensitivity.Location = new System.Drawing.Point(160, 64);
      this.numericUpDownMouseSensitivity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numericUpDownMouseSensitivity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
      this.numericUpDownMouseSensitivity.Name = "numericUpDownMouseSensitivity";
      this.numericUpDownMouseSensitivity.Size = new System.Drawing.Size(80, 20);
      this.numericUpDownMouseSensitivity.TabIndex = 3;
      this.numericUpDownMouseSensitivity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.toolTips.SetToolTip(this.numericUpDownMouseSensitivity, "Multiply mouse movements by this number");
      this.numericUpDownMouseSensitivity.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
      // 
      // checkBoxDisableMCEServices
      // 
      this.checkBoxDisableMCEServices.AutoSize = true;
      this.checkBoxDisableMCEServices.Checked = true;
      this.checkBoxDisableMCEServices.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxDisableMCEServices.Location = new System.Drawing.Point(8, 40);
      this.checkBoxDisableMCEServices.Name = "checkBoxDisableMCEServices";
      this.checkBoxDisableMCEServices.Size = new System.Drawing.Size(216, 17);
      this.checkBoxDisableMCEServices.TabIndex = 2;
      this.checkBoxDisableMCEServices.Text = "Disable Windows Media Center services";
      this.toolTips.SetToolTip(this.checkBoxDisableMCEServices, "Disable Microsoft Windows Media Center services to prevent interference with the " +
              "Input Service");
      this.checkBoxDisableMCEServices.UseVisualStyleBackColor = true;
      // 
      // checkBoxEnableRemote
      // 
      this.checkBoxEnableRemote.AutoSize = true;
      this.checkBoxEnableRemote.Checked = true;
      this.checkBoxEnableRemote.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxEnableRemote.Location = new System.Drawing.Point(8, 8);
      this.checkBoxEnableRemote.Name = "checkBoxEnableRemote";
      this.checkBoxEnableRemote.Size = new System.Drawing.Size(155, 17);
      this.checkBoxEnableRemote.TabIndex = 0;
      this.checkBoxEnableRemote.Text = "Enable remote control input";
      this.toolTips.SetToolTip(this.checkBoxEnableRemote, "Decode remote control button presses");
      this.checkBoxEnableRemote.UseVisualStyleBackColor = true;
      // 
      // checkBoxEnableKeyboard
      // 
      this.checkBoxEnableKeyboard.AutoSize = true;
      this.checkBoxEnableKeyboard.Checked = true;
      this.checkBoxEnableKeyboard.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxEnableKeyboard.Location = new System.Drawing.Point(8, 8);
      this.checkBoxEnableKeyboard.Name = "checkBoxEnableKeyboard";
      this.checkBoxEnableKeyboard.Size = new System.Drawing.Size(132, 17);
      this.checkBoxEnableKeyboard.TabIndex = 0;
      this.checkBoxEnableKeyboard.Text = "Enable keyboard input";
      this.toolTips.SetToolTip(this.checkBoxEnableKeyboard, "Decode remote keyboard input");
      this.checkBoxEnableKeyboard.UseVisualStyleBackColor = true;
      // 
      // checkBoxEnableMouse
      // 
      this.checkBoxEnableMouse.AutoSize = true;
      this.checkBoxEnableMouse.Checked = true;
      this.checkBoxEnableMouse.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxEnableMouse.Location = new System.Drawing.Point(8, 8);
      this.checkBoxEnableMouse.Name = "checkBoxEnableMouse";
      this.checkBoxEnableMouse.Size = new System.Drawing.Size(119, 17);
      this.checkBoxEnableMouse.TabIndex = 0;
      this.checkBoxEnableMouse.Text = "Enable mouse input";
      this.toolTips.SetToolTip(this.checkBoxEnableMouse, "Decode remote mouse input");
      this.checkBoxEnableMouse.UseVisualStyleBackColor = true;
      // 
      // checkBoxUseSystemRatesRemote
      // 
      this.checkBoxUseSystemRatesRemote.AutoSize = true;
      this.checkBoxUseSystemRatesRemote.Location = new System.Drawing.Point(8, 64);
      this.checkBoxUseSystemRatesRemote.Name = "checkBoxUseSystemRatesRemote";
      this.checkBoxUseSystemRatesRemote.Size = new System.Drawing.Size(187, 17);
      this.checkBoxUseSystemRatesRemote.TabIndex = 2;
      this.checkBoxUseSystemRatesRemote.Text = "Use system keyboard rate settings";
      this.toolTips.SetToolTip(this.checkBoxUseSystemRatesRemote, "Use the system keyboard repeat rate settings for remote button timing");
      this.checkBoxUseSystemRatesRemote.UseVisualStyleBackColor = true;
      this.checkBoxUseSystemRatesRemote.CheckedChanged += new System.EventHandler(this.checkBoxUseSystemRatesRemote_CheckedChanged);
      // 
      // checkBoxUseSystemRatesKeyboard
      // 
      this.checkBoxUseSystemRatesKeyboard.AutoSize = true;
      this.checkBoxUseSystemRatesKeyboard.Location = new System.Drawing.Point(8, 64);
      this.checkBoxUseSystemRatesKeyboard.Name = "checkBoxUseSystemRatesKeyboard";
      this.checkBoxUseSystemRatesKeyboard.Size = new System.Drawing.Size(187, 17);
      this.checkBoxUseSystemRatesKeyboard.TabIndex = 2;
      this.checkBoxUseSystemRatesKeyboard.Text = "Use system keyboard rate settings";
      this.toolTips.SetToolTip(this.checkBoxUseSystemRatesKeyboard, "Use the system keyboard repeat rate settings for remote keyboard repeat rates");
      this.checkBoxUseSystemRatesKeyboard.UseVisualStyleBackColor = true;
      this.checkBoxUseSystemRatesKeyboard.CheckedChanged += new System.EventHandler(this.checkBoxUseSystemRatesKeyboard_CheckedChanged);
      // 
      // checkBoxDisableAutomaticButtons
      // 
      this.checkBoxDisableAutomaticButtons.AutoSize = true;
      this.checkBoxDisableAutomaticButtons.Checked = true;
      this.checkBoxDisableAutomaticButtons.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxDisableAutomaticButtons.Location = new System.Drawing.Point(8, 40);
      this.checkBoxDisableAutomaticButtons.Name = "checkBoxDisableAutomaticButtons";
      this.checkBoxDisableAutomaticButtons.Size = new System.Drawing.Size(148, 17);
      this.checkBoxDisableAutomaticButtons.TabIndex = 1;
      this.checkBoxDisableAutomaticButtons.Text = "Disable automatic buttons";
      this.toolTips.SetToolTip(this.checkBoxDisableAutomaticButtons, "Prevent the operating system from automatically handling some buttons");
      this.checkBoxDisableAutomaticButtons.UseVisualStyleBackColor = true;
      // 
      // labelLearnIRTimeout
      // 
      this.labelLearnIRTimeout.Location = new System.Drawing.Point(8, 8);
      this.labelLearnIRTimeout.Name = "labelLearnIRTimeout";
      this.labelLearnIRTimeout.Size = new System.Drawing.Size(144, 20);
      this.labelLearnIRTimeout.TabIndex = 0;
      this.labelLearnIRTimeout.Text = "Learn IR timeout:";
      this.labelLearnIRTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabControl
      // 
      this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl.Controls.Add(this.tabPageBasic);
      this.tabControl.Controls.Add(this.tabPageRemote);
      this.tabControl.Controls.Add(this.tabPageKeyboard);
      this.tabControl.Controls.Add(this.tabPageMouse);
      this.tabControl.Location = new System.Drawing.Point(8, 8);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(256, 248);
      this.tabControl.TabIndex = 0;
      // 
      // tabPageBasic
      // 
      this.tabPageBasic.Controls.Add(this.groupBoxMceServices);
      this.tabPageBasic.Controls.Add(this.checkBoxDisableMCEServices);
      this.tabPageBasic.Controls.Add(this.labelLearnIRTimeout);
      this.tabPageBasic.Controls.Add(this.numericUpDownLearnTimeout);
      this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
      this.tabPageBasic.Name = "tabPageBasic";
      this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageBasic.Size = new System.Drawing.Size(248, 222);
      this.tabPageBasic.TabIndex = 0;
      this.tabPageBasic.Text = "Basic";
      this.toolTips.SetToolTip(this.tabPageBasic, "Basic settings");
      this.tabPageBasic.UseVisualStyleBackColor = true;
      // 
      // tabPageRemote
      // 
      this.tabPageRemote.Controls.Add(this.checkBoxDisableAutomaticButtons);
      this.tabPageRemote.Controls.Add(this.checkBoxUseSystemRatesRemote);
      this.tabPageRemote.Controls.Add(this.checkBoxEnableRemote);
      this.tabPageRemote.Controls.Add(this.groupBoxRemoteTiming);
      this.tabPageRemote.Location = new System.Drawing.Point(4, 22);
      this.tabPageRemote.Name = "tabPageRemote";
      this.tabPageRemote.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageRemote.Size = new System.Drawing.Size(248, 222);
      this.tabPageRemote.TabIndex = 1;
      this.tabPageRemote.Text = "Remote";
      this.toolTips.SetToolTip(this.tabPageRemote, "Remote control settings");
      this.tabPageRemote.UseVisualStyleBackColor = true;
      // 
      // tabPageKeyboard
      // 
      this.tabPageKeyboard.Controls.Add(this.checkBoxKeyboardQwertz);
      this.tabPageKeyboard.Controls.Add(this.checkBoxUseSystemRatesKeyboard);
      this.tabPageKeyboard.Controls.Add(this.checkBoxHandleKeyboardLocal);
      this.tabPageKeyboard.Controls.Add(this.checkBoxEnableKeyboard);
      this.tabPageKeyboard.Controls.Add(this.groupBoxKeypressTiming);
      this.tabPageKeyboard.Location = new System.Drawing.Point(4, 22);
      this.tabPageKeyboard.Name = "tabPageKeyboard";
      this.tabPageKeyboard.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageKeyboard.Size = new System.Drawing.Size(248, 222);
      this.tabPageKeyboard.TabIndex = 2;
      this.tabPageKeyboard.Text = "Keyboard";
      this.toolTips.SetToolTip(this.tabPageKeyboard, "Keyboard settings for use with the MCE Replacement Driver");
      this.tabPageKeyboard.UseVisualStyleBackColor = true;
      // 
      // groupBoxKeypressTiming
      // 
      this.groupBoxKeypressTiming.Controls.Add(this.labelKeyRepeatDelay);
      this.groupBoxKeypressTiming.Controls.Add(this.numericUpDownKeyHeldDelay);
      this.groupBoxKeypressTiming.Controls.Add(this.labelKeyHeldDelay);
      this.groupBoxKeypressTiming.Controls.Add(this.numericUpDownKeyRepeatDelay);
      this.groupBoxKeypressTiming.Location = new System.Drawing.Point(8, 88);
      this.groupBoxKeypressTiming.Name = "groupBoxKeypressTiming";
      this.groupBoxKeypressTiming.Size = new System.Drawing.Size(232, 88);
      this.groupBoxKeypressTiming.TabIndex = 3;
      this.groupBoxKeypressTiming.TabStop = false;
      this.groupBoxKeypressTiming.Text = "Key press timing (in milliseconds)";
      // 
      // labelKeyRepeatDelay
      // 
      this.labelKeyRepeatDelay.Location = new System.Drawing.Point(8, 24);
      this.labelKeyRepeatDelay.Name = "labelKeyRepeatDelay";
      this.labelKeyRepeatDelay.Size = new System.Drawing.Size(128, 20);
      this.labelKeyRepeatDelay.TabIndex = 0;
      this.labelKeyRepeatDelay.Text = "Key repeat delay:";
      this.labelKeyRepeatDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelKeyHeldDelay
      // 
      this.labelKeyHeldDelay.Location = new System.Drawing.Point(8, 56);
      this.labelKeyHeldDelay.Name = "labelKeyHeldDelay";
      this.labelKeyHeldDelay.Size = new System.Drawing.Size(128, 20);
      this.labelKeyHeldDelay.TabIndex = 2;
      this.labelKeyHeldDelay.Text = "Key held delay:";
      this.labelKeyHeldDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabPageMouse
      // 
      this.tabPageMouse.Controls.Add(this.labelMouseSensitivity);
      this.tabPageMouse.Controls.Add(this.numericUpDownMouseSensitivity);
      this.tabPageMouse.Controls.Add(this.checkBoxHandleMouseLocal);
      this.tabPageMouse.Controls.Add(this.checkBoxEnableMouse);
      this.tabPageMouse.Location = new System.Drawing.Point(4, 22);
      this.tabPageMouse.Name = "tabPageMouse";
      this.tabPageMouse.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageMouse.Size = new System.Drawing.Size(248, 222);
      this.tabPageMouse.TabIndex = 3;
      this.tabPageMouse.Text = "Mouse";
      this.toolTips.SetToolTip(this.tabPageMouse, "Mouse settings for use with the MCE Replacement Driver");
      this.tabPageMouse.UseVisualStyleBackColor = true;
      // 
      // labelMouseSensitivity
      // 
      this.labelMouseSensitivity.Location = new System.Drawing.Point(8, 64);
      this.labelMouseSensitivity.Name = "labelMouseSensitivity";
      this.labelMouseSensitivity.Size = new System.Drawing.Size(144, 20);
      this.labelMouseSensitivity.TabIndex = 2;
      this.labelMouseSensitivity.Text = "Mouse sensitivity:";
      this.labelMouseSensitivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // radioButtonStopAtStartup
      // 
      this.radioButtonStopAtStartup.AutoSize = true;
      this.radioButtonStopAtStartup.Location = new System.Drawing.Point(8, 48);
      this.radioButtonStopAtStartup.Name = "radioButtonStopAtStartup";
      this.radioButtonStopAtStartup.Size = new System.Drawing.Size(93, 17);
      this.radioButtonStopAtStartup.TabIndex = 1;
      this.radioButtonStopAtStartup.TabStop = true;
      this.radioButtonStopAtStartup.Text = "Stop if running";
      this.radioButtonStopAtStartup.UseVisualStyleBackColor = true;
      // 
      // groupBoxMceServices
      // 
      this.groupBoxMceServices.Controls.Add(this.radioButton1);
      this.groupBoxMceServices.Controls.Add(this.radioButtonDoNothing);
      this.groupBoxMceServices.Controls.Add(this.radioButtonStopAtStartup);
      this.groupBoxMceServices.Enabled = false;
      this.groupBoxMceServices.Location = new System.Drawing.Point(8, 112);
      this.groupBoxMceServices.Name = "groupBoxMceServices";
      this.groupBoxMceServices.Size = new System.Drawing.Size(232, 96);
      this.groupBoxMceServices.TabIndex = 3;
      this.groupBoxMceServices.TabStop = false;
      this.groupBoxMceServices.Text = "Windows Media Center Services";
      // 
      // radioButtonDoNothing
      // 
      this.radioButtonDoNothing.AutoSize = true;
      this.radioButtonDoNothing.Location = new System.Drawing.Point(8, 24);
      this.radioButtonDoNothing.Name = "radioButtonDoNothing";
      this.radioButtonDoNothing.Size = new System.Drawing.Size(77, 17);
      this.radioButtonDoNothing.TabIndex = 0;
      this.radioButtonDoNothing.TabStop = true;
      this.radioButtonDoNothing.Text = "Do nothing";
      this.radioButtonDoNothing.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new System.Drawing.Point(8, 72);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(119, 17);
      this.radioButton1.TabIndex = 2;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Permanently disable";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // checkBoxKeyboardQwertz
      // 
      this.checkBoxKeyboardQwertz.AutoSize = true;
      this.checkBoxKeyboardQwertz.Location = new System.Drawing.Point(8, 184);
      this.checkBoxKeyboardQwertz.Name = "checkBoxKeyboardQwertz";
      this.checkBoxKeyboardQwertz.Size = new System.Drawing.Size(127, 17);
      this.checkBoxKeyboardQwertz.TabIndex = 4;
      this.checkBoxKeyboardQwertz.Text = "Use QWERTZ layout";
      this.toolTips.SetToolTip(this.checkBoxKeyboardQwertz, "Use the QWERTZ keyboard layout instead of QWERTY");
      this.checkBoxKeyboardQwertz.UseVisualStyleBackColor = true;
      // 
      // Configure
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(272, 296);
      this.Controls.Add(this.tabControl);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(280, 330);
      this.Name = "Configure";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Microsoft MCE Configuration";
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButtonRepeatDelay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButtonHeldDelay)).EndInit();
      this.groupBoxRemoteTiming.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearnTimeout)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyHeldDelay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyRepeatDelay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMouseSensitivity)).EndInit();
      this.tabControl.ResumeLayout(false);
      this.tabPageBasic.ResumeLayout(false);
      this.tabPageBasic.PerformLayout();
      this.tabPageRemote.ResumeLayout(false);
      this.tabPageRemote.PerformLayout();
      this.tabPageKeyboard.ResumeLayout(false);
      this.tabPageKeyboard.PerformLayout();
      this.groupBoxKeypressTiming.ResumeLayout(false);
      this.tabPageMouse.ResumeLayout(false);
      this.tabPageMouse.PerformLayout();
      this.groupBoxMceServices.ResumeLayout(false);
      this.groupBoxMceServices.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label labelButtonRepeatDelay;
    private System.Windows.Forms.Label labelButtonHeldDelay;
    private System.Windows.Forms.NumericUpDown numericUpDownButtonRepeatDelay;
    private System.Windows.Forms.NumericUpDown numericUpDownButtonHeldDelay;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.GroupBox groupBoxRemoteTiming;
    private System.Windows.Forms.ToolTip toolTips;
    private System.Windows.Forms.Label labelLearnIRTimeout;
    private System.Windows.Forms.NumericUpDown numericUpDownLearnTimeout;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabPageBasic;
    private System.Windows.Forms.TabPage tabPageRemote;
    private System.Windows.Forms.CheckBox checkBoxEnableRemote;
    private System.Windows.Forms.TabPage tabPageKeyboard;
    private System.Windows.Forms.GroupBox groupBoxKeypressTiming;
    private System.Windows.Forms.Label labelKeyRepeatDelay;
    private System.Windows.Forms.NumericUpDown numericUpDownKeyHeldDelay;
    private System.Windows.Forms.Label labelKeyHeldDelay;
    private System.Windows.Forms.NumericUpDown numericUpDownKeyRepeatDelay;
    private System.Windows.Forms.CheckBox checkBoxHandleKeyboardLocal;
    private System.Windows.Forms.CheckBox checkBoxEnableKeyboard;
    private System.Windows.Forms.TabPage tabPageMouse;
    private System.Windows.Forms.Label labelMouseSensitivity;
    private System.Windows.Forms.NumericUpDown numericUpDownMouseSensitivity;
    private System.Windows.Forms.CheckBox checkBoxHandleMouseLocal;
    private System.Windows.Forms.CheckBox checkBoxEnableMouse;
    private System.Windows.Forms.CheckBox checkBoxDisableMCEServices;
    private System.Windows.Forms.CheckBox checkBoxUseSystemRatesRemote;
    private System.Windows.Forms.CheckBox checkBoxUseSystemRatesKeyboard;
    private System.Windows.Forms.CheckBox checkBoxDisableAutomaticButtons;
    private System.Windows.Forms.GroupBox groupBoxMceServices;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButtonDoNothing;
    private System.Windows.Forms.RadioButton radioButtonStopAtStartup;
    private System.Windows.Forms.CheckBox checkBoxKeyboardQwertz;
  }
}