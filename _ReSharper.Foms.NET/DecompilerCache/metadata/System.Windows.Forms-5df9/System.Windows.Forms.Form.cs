// Type: System.Windows.Forms.Form
// Assembly: System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.Windows.Forms.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    [Designer(
        "System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        , typeof (IRootDesigner))]
    [ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
    [DesignerCategory("Form")]
    [DefaultEvent("Load")]
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [ComVisible(true)]
    [InitializationEvent("Load")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class Form : ContainerControl
    {
        public Form();

        [DefaultValue(null)]
        public IButtonControl AcceptButton { get; set; }

        public static Form ActiveForm { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Form ActiveMdiChild { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowTransparency { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(
            "This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool AutoScale { get; set; }

        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public virtual Size AutoScaleBaseSize { get; set; }

        [Localizable(true)]
        public override bool AutoScroll { get; set; }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public override bool AutoSize { get; set; }

        [Browsable(true)]
        [Localizable(true)]
        [DefaultValue(1)]
        public AutoSizeMode AutoSizeMode { get; set; }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public override AutoValidate AutoValidate { get; set; }

        public override Color BackColor { get; set; }

        [DefaultValue(4)]
        [DispId(-504)]
        public FormBorderStyle FormBorderStyle { get; set; }

        [DefaultValue(null)]
        public IButtonControl CancelButton { get; set; }

        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Size ClientSize { get; set; }

        [DefaultValue(true)]
        public bool ControlBox { get; set; }

        protected override CreateParams CreateParams { get; }
        protected override ImeMode DefaultImeMode { get; }
        protected override Size DefaultSize { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Rectangle DesktopBounds { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Point DesktopLocation { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DialogResult DialogResult { get; set; }

        [DefaultValue(false)]
        public bool HelpButton { get; set; }

        [AmbientValue(null)]
        [Localizable(true)]
        public Icon Icon { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsMdiChild { get; }

        [DefaultValue(false)]
        public bool IsMdiContainer { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsRestrictedWindow { get; }

        [DefaultValue(false)]
        public bool KeyPreview { get; set; }

        [SettingsBindable(true)]
        public new Point Location { get; set; }

        protected Rectangle MaximizedBounds { get; set; }

        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(typeof (Size), "0, 0")]
        public override Size MaximumSize { get; set; }

        [DefaultValue(null)]
        [TypeConverter(typeof (ReferenceConverter))]
        public MenuStrip MainMenuStrip { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Margin { get; set; }

        [Browsable(false)]
        [TypeConverter(typeof (ReferenceConverter))]
        [DefaultValue(null)]
        public MainMenu Menu { get; set; }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        public override Size MinimumSize { get; set; }

        [DefaultValue(true)]
        public bool MaximizeBox { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form[] MdiChildren { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form MdiParent { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MainMenu MergedMenu { get; }

        [DefaultValue(true)]
        public bool MinimizeBox { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modal { get; }

        [DefaultValue(1)]
        [TypeConverter(typeof (OpacityConverter))]
        public double Opacity { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form[] OwnedForms { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Form Owner { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Rectangle RestoreBounds { get; }

        [DefaultValue(false)]
        [Localizable(true)]
        public virtual bool RightToLeftLayout { get; set; }

        [DefaultValue(true)]
        public bool ShowInTaskbar { get; set; }

        [DefaultValue(true)]
        public bool ShowIcon { get; set; }

        [Browsable(false)]
        protected virtual bool ShowWithoutActivation { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizable(false)]
        public new Size Size { get; set; }

        [DefaultValue(0)]
        public SizeGripStyle SizeGripStyle { get; set; }

        [DefaultValue(2)]
        [Localizable(true)]
        public FormStartPosition StartPosition { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int TabIndex { get; set; }

        [DefaultValue(true)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DispId(-516)]
        public new bool TabStop { get; set; }

        [SettingsBindable(true)]
        public override string Text { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool TopLevel { get; set; }

        [DefaultValue(false)]
        public bool TopMost { get; set; }

        public Color TransparencyKey { get; set; }

        [DefaultValue(0)]
        public FormWindowState WindowState { get; set; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void SetVisibleCore(bool value);

        public void Activate();
        protected void ActivateMdiChild(Form form);
        public void AddOwnedForm(Form ownedForm);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void AdjustFormScrollbars(bool displayScrollbars);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete(
            "This method has been deprecated. Use the ApplyAutoScaling method instead.  http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        protected void ApplyAutoScaling();

        public void Close();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void CreateHandle();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void DefWndProc(ref Message m);

        protected override void Dispose(bool disposing);

        [Obsolete(
            "This method has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202"
            )]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static SizeF GetAutoScaleSize(Font font);

        protected internal override bool ProcessMnemonic(char charCode);
        protected void CenterToParent();
        protected void CenterToScreen();
        public void LayoutMdi(MdiLayout value);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnActivated(EventArgs e);

        protected override void OnBackgroundImageChanged(EventArgs e);
        protected override void OnBackgroundImageLayoutChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnClosing(CancelEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnClosed(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnFormClosing(FormClosingEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnFormClosed(FormClosedEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnCreateControl();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnDeactivate(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnEnabledChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnEnter(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnFontChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnHandleCreated(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnHandleDestroyed(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnHelpButtonClicked(CancelEventArgs e);

        protected override void OnLayout(LayoutEventArgs levent);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLoad(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMaximizedBoundsChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMaximumSizeChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMinimumSizeChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnVisibleChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMdiChildActivate(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMenuStart(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnMenuComplete(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnPaint(PaintEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnResize(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnRightToLeftLayoutChanged(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnShown(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnTextChanged(EventArgs e);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData);
        protected override bool ProcessDialogKey(Keys keyData);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override bool ProcessDialogChar(char charCode);

        protected override bool ProcessKeyPreview(ref Message m);
        protected override bool ProcessTabKey(bool forward);
        public void RemoveOwnedForm(Form ownedForm);
        protected override void Select(bool directed, bool forward);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void ScaleCore(float x, float y);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void SetClientSizeCore(int x, int y);

        public void SetDesktopBounds(int x, int y, int width, int height);
        public void SetDesktopLocation(int x, int y);
        public void Show(IWin32Window owner);
        public DialogResult ShowDialog();
        public DialogResult ShowDialog(IWin32Window owner);
        public override string ToString();
        protected override void UpdateDefaultButton();

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnResizeBegin(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnResizeEnd(EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnStyleChanged(EventArgs e);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override bool ValidateChildren();

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public override bool ValidateChildren(ValidationConstraints validationConstraints);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void WndProc(ref Message m);

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public new event EventHandler AutoSizeChanged;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler AutoValidateChanged;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public event CancelEventHandler HelpButtonClicked;

        public event EventHandler MaximizedBoundsChanged;
        public event EventHandler MaximumSizeChanged;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler MarginChanged;

        public event EventHandler MinimumSizeChanged;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TabIndexChanged;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler TabStopChanged;

        public event EventHandler Activated;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event CancelEventHandler Closing;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler Closed;

        public event EventHandler Deactivate;
        public event FormClosingEventHandler FormClosing;
        public event FormClosedEventHandler FormClosed;
        public event EventHandler Load;
        public event EventHandler MdiChildActivate;

        [Browsable(false)]
        public event EventHandler MenuComplete;

        [Browsable(false)]
        public event EventHandler MenuStart;

        public event InputLanguageChangedEventHandler InputLanguageChanged;
        public event InputLanguageChangingEventHandler InputLanguageChanging;
        public event EventHandler RightToLeftLayoutChanged;
        public event EventHandler Shown;
        public event EventHandler ResizeBegin;
        public event EventHandler ResizeEnd;

        #region Nested type: ControlCollection

        [ComVisible(false)]
        public class ControlCollection : Control.ControlCollection
        {
            public ControlCollection(Form owner);
            public override void Add(Control value);
            public override void Remove(Control value);
        }

        #endregion
    }
}
