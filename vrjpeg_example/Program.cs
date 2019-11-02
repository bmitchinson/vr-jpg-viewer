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
            string filename = @"Resources\IMG_20170723_142255.vr.jpg";
            string filenameWithoutExtension = filename.Substring(0, filename.Length - 7);
            Debug.WriteLine("{0}", filenameWithoutExtension);
    
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
                string rightEyeFile = Path.Combine(Path.GetDirectoryName(filename), filenameWithoutExtension, rightEyeFilename);

                File.WriteAllBytes(rightEyeFile, pano.ImageData);
            }

            // Extract embedded audio.
            if (pano.AudioData != null)
            {
                string audioFilename = string.Format("{0}_audio.mp4", filenameWithoutExtension);
                string audioFile = Path.Combine(Path.GetDirectoryName(filename), filenameWithoutExtension, audioFilename);

                File.WriteAllBytes(audioFile, pano.AudioData);
            }

        }

        private static void DumpProperties(IEnumerable<XmpDirectory> directories)
        {
            foreach (var dir in directories)
            {
                foreach (var prop in dir.XmpMeta.Properties)
                {
                if (prop.Path != null && prop.Path.EndsWith(":Data"))
                    Debug.WriteLine("{0} - {1}", prop.Namespace, prop.Path);
                else
                    Debug.WriteLine("{0} - {1}:{2}", prop.Namespace, prop.Path, prop.Value);
                }
            }
        }
    }
}
