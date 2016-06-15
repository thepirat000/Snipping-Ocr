namespace Snipping_OCR
{
    public class OcrResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Text { get; set; }
        public float Confidence { get; set; }
    }
}