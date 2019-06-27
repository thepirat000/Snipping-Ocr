namespace Snipping_OCR
{
    public static class OcrFactory
    {
        public static IOcr GetOcr(string type)
        {
            // switch on type
            if (type == "SpaceOCR")
                return new OcrSpaceOcr();
            else
                return new OcrTesseract();
        }
    }
}
