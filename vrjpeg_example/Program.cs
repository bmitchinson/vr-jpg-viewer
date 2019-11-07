using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VrJpeg;
using MetadataExtractor.Formats.Xmp;
using Firebase.Database;
using Firebase.Database.Query;

namespace vrjpeg_example
{
    class Program
    {

        static void Main(string[] args)
        {

            breakCore();
            {
                // Console.WriteLine("Sleep for 5 second!");
                // var convertPending = await firebase.Child("convertPending");
                // if (converPending)
                // {
                //     breakCore();
                //     System.Environment.Exit(0);
                // }
                // Thread.Sleep(5000);
            }

            // query convertPending every 5
            // when convertPending 
            // * turn off convertPending
            // * turn on convertInProgress
            // * call convert method, puts files on desktop
            // * upload files from desktop to bucket
        }

        static void breakCore()
        {
            string uploadLink = "https://firebasestorage.googleapis.com/v0/b/cardboardcameraoculusviewer.appspot.com/o/upload.vr.jpg?alt=media";
            string coreFile = @"C:\Users\Ben\Desktop\breakCore\core_left.vr.jpg";
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(uploadLink, coreFile);
            }

            System.IO.File.WriteAllText(@"C:\Users\Ben\Desktop\breakCore\OuputLog.txt", "Loggin:\n");

            // Read raw XMP metadata.
            var xmpDirectories = VrJpegMetadataReader.ReadMetadata(coreFile);

            // Parse metadata into a dedicated class.
            GPanorama pano = new GPanorama(xmpDirectories.ToList());

            // Extract embedded image.
            // The primary image is the left eye, the right eye is embedded in the metadata.
            if (pano.ImageData != null)
            {
                string rightEyeFile = @"C:\Users\Ben\Desktop\breakCore\core_right.jpg";
                File.WriteAllBytes(rightEyeFile, pano.ImageData);
            }

            // if (pano.PanoInitialViewHeadingDegrees != null)
            // {
            //     log(pano.PanoInitialViewHeadingDegrees.ToString());
            // }

            // Extract embedded audio.
            if (pano.AudioData != null)
            {
                string audioFile = @"C:\Users\Ben\Desktop\breakCore\core_audio.mp4";
                File.WriteAllBytes(audioFile, pano.AudioData);
            }

        }

        static void log(string towrite)
        {
            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(@"C:\Users\Ben\Desktop\breakCore\OuputLog.txt", true))
            {
                file.WriteLine(towrite);
            }
        }
    }
}
