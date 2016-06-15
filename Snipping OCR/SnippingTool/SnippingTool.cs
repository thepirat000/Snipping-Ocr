using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Snipping_OCR
{
    public partial class SnippingTool : Form
    {
        public static event EventHandler Cancel;
        public static event EventHandler<AreaSelectedEventArgs> AreaSelected;
        public static Image Image { get; set; }
        private Rectangle rcSelect;
        private Point pntStart;
        protected virtual void OnCancel(EventArgs e)
        {
            Cancel?.Invoke(this, e);
        }

        protected virtual void OnAreaSelected(AreaSelectedEventArgs e)
        {
            AreaSelected?.Invoke(this, e);
        }

        private static SnippingTool[] _forms;

        public static void Snip()
        {
            var mi = ScreenHelper.GetMonitorsInfo();
            _forms = new SnippingTool[mi.Count];
            for (int i = 0; i < mi.Count; i++)
            {
                int horizontalRes = mi[i].HorizontalResolution;
                int verticalRes = mi[i].VerticalResolution;
                using (Bitmap bmp = new Bitmap(horizontalRes, verticalRes, PixelFormat.Format32bppPArgb))
                {
                    using (Graphics gr = Graphics.FromImage(bmp))
                    {
                        gr.CopyFromScreen(mi[i].MonitorArea.left, mi[i].MonitorArea.top, 0, 0, bmp.Size);
                    }
                    _forms[i] = new SnippingTool(bmp, mi[i].MonitorArea.left, mi[i].MonitorArea.top,
                                                 horizontalRes, verticalRes);
                    _forms[i].Show();
                    _forms[i].TopMost = true;
                    _forms[i].Invalidate();
                    _forms[i].Update();
                }
            }
        }

        private void CloseForms()
        {
            for (int i = 0; i < _forms.Length; i++)
            {
                _forms[i].Dispose();
            }
        }
        public SnippingTool(Image screenShot, int x, int y, int width, int height)
        {
            InitializeComponent();
            BackgroundImage = (Image)screenShot.Clone();
            BackgroundImageLayout = ImageLayout.Stretch;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            SetBounds(x, y, width, height);
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            Cursor = Cursors.Cross;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Start the snip on mouse down
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            pntStart = e.Location;
            rcSelect = new Rectangle(e.Location, new Size(0, 0));
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Modify the selection on mouse move
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            int x1 = Math.Min(e.X, pntStart.X);
            int y1 = Math.Min(e.Y, pntStart.Y);
            int x2 = Math.Max(e.X, pntStart.X);
            int y2 = Math.Max(e.Y, pntStart.Y);
            rcSelect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Complete the snip on mouse-up
            if (rcSelect.Width <= 0 || rcSelect.Height <= 0)
            {
                CloseForms();
                OnCancel(new EventArgs());
                return;
            }
            Image = new Bitmap(rcSelect.Width, rcSelect.Height);
            var wFix = BackgroundImage.Width / (double)Width;
            var hFix = BackgroundImage.Height / (double)Height;
            using (Graphics gr = Graphics.FromImage(Image))
            {
                
                gr.DrawImage(BackgroundImage, 
                    new Rectangle(0, 0, Image.Width, Image.Height),
                    new Rectangle((int)(rcSelect.X * wFix), (int)(rcSelect.Y * hFix), (int)(rcSelect.Width * wFix), (int)(rcSelect.Height * hFix)),  
                    GraphicsUnit.Pixel);
            }
            CloseForms();
            OnAreaSelected(new AreaSelectedEventArgs { Image = Image });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the current selection
            using (Brush br = new SolidBrush(Color.FromArgb(120, Color.White)))
            {
                int x1 = rcSelect.X;
                int x2 = rcSelect.X + rcSelect.Width;
                int y1 = rcSelect.Y;
                int y2 = rcSelect.Y + rcSelect.Height;
                e.Graphics.FillRectangle(br, new Rectangle(0, 0, x1, this.Height));
                e.Graphics.FillRectangle(br, new Rectangle(x2, 0, this.Width - x2, this.Height));
                e.Graphics.FillRectangle(br, new Rectangle(x1, 0, x2 - x1, y1));
                e.Graphics.FillRectangle(br, new Rectangle(x1, y2, x2 - x1, this.Height - y2));
            }
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, rcSelect);
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
    }
}