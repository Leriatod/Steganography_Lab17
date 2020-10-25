using System.Text;
using System.Drawing;
using Lab17.Extensions;
using System;

namespace Lab17.Steganography
{
    public class LsbImageSteganography
    {
        private const int BITS_PER_CHAR = 8;
        private const int RGB = 3;
        private int getNewColorByMsgBit(int colorNumber, string msgBits, int index)
        {
            return colorNumber - colorNumber % 2 + ( msgBits[index] == '1' ? 1 : 0 );
        }

        // TO BE REFACTORED
        public Bitmap GenerateImageWithHiddenMsg(string msg, Bitmap image)
        {
            var imageWithHiddenMsg = (Bitmap) image.Clone();

            string msgBits          = msg.ToASCIIBinaryString();
            int bitCounter          = 0;
            int pixelsFilledCounter = 0;

            while (bitCounter < msgBits.Length)
            {
                int x = pixelsFilledCounter / image.Width;
                int y = pixelsFilledCounter % image.Width;

                var pixelToChange = image.GetPixel(y, x);

                // replacing the last bit with bit of msg
                int R = getNewColorByMsgBit(pixelToChange.R, msgBits, bitCounter);
                bitCounter++;

                int G = pixelToChange.G;
                int B = pixelToChange.B;

                if (bitCounter == msgBits.Length) 
                {
                    imageWithHiddenMsg.SetPixel(y, x, Color.FromArgb(R, G, B) );
                    break;
                } 
                G = getNewColorByMsgBit(pixelToChange.G, msgBits, bitCounter);
                bitCounter++; 

                if (bitCounter == msgBits.Length) 
                {
                    imageWithHiddenMsg.SetPixel(y, x, Color.FromArgb(R, G, B) );
                    break;
                } 
                B = getNewColorByMsgBit(pixelToChange.B, msgBits, bitCounter);
                bitCounter++;
   
                imageWithHiddenMsg.SetPixel(y, x, Color.FromArgb(R, G, B) );
                pixelsFilledCounter++;
            }

            return imageWithHiddenMsg;
        }


        public string GetMsgFromImage(Bitmap originalImage, Bitmap imageWithHiddenMsg, int hiddenMsgLength)
        {
            var hiddenMsgBits = new StringBuilder();
            int bitsInHiddenMsg = hiddenMsgLength * BITS_PER_CHAR;
            int pixelsNumber = bitsInHiddenMsg / RGB;
            pixelsNumber += ( bitsInHiddenMsg % RGB == 0 ? 0 : 1 );

            for (int pixelCounter = 0; pixelCounter < pixelsNumber; pixelCounter++)
            {
                int x = pixelCounter / originalImage.Width;
                int y = pixelCounter % originalImage.Width;

                var originalPixel = originalImage.GetPixel(y, x);
                var modifiedPixel = imageWithHiddenMsg.GetPixel(y, x);

                // append to msg bits last bits of rgb structure
                hiddenMsgBits.Append(modifiedPixel.R % 2);
                hiddenMsgBits.Append(modifiedPixel.G % 2);
                hiddenMsgBits.Append(modifiedPixel.B % 2);
            }

            return hiddenMsgBits.ToString().ToStringASCIIFromBinary();
        }
    }


}