﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using System.IO;
using Windows.Foundation;

namespace dreaming.ControlHelp
{
    public class ImageHelp
    {
        internal static async Task LoadTileImageInternalAsync(string imagePath)
        {
            string tileName = "dreaming";
            uint MaxImageWidth =360;
            uint MaxImageHeight = 600;

            StorageFile origFile = await ApplicationData.Current.LocalFolder.GetFileAsync(imagePath);

            // open file for the new tile image file
            StorageFile tileFile = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync(tileName, CreationCollisionOption.GenerateUniqueName);
            using (IRandomAccessStream tileStream = await tileFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // get width and height from the original image
                IRandomAccessStreamWithContentType stream = await origFile.OpenReadAsync();
                ImageProperties properties = await origFile.Properties.GetImagePropertiesAsync();
                uint width = properties.Width;
                uint height = properties.Height;

                // get proper decoder for the input file - jpg/png/gif
                BitmapDecoder decoder = await GetProperDecoder(stream, origFile );
                if (decoder == null) return; // should not happen
                // get byte array of actual decoded image
                PixelDataProvider data = await decoder.GetPixelDataAsync();
                byte[] bytes = data.DetachPixelData();

                // create encoder for saving the tile image
                BitmapPropertySet propertySet = new BitmapPropertySet();
                // create class representing target jpeg quality - a bit obscure, but it works
                BitmapTypedValue qualityValue = new BitmapTypedValue(0.5, PropertyType.Single);
                propertySet.Add("ImageQuality", qualityValue);
                // create the target jpeg decoder
                BitmapEncoder be = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, tileStream, propertySet);
                be.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Straight, width, height, 96.0, 96.0, bytes);

                // crop the image, if it's too big
                if (width > MaxImageWidth || height > MaxImageHeight)
                {
                    BitmapBounds bounds = new BitmapBounds();
                    if (width > MaxImageWidth)
                    {
                        bounds.Width = MaxImageWidth;
                        bounds.X = (width - MaxImageWidth) / 2;
                    }
                    else bounds.Width = width;
                    if (height > MaxImageHeight)
                    {
                        bounds.Height = MaxImageHeight;
                        bounds.Y = (height - MaxImageHeight) / 2;
                    }
                    else bounds.Height = height;
                    be.BitmapTransform.Bounds = bounds;
                }
                // save the target jpg to the file
                await be.FlushAsync();
            }
           
        }


        public static async Task<BitmapDecoder> GetProperDecoder(IRandomAccessStreamWithContentType stream, StorageFile file)
        {
            string ext = Path.GetExtension(file.Path);
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    return await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, stream);
                case ".png":
                    return await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);
                case ".gif":
                    return await BitmapDecoder.CreateAsync(BitmapDecoder.GifDecoderId, stream);
            }
            return null;
        }

        public static async Task<string> GetImagePath(StorageFile file)
        {
            string ImagePath = null;
            StorageFile imageFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(file.Name, CreationCollisionOption.ReplaceExisting);

            var inStream = await file.OpenAsync(FileAccessMode.Read);

            var outStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite);
            outStream.Size = 0;
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(inStream);
            PixelDataProvider provider = await decoder.GetPixelDataAsync();
            byte[] data = provider.DetachPixelData();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, outStream);
            encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode,
                                               decoder.PixelWidth, decoder.PixelHeight,
                                               decoder.DpiX, decoder.DpiY, data
                );

            try
            {
                   await encoder.FlushAsync();

                    ImagePath = imageFile.Path;
              
            }
            catch (Exception err)
            {
                HelpMethods.Msg(err.Message.ToString());
            }
            finally
            {
                inStream.Dispose();
                outStream.Dispose();
            }
            return ImagePath;
        }
    }
}
