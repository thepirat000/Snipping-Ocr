using System.Drawing;

namespace Snipping_OCR
{
    public interface IOcr
    {
        OcrResult Process(Image image, string language);
    }
}
