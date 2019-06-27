using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Snipping_OCR
{
    public class OcrTesseract : IOcr
    {
        public OcrResult Process(string filePath, string language = "eng")
        {
            using (var pix = Pix.LoadFromFile(filePath))
            {
                return ProcessProc(pix, language);
            }
        }

        public OcrResult Process(Image image, string language = "eng")
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

        private OcrResult ProcessProc(Pix pix, string language = "eng")
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
