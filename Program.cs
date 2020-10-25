using System;
using System.Drawing;
using Lab17.Extensions;
using Lab17.Steganography;

namespace Lab17
{
    class Program
    {
        private const string ORIGINAL_IMAGE_PATH = "original.bmp";
        private const string ADITIVE_STEGANOGRAPHY_IMAGE_PATH = "steganographyAditive.bmp";
        private const string LSB_STEGANOGRAPHY_IMAGE_PATH = "steganographyLSB.bmp";

        static void Main(string[] args)
        {
            var aditiveSteganography = new AditiveImageSteganography();
            var lsbSteganography     = new LsbImageSteganography();
            
            string text = @"Geralt of Rivia was a legendary witcher of the School of the Wolf active throughout the 13th century. He loved the sorceress Yennefer, considered the love of his life despite their tumultuous relationship, and became Ciri's adoptive father.";


            // IMAGE WITH HIDDEN MSG ADITIVE METHOD

            //var originalImage = new Bitmap(ADITIVE_ORIGINAL_IMAGE_PATH);
            //var imageWithHiddenMsg = aditiveSteganography.GenerateImageWithHiddenMsg(text, originalImage);
            //imageWithHiddenMsg.Save(ADITIVE_STEGANOGRAPHY_IMAGE_PATH);


            // TEXT FROM IMAGE ADITIVE METHOD

            //var originalImage = new Bitmap(ADITIVE_ORIGINAL_IMAGE_PATH);
            //var imageWithHiddenMsg = new Bitmap(ADITIVE_STEGANOGRAPHY_IMAGE_PATH);
            //Console.WriteLine(aditiveSteganography.GetMsgFromImage(originalImage, imageWithHiddenMsg));


            // IMAGE WITH HIDDEN MSG LSB METHOD

            var originalImage = new Bitmap(ORIGINAL_IMAGE_PATH);
            var imageWithHiddenMsg = lsbSteganography.GenerateImageWithHiddenMsg(text, originalImage);
            imageWithHiddenMsg.Save(LSB_STEGANOGRAPHY_IMAGE_PATH);


            // TEXT FROM IMAGE LSB METHOD

            originalImage = new Bitmap(ORIGINAL_IMAGE_PATH);
            imageWithHiddenMsg = new Bitmap(LSB_STEGANOGRAPHY_IMAGE_PATH);
            Console.WriteLine(lsbSteganography.GetMsgFromImage(originalImage, imageWithHiddenMsg, text.Length));


            Console.ReadLine();
        }
    }
}
