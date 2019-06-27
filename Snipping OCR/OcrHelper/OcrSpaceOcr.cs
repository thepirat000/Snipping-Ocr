using Newtonsoft.Json;
using Snipping_OCR.OcrHelper.OcrSpace;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Snipping_OCR
{
    public class OcrSpaceOcr : IOcr
    {
        //https://ocr.space/ocrapi
        private const string APIURLFree = "https://api.ocr.space/parse/image";
        private static byte[] ak = new byte[] { 56, 55, 100, 50, 55, 101, 101, 102, 56, 48, 56, 56, 57, 53, 55 };
        
        private static HttpClient _httpClient = new HttpClient();

        public OcrResult Process(Image image, string language = "eng")
        {
            var result = UploadFileAsync(image, language);
            var stringContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var response = JsonConvert.DeserializeObject<OCRSpaceResponse>(stringContent);
            return new OcrResult()
            {
                Confidence = 1,
                Error = response.ErrorMessage,
                Success = response.OCRExitCode == 1,
                Text = response.OCRExitCode == 1 ? string.Join(Environment.NewLine, response.ParsedResults.Select(x => x.ParsedText)) : null
            };
        }

        public HttpResponseMessage UploadFileAsync(Image image, string language = "eng")
        {
            // we need to send a request with multipart/form-data
            var multiForm = new MultipartFormDataContent();

            // add API method parameters
            multiForm.Headers.Add("apikey", Encoding.ASCII.GetString(ak));

            multiForm.Add(new StringContent(language), "language");
            multiForm.Add(new StringContent("false"), "isOverlayRequired");

            // add file and directly upload it
            var stream = ToStream(image, System.Drawing.Imaging.ImageFormat.Jpeg);
            multiForm.Add(new StreamContent(stream), "file", "test1.jpg");
            
            return _httpClient.PostAsync(APIURLFree, multiForm).GetAwaiter().GetResult();

        }

        public static Stream ToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
