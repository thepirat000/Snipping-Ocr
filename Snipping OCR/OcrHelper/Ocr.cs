using System;
using System.Drawing;
using System.IO;
using Tesseract;

namespace Snipping_OCR
{
    public class Ocr
    {
        public static OcrResult Process(string filePath, string language = "eng")
        {
            using (var pix = Pix.LoadFromFile(filePath))
            {
                return ProcessProc(pix, language);
            }
        }
        public static OcrResult Process(Image image, string language = "eng")
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);
                var array = ms.ToArray();
                using (var pix = Pix.LoadTiffFromMemory(array))
                {
                    return ProcessProc(pix, language);
                }
            }
        }
        private static OcrResult ProcessProc(Pix pix, string language = "eng")
        {
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", language, EngineMode.Default))
                {
                    using (var page = engine.Process(pix))
                    {
                        var text = page.GetText();
                        return new OcrResult()
                        {
                            Text = text,
                            Confidence = page.GetMeanConfidence(),
                            Success = true
                        };
                    }
                }
            }
            catch (Exception e)
            {
                return new OcrResult()
                {
                    Error = e.Message,
                    Success = false
                };
            }
        }
    }
}
