// Original from: http://stackoverflow.com/a/3124252/122195
// Modified version multiple monitors aware and text scaling aware
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Snipping_OCR
{
    public sealed partial class SnippingTool : Form
    {
        #region Public members
        public static event EventHandler Cancel;
        public static event EventHandler AreaSelected;
        public static Image Image { get; set; }
        #endregion

        #region Private members
        private static SnippingTool[] _forms;
        private Rectangle _rectSelection;
        private Point _pointStart;
        #endregion

        #region Constructor
        public SnippingTool(Image screenShot, int x, int y, int width, int height)
        {
            InitializeComponent();
            BackgroundImage = screenShot;
            BackgroundImageLayout = ImageLayout.Stretch;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            SetBounds(x, y, width, height);
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            Cursor = Cursors.Cross;
            TopMost = true;
        }
        #endregion

        #region Private methods
        private void OnCancel(EventArgs e)
        {
            Cancel?.Invoke(this, e);
        }

        private void OnAreaSelected(EventArgs e)
        {
            AreaSelected?.Invoke(this, e);
        }

        private void CloseForms()
        {
            for (int i = 0; i < _forms.Length; i++)
            {
                _forms[i].Dispose();
            }
        }
        #endregion

        #region Public methods
        public static void Snip()
        {
            var screens = ScreenHelper.GetMonitorsInfo();
            _forms = new SnippingTool[screens.Count];
            for (int i = 0; i < screens.Count; i++)
            {
                int hRes = screens[i].HorizontalResolution;
                int vRes = screens[i].VerticalResolution;
                int top = screens[i].MonitorArea.top;
                int left = screens[i].MonitorArea.left;
                var bmp = new Bitmap(hRes, vRes, PixelFormat.Format32bppPArgb);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(left, top, 0, 0, bmp.Size);
                }
                _forms[i] = new SnippingTool(bmp, left, top, hRes, vRes);
                _forms[i].Show();
            }
        }
        #endregion

        #region Overrides
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Start the snip on mouse down
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            _pointStart = e.Location;
            _rectSelection = new Rectangle(e.Location, new Size(0, 0));
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Modify the selection on mouse move
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            int x1 = Math.Min(e.X, _pointStart.X);
            int y1 = Math.Min(e.Y, _pointStart.Y);
            int x2 = Math.Max(e.X, _pointStart.X);
            int y2 = Math.Max(e.Y, _pointStart.Y);
            _rectSelection = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Complete the snip on mouse-up
            if (_rectSelection.Width <= 0 || _rectSelection.Height <= 0)
            {
                CloseForms();
                OnCancel(new EventArgs());
                return;
            }
            Image = new Bitmap(_rectSelection.Width, _rectSelection.Height);
            var hScale = BackgroundImage.Width / (double)Width;
            var vScale = BackgroundImage.Height / (double)Height;
            using (Graphics gr = Graphics.FromImage(Image))
            {
                
                gr.DrawImage(BackgroundImage, 
                    new Rectangle(0, 0, Image.Width, Image.Height),
                    new Rectangle((int)(_rectSelection.X * hScale), (int)(_rectSelection.Y * vScale), (int)(_rectSelection.Width * hScale), (int)(_rectSelection.Height * vScale)),  
                    GraphicsUnit.Pixel);
            }
            CloseForms();
            OnAreaSelected(new EventArgs());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the current selection
            using (Brush br = new SolidBrush(Color.FromArgb(120, Color.White)))
            {
                int x1 = _rectSelection.X;
                int x2 = _rectSelection.X + _rectSelection.Width;
                int y1 = _rectSelection.Y;
                int y2 = _rectSelection.Y + _rectSelection.Height;
                e.Graphics.FillRectangle(br, new Rectangle(0, 0, x1, Height));
                e.Graphics.FillRectangle(br, new Rectangle(x2, 0, Width - x2, Height));
                e.Graphics.FillRectangle(br, new Rectangle(x1, 0, x2 - x1, y1));
                e.Graphics.FillRectangle(br, new Rectangle(x1, y2, x2 - x1, Height - y2));
            }
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, _rectSelection);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Allow canceling the snip with the Escape key
            if (keyData == Keys.Escape)
            {
                Image = null;
                CloseForms();
                OnCancel(new EventArgs());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}