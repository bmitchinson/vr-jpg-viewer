using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VrJpeg;
using MetadataExtractor.Formats.Xmp;

namespace vrjpeg_example
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("Starting!");
            string filename = @"Resources\IMG_20170723_142255.vr.jpg";
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filename);

            // TODO: Save to sub folder

            // Read raw XMP metadata.
            var xmpDirectories = VrJpegMetadataReader.ReadMetadata(filename);

            // Parse metadata into a dedicated class.
            GPanorama pano = new GPanorama(xmpDirectories.ToList());

            // Extract embedded image.
            // The primary image is the left eye, the right eye is embedded in the metadata.
            if (pano.ImageData != null)
            {
                string rightEyeFilename = string.Format("{0}_right.jpg", filenameWithoutExtension);
                string rightEyeFile = Path.Combine(Path.GetDirectoryName(filename), rightEyeFilename);

                File.WriteAllBytes(rightEyeFile, pano.ImageData);
            }

            // Extract embedded audio.
            if (pano.AudioData != null)
            {
                string audioFilename = string.Format("{0}_audio.mp4", filenameWithoutExtension);
                string audioFile = Path.Combine(Path.GetDirectoryName(filename), audioFilename);

                File.WriteAllBytes(audioFile, pano.AudioData);
            }

        }
    }
}
