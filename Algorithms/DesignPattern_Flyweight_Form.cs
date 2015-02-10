using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Algorithms
{
    // Purpose:
    //  The Fly Weight pattern is intended to reduce the number of objects by reusing objects
    //  In this example, we are writing 1,000,000 rectangles to the screen
    //  Each rectangle requires a brush object
    //
    //  In the first test, we create a new brush object for each rectangle
    //  In the FlyWeight test, we get a previously created brush from the factory
    //    The factory stores the brushes by color, so we only create a new brush
    //    if we are using a new color
    //
    


    public partial class DesignPattern_Flyweight_Form : Form
    {

        //Declare local vars
        public Color[] colors = null;
        int numberOfRect = 1000000;
        int maxWidth;
        int maxHeight;


        //Create an array of colors to write
        public void InitColors()
        {         
            colors = new Color[colorCount];
            for (int x = 0; x < colorCount; x++)
            {
                colors[x] = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            }
        }


        public DesignPattern_Flyweight_Form()
        {
            InitializeComponent();
            InitColors();

            //Init vars
            maxWidth = this.Width;
            maxHeight = this.Height;
        }

        //Init number of colors
        int colorCount = 100;

        //Init random number generator
        Random r = new Random((int)System.DateTime.Now.Ticks);


        //Draw - no fly weight pattern
        //This function will draw the rectangles and create a new brush object every time
        private void Draw()
        {
            Graphics g = this.CreateGraphics();
            Rectangle rect = new Rectangle();
            for (int x = 0; x < numberOfRect; x++)
            {
                System.Drawing.Brush b = new System.Drawing.SolidBrush(colors[r.Next(colorCount - 1)]);
                rect.X = r.Next(maxWidth);
                rect.Y = r.Next(maxHeight);
                rect.Width = r.Next(maxWidth);
                rect.Height = r.Next(maxHeight);
                g.FillRectangle(b, rect);
            }

        }

        //Draw - with Fly Weight pattern
        //The brush object will be obtained from the FlyWeight brush factory
        //That factory will only create a new brush if it has not been created before
        private void Draw_Flyweight()
        {
            Graphics g = this.CreateGraphics();
            Rectangle rect = new Rectangle();
            DesignPattern_Flyweight fly = new DesignPattern_Flyweight();
            for (int x = 0; x < numberOfRect; x++)
            {
                System.Drawing.Brush b = fly.getBrush(colors[r.Next(colorCount - 1)]);
                rect.X = r.Next(maxWidth);
                rect.Y = r.Next(maxHeight);
                rect.Width = r.Next(maxWidth);
                rect.Height = r.Next(maxHeight);
                g.FillRectangle(b, rect);
            }
        }




        private void Run(bool useFlyWeight)
        {
            System.DateTime start = System.DateTime.Now;

            if (useFlyWeight)
                Draw_Flyweight();
            else
                Draw();
            System.DateTime end = System.DateTime.Now;

            System.TimeSpan lapse = end - start;

            MessageBox.Show("Total time: " + lapse.TotalMilliseconds);


        }


        private void button_run_no_flyweight(object sender, EventArgs e)
        {
            Run(false);
        }

        private void button_run_with_flyweight(object sender, EventArgs e)
        {
            Run(true);
        }
    }
}
