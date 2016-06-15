using System;
using System.Drawing;

namespace Snipping_OCR
{
    public class AreaSelectedEventArgs : EventArgs
    {
        public Image Image { get; set; }
    }
}