using System;
using System.Linq;


using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ZPW.ViewModel
{
    
    #region Image resize and save
    public class ImageHandler
    { 
     
        public static void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);
    
            Bitmap newImage = new (newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters encoderParameters = new(1);

            EncoderParameter encoderParameter = new(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }
    #endregion //Image
}
