using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;

using System.Drawing;
using System.Text;
using System;

namespace Lab17.Steganography
{
    public class AditiveImageSteganography
    {
        private const int ALPHA = 1; // coeff for changing image

        public string GetMsgFromImage(Bitmap originalImage, Bitmap imageWithHiddenMsg)
        {
            var msg = new StringBuilder();

            for (int j = 0; j < originalImage.Height; j++)
            {
                for (int i = 0; i < originalImage.Width; i++)
                {
                    int originalPixelAsInteger = originalImage.GetPixel(i, j).ToArgb();
                    int modifiedPixelAsInteger = imageWithHiddenMsg.GetPixel(i, j).ToArgb();

                    if ( originalPixelAsInteger == modifiedPixelAsInteger ) continue;

                    char hiddenLetter = (char) ( ( modifiedPixelAsInteger - originalPixelAsInteger ) / ALPHA );

                    msg.Append(hiddenLetter);
                }
            }

            return msg.ToString();
        }

        public Bitmap GenerateImageWithHiddenMsg(string msg, Bitmap image)
        {
            var indexesToChange = chooseIndexesOfImageToChange(msg.Length, image.Height * image.Width);
            var imageWithHiddenMsg = (Bitmap) image.Clone();

            for (int i = 0; i < indexesToChange.Length; i++)
            {
                int x = indexesToChange[i] / image.Width;
                int y = indexesToChange[i] % image.Width;

                int pixelAsInteger = image.GetPixel(y, x).ToArgb() + msg[i] * ALPHA;

                imageWithHiddenMsg.SetPixel(y, x, Color.FromArgb(pixelAsInteger));
            }
            
            return imageWithHiddenMsg;
        }

        private int[] chooseIndexesOfImageToChange(int msgLength, int pixelCount)
        {
            var chosenIndexes = new int[msgLength];
            int step = pixelCount / msgLength;

            for (int i = 0; i < msgLength; i++)
            {
                chosenIndexes[i] = step * i;
            }
            return chosenIndexes;
        }
    }
}