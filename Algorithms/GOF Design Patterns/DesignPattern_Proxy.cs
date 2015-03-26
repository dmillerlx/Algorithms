using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Proxy
    {
        //Virtual Proxy Example
        //
        //Allows for lazy loading of data



        public interface Image
        {
            void showImage();
        }


        public class ImageProxy : Image
        {
            string imageFilePath;

            Image proxifiedImage;

            public ImageProxy(string fileName)
            {
                imageFilePath = fileName;
            }

            public void showImage()
            {

                proxifiedImage = new HighResolutionImage(imageFilePath);

                proxifiedImage.showImage();

            }            
        }

        public class HighResolutionImage: Image
        {
            string imageData;

            public HighResolutionImage(string filename)
            {
                loadImage(filename);
            }

            void loadImage(string filename)
            {
                //Long operation
                System.Threading.Thread.Sleep(1000);

                imageData = "Image Data for ("+filename+")";

                Console.WriteLine("  HighRes - Load Image: " + imageData);
            }


            public void showImage()
            {
                //Show image
                Console.WriteLine("  Showing Image: " + imageData);
            }
        }

        public void Run()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Proxy Pattern");
            Console.WriteLine("------------------------------");

            Console.WriteLine();
            Console.WriteLine("Loading data using proxy for lazy loading...");

            // assuming that the user selects a folder that has 3 images	
            //create the 3 images 	
            Image highResolutionImage1 = new ImageProxy("sample/veryHighResPhoto1.jpeg");
            Image highResolutionImage2 = new ImageProxy("sample/veryHighResPhoto2.jpeg");
            Image highResolutionImage3 = new ImageProxy("sample/veryHighResPhoto3.jpeg");

            Console.WriteLine("Now showing image #1");

            // assume that the user clicks on Image one item in a list
            // this would cause the program to call showImage() for that image only
            // note that in this case only image one was loaded into memory
            highResolutionImage1.showImage();

            Console.WriteLine();
            Console.WriteLine("Loading data without proxy...");

            // consider using the high resolution image object directly
            Image highResolutionImageNoProxy1 = new HighResolutionImage("sample/veryHighResPhoto1.jpeg");
            Image highResolutionImageNoProxy2 = new HighResolutionImage("sample/veryHighResPhoto2.jpeg");
            Image highResolutionImageBoProxy3 = new HighResolutionImage("sample/veryHighResPhoto3.jpeg");


            Console.WriteLine("Now showing image #2");

            // assume that the user selects image two item from images list
            highResolutionImageNoProxy2.showImage();

            // note that in this case all images have been loaded into memory 
            // and not all have been actually displayed
            // this is a waste of memory resources

        }


    }
}
