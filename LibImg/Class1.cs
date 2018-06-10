using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ImageMagick;

namespace LibImg
{
    public static class Resizer
    {
        public static void PdfToImage()
        {
            /*
          * Ghostscript

             You only need to install Ghostscript if you want to convert EPS/PDF/PS files. Make sure you only install the version of 
             GhostScript with the same platform. If you use the 64-bit version of Magick.NET you should also install 
             the 64-bit version of Ghostscript. 
             You can use the 32-bit version together with the 64-version but you will get a better performance 
             if you keep the platforms the same. 
             Ghostscript can be downloaded here: http://www.ghostscript.com/download/gsdnld.html. 

             If you don't want to install Ghostscript on your machine 
             you can copy gsdll32.dll/gsdl64.dll and gswin32c.exe/gswin64c.exe to your server 
             and tell Magick.NET where the file is located with the code below.

             MagickNET.SetGhostscriptDirectory(@"C:\MyProgram\Ghostscript");
          */

            // Assembly Absolute Path 
            string assemblyFile = new Uri(Assembly.GetExecutingAssembly().CodeBase)
                .LocalPath;

            // Assembly folder name
            var path = Path.GetDirectoryName(assemblyFile);

            // Check for library
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Impossibile trovare la directory dei files ghostscript");
            }

            MagickNET.SetGhostscriptDirectory(Path.Combine(path, "ghostscript"));

            MagickReadSettings settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(300, 300);

            using (MagickImageCollection images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(@"C:\tmp\doc.pdf", settings);

                int page = 1;
                foreach (MagickImage image in images)
                {
                    // resize
                    image.Resize(150, 150);

                    // Write page to file that contains the page number
                    image.Write("Snakeware.Page" + page + ".png");

                    // Writing to a specific format works the same as for a single image
                    //image.Format = MagickFormat.Ptif;
                    //image.Write("Snakeware.Page" + page + ".tif");
                    page++;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Write(ms);

                        var bytes = ms.ToArray();
                        Debug.WriteLine((int)bytes.Length / 1024);
                    }
                }
            }

            //using (MagickImage image = new MagickImage("gatto.jpg"))
            //{
            //    image.Resize(50, 50);
            //    FileInfo fi = new FileInfo(@"gatto_resize2.jpg");
            //    image.Write(fi);
            //}

            //using (MagickImage image = new MagickImage(@"C:\tmp\doc.pdf"))
            //{
            //    image.Write("test.pdf");
            //    //FileInfo fi = new FileInfo(@"gatto_resize2.jpg");
            //    //image.Write(fi);
            //}
        }
    }
}
