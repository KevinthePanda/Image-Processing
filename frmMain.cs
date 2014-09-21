using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Input;

//Name: Kevin Quicho
//Date: March 27 2013
//Assignment: Image Processing
//Purpose: Program changes image according to commands
namespace ImageProcessing
{
    public partial class frmMain : Form
    {

        private Color[,] original; //this is the original picture - never change the values stored in this array
        private Color[,] transformedPic;  //transformed picture that is displayed
        int div = 1;//div is a variable that is used to set the transform pic to half when it is used
       
        
        public frmMain()
        {
            this.KeyPreview = true;
            this.KeyPress +=
                new KeyPressEventHandler(Form1_KeyPress);

            InitializeComponent();
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'b')
            {
                mnuProcessBlur_Click(sender, e);
            }
            else if(e.KeyChar == 'r')
            {
                mnuProcessReset_Click(sender, e);
            }
            else if (e.KeyChar == 'i')
            {
                mnuProcessInvert_Click(sender, e);
            }
            else if (e.KeyChar == 'd')
            {
                mnuProcessDarken_Click(sender, e);
            }
            else if (e.KeyChar == 'w')
            {
                mnuProcessWhiten_Click(sender, e);
            }
            else if (e.KeyChar == 'x')
            {
                mnuProcessFlipX_Click(sender, e);
            }
            else if (e.KeyChar == 'y')
            {
                mnuProcessFlipY_Click(sender, e);
            }
            else if (e.KeyChar == 'h')
            {
                mnuProcessMirrorH_Click(sender, e);
            }
            else if (e.KeyChar == 'v')
            {
                mnuProcessMirrorV_Click(sender, e);
            }
            else if (e.KeyChar == '5')
            {
                mnuProcessScale50_Click(sender, e);
            }
            else if (e.KeyChar == '2')
            {
                mnuProcessScale200_Click(sender, e);
            }
            else if (e.KeyChar == 'o')
            {
                rotateToolStripMenuItem_Click(sender, e);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //this method draws the transformed picture
            //what ever is stored in transformedPic array will
            //be displayed on the form

            base.OnPaint(e);

            Graphics g = e.Graphics;

            //only draw if picture is transformed
            if (transformedPic != null)
            {
                //get height and width of the transfrormedPic array
                int height = transformedPic.GetUpperBound(0)+1;
                int width = transformedPic.GetUpperBound(1) + 1;

                //create a new Bitmap to be dispalyed on the form
                Bitmap newBmp = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        //loop through each element transformedPic and set the 
                        //colour of each pixel in the bitmalp

                            newBmp.SetPixel(j, i, transformedPic[i, j]);
          
                        
                    }

                }
                //call DrawImage to draw the bitmap
                g.DrawImage(newBmp, 0, 20, width, height);
            }
            
        }


        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            div = 1;
            
     
            //this method reads in a picture file and stores it in an array

            //try catch should handle any errors for invalid picture files
            try
            {

                //open the file dialog to select a picture file

                OpenFileDialog fd = new OpenFileDialog();

                //create a bitmap to store the file in
                Bitmap bmp;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    //store the selected file into a bitmap
                    bmp = new Bitmap(fd.FileName);

                    //create the arrays that store the colours for the image
                    //the size of the arrays is based on the height and width of the bitmap
                    //initially both the original and transformedPic arrays will be identical
                    original = new Color[bmp.Height, bmp.Width];
                    transformedPic = new Color[bmp.Height, bmp.Width];
               
                    //load each color into a color array
                    for (int i = 0; i < bmp.Height; i++)//each row
                    {
                        for (int j = 0; j < bmp.Width; j++)//each column
                        {
                            //assign the colour in the bitmap to the array
                            original[i, j] = bmp.GetPixel(j, i);
                            transformedPic[i, j] = original[i, j];
                        }                        
                    }
                               
                    //this will cause the form to be redrawn and OnPaint() will be called
                    this.Refresh();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Picture File. \n" + ex.Message);
            }
            
        }

        private void mnuProcessDarken_Click(object sender, EventArgs e)
        {

            //code to Darken
            int red, green, blue;

            if (transformedPic != null)
            {
                int height = transformedPic.GetLength(0);
                int width = transformedPic.GetLength(1);

                //loop through each element in the transformPic

                for (int r = 0; r < transformedPic.GetLength(0)/div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1)/div; c++)
                    {
                        //darkens by 10
                        red = transformedPic[r, c].R - 10;
                        blue = transformedPic[r, c].B - 10;
                        green = transformedPic[r, c].G - 10;
                        //doesnt allow the colours to go below 0
                        if (red < 0)
                        {
                            red = 0;
                        }
                        if (blue < 0)
                        {
                            blue = 0;
                        }
                        if (green < 0)
                        {
                            green = 0;
                        }
                        //sets the pixel
                        transformedPic[r, c] = Color.FromArgb(red, green, blue);

                    }

                }

            }

            this.Refresh();
        }

        private void mnuProcessInvert_Click(object sender, EventArgs e)
        {
            //code to Invert

            int red, green, blue;

            if (transformedPic != null)
            {
                int height = transformedPic.GetLength(0);
                int width = transformedPic.GetLength(1); 

                //loop through each element in the transformPic

                for (int r = 0; r < transformedPic.GetLength(0)/div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1)/div; c++)
                    {
                        //subtract existing colour value from 255
                        //to get the inverse colour
                        red = 255 - transformedPic[r, c].R;
                        blue = 255 - transformedPic[r, c].B;
                        green = 255 - transformedPic[r, c].G;
                        transformedPic[r, c] = Color.FromArgb(red, green, blue);

                    }

                }
            }

            this.Refresh();
        }

        private void mnuProcessWhiten_Click(object sender, EventArgs e)
        {
            //code to Whiten

            int red, green, blue;

            if (transformedPic != null)
            {
                int height = transformedPic.GetLength(0);
                int width = transformedPic.GetLength(1);

                //loop through each element in the transformPic

                for (int r = 0; r < transformedPic.GetLength(0)/div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1)/div; c++)
                    {

                        red = transformedPic[r, c].R + 10;
                        blue = transformedPic[r, c].B + 10;
                        green = transformedPic[r, c].G + 10;
                        //doesnt let colour go over 255
                        if (red > 255)
                        {
                            red = 255;
                        }
                        if (blue > 255)
                        {
                            blue = 255;
                        }
                        if (green > 255)
                        {
                            green = 255;
                        }

                        transformedPic[r, c] = Color.FromArgb(red, green, blue);

                    }

                }

                this.Refresh();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void mnuProcessReset_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //resets the divided used when scaling
                div = 1;
                transformedPic = new Color[original.GetLength(0), original.GetLength(1)];
                for (int i = 0; i < original.GetLength(0); i++)//each row
                {
                    for (int j = 0; j < original.GetLength(1); j++)//each column
                    {
                        //assign the colour in the bitmap to the array
                        transformedPic[i, j] = original[i, j];
                    }
                }
            }

            this.Refresh();
        
        }

        private void mnuProcessMirrorH_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //copy is the length to the transform by colum is multipled by 2 since it is doubled
                Color[,] copy = new Color[(transformedPic.GetLength(0)) / div, (transformedPic.GetLength(1) / div) * 2];

                for (int r = 0; r < transformedPic.GetLength(0) / div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        //set them equal
                        copy[r, c] = transformedPic[r, c];
                    }
                }

                transformedPic = new Color[copy.GetLength(0), copy.GetLength(1)];

                for (int r = 0; r < transformedPic.GetLength(0); r++)
                {
                    //used to place a pixel at one end of the picture to the other
                    int a = (transformedPic.GetLength(1) - 1);


                    for (int c = 0; c < transformedPic.GetLength(1); c++)
                    {


                        //only half since only have will have the original since the rest is flipped
                        if (c < (transformedPic.GetLength(1) / 2))
                        {
                            transformedPic[r, c] = copy[r, c];

                        }
                        else
                        {
                            transformedPic[r, c] = copy[r, a];
                        }
                        a--;

                    }

                }
                //sets divided to 1
                div = 1;
            }
            this.Refresh();
        }

        private void mnuProcessScale50_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //div is a variable that is used to set the transform pic to half when it is used
                div = div * 2;
                Color[,] copy = new Color[(transformedPic.GetLength(0)), (transformedPic.GetLength(1))];

                int i = 0;
                for (int r = 0; r < transformedPic.GetLength(0); r++)
                {

                    int j = 0;

                    for (int c = 0; c < transformedPic.GetLength(1); c++)
                    {
                        if (r < transformedPic.GetLength(0) / 2 && c < transformedPic.GetLength(1) / 2)
                        {
                            //makes it equal to every other pixel
                            copy[r, c] = transformedPic[i, j];


                        }
                        transformedPic[r, c] = copy[r, c];

                        j = j + 2;



                    }
                    
                    i = i + 2;
                }

            
            this.Refresh();
        }
  
        }

        private void mnuProcessFlipX_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                Color[,] copyPic = new Color[transformedPic.GetLength(0), transformedPic.GetLength(1)];

                for (int r = 0; r < transformedPic.GetLength(0) / div; r++)
                {
                    int a = (transformedPic.GetLength(1) - 1) / div;
                    //only half since it is being flipped on half its columns
                    for (int c = 0; c < (transformedPic.GetLength(1) / 2) / div; c++)
                    {

                        copyPic[r, c] = transformedPic[r, c];
                        copyPic[r, a] = transformedPic[r, a];
                        transformedPic[r, a] = copyPic[r, c];
                        transformedPic[r, c] = copyPic[r, a];
                        a = a - 1;
                    }
                }
                this.Refresh();
            }
        }

        private void mnuProcessFlipY_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                Color[,] copyPic = new Color[transformedPic.GetLength(0), transformedPic.GetLength(1)];

                int num = 1;
                //being flipped on half its rows
                for (int r = 0; r < (transformedPic.GetLength(0) / 2) / div; r++)
                {
                    int a = (transformedPic.GetLength(0)) / div;

                    a = a - num;
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {


                        copyPic[r, c] = transformedPic[r, c];
                        copyPic[a, c] = transformedPic[a, c];
                        transformedPic[r, c] = copyPic[a, c];
                        transformedPic[a, c] = copyPic[r, c];

                    }
                    num++;
                }
                this.Refresh();
            }
        }

        private void mnuProcessScale200_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //doubled
                Color[,] copy = new Color[(transformedPic.GetLength(0) * 2), transformedPic.GetLength(1) * 2];

                for (int r = 0; r < transformedPic.GetLength(0); r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1); c++)
                    {
                        copy[r, c] = transformedPic[r, c];
                    }
                }

                transformedPic = new Color[copy.GetLength(0), copy.GetLength(1)];
                int i = 0;
                //r+2 and c+2 since a square of pixels are the same
                for (int r = 0; r < transformedPic.GetLength(0); r = r + 2)
                {
                    int j = 0;
                    for (int c = 0; c < transformedPic.GetLength(1); c = c + 2)
                    {

                        transformedPic[r, c] = copy[i, j];
                        transformedPic[r + 1, c] = copy[i, j];
                        transformedPic[r, c + 1] = copy[i, j];
                        transformedPic[r + 1, c + 1] = copy[i, j];
                        j++;
                    }
                    i++;
                }

                this.Refresh();
            }
        }

        private void mnuProcessMirrorV_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //*2 rows
                Color[,] copy = new Color[(2 * transformedPic.GetLength(0)) / div, (transformedPic.GetLength(1) / div)];

                for (int r = 0; r < transformedPic.GetLength(0) / div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        copy[r, c] = transformedPic[r, c];
                    }
                }

                transformedPic = new Color[copy.GetLength(0), copy.GetLength(1)];
                int a = (transformedPic.GetLength(0) - 1) / 2;
                bool x = false;
                for (int r = 0; r < transformedPic.GetLength(0); r++)
                {
                    //once it reaches the half way point the mirrored immage beings printting
                    if (x == true)
                    {
                        a--;
                    }

                    for (int c = 0; c < transformedPic.GetLength(1); c++)
                    {
                        x = false;
                        if (r < (transformedPic.GetLength(0) / 2))
                        {
                            transformedPic[r, c] = copy[r, c];

                        }
                        else
                        {
                            transformedPic[r, c] = copy[a, c];
                            x = true;
                        }


                    }


                }
                div = 1;
                this.Refresh();
            }
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                Color[,] copyPic = new Color[transformedPic.GetLength(0), transformedPic.GetLength(1)];


                for (int r = 0; r < (transformedPic.GetLength(0)) / div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        copyPic[r, c] = transformedPic[r, c];


                    }
                }
                //length and width switch
                transformedPic = new Color[copyPic.GetLength(1), copyPic.GetLength(0)];
                int a = (transformedPic.GetLength(1) - 1) / div;
                for (int r = 0; r < (transformedPic.GetLength(0)) / div; r++)
                {


                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        //column is now the row, row is now the length - column
                        transformedPic[r, c] = copyPic[a - c, r];


                    }
                }
                this.Refresh();
            }
        }

        private void mnuProcessBlur_Click(object sender, EventArgs e)
        {
            if (transformedPic != null)
            {
                //variables for the colour 
                int red, green, blue;
                Color[,] copyPic = new Color[transformedPic.GetLength(0), transformedPic.GetLength(1)];

                for (int r = 0; r < (transformedPic.GetLength(0)) / div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        copyPic[r, c] = transformedPic[r, c];

                    }
                }

                for (int r = 0; r < transformedPic.GetLength(0) / div; r++)
                {
                    for (int c = 0; c < transformedPic.GetLength(1) / div; c++)
                    {
                        //left
                        if (c == 0 && r != 0 && r != transformedPic.GetLength(0) - 1)
                        {          //m               //tm                   //tr                       //mr                 dm                   dr
                            red = (copyPic[r, c].R + copyPic[r - 1, c].R + copyPic[r - 1, c + 1].R + copyPic[r, c + 1].R + copyPic[r + 1, c].R + copyPic[r + 1, c + 1].R) / 6;
                            green = (copyPic[r, c].G + copyPic[r - 1, c].G + copyPic[r - 1, c + 1].G
                                + copyPic[r, c + 1].G + copyPic[r + 1, c].G + copyPic[r + 1, c + 1].G) / 6;
                            blue = (copyPic[r, c].B + copyPic[r - 1, c].B + copyPic[r - 1, c + 1].B
                                + copyPic[r, c + 1].B + copyPic[r + 1, c].B + copyPic[r + 1, c + 1].B) / 6;

                            transformedPic[r, c] = Color.FromArgb(red, green, blue);
                        }
                        //right
                        if (c == transformedPic.GetLength(1) - 1 && r != 0 && r != transformedPic.GetLength(0) - 1)
                        {
                            red = (copyPic[r, c].R + copyPic[r - 1, c].R + copyPic[r - 1, c - 1].R
                                + copyPic[r, c - 1].R + copyPic[r + 1, c].R + copyPic[r + 1, c - 1].R) / 6;
                            green = (copyPic[r, c].G + copyPic[r - 1, c].G + copyPic[r - 1, c - 1].G
                                + copyPic[r, c - 1].G + copyPic[r + 1, c].G + copyPic[r + 1, c - 1].G) / 6;
                            blue = (copyPic[r, c].B + copyPic[r - 1, c].B + copyPic[r - 1, c - 1].B
                                + copyPic[r, c - 1].B + copyPic[r + 1, c].B + copyPic[r + 1, c - 1].B) / 6;




                            transformedPic[r, c] = Color.FromArgb(red, green, blue);
                        }
                        //top
                        if (r == 0 && c != 0 && c != transformedPic.GetLength(1) - 1)
                        {
                            red = (copyPic[r, c].R + copyPic[r, c - 1].R + copyPic[r, c + 1].R
                                + copyPic[r + 1, c + 1].R + copyPic[r + 1, c].R + copyPic[r + 1, c - 1].R) / 6;
                            green = (copyPic[r, c].G + copyPic[r, c - 1].G + copyPic[r, c + 1].G
                                + copyPic[r + 1, c + 1].G + copyPic[r + 1, c].G + copyPic[r + 1, c - 1].G) / 6;
                            blue = (copyPic[r, c].B + copyPic[r, c - 1].B + copyPic[r, c + 1].B
                                + copyPic[r + 1, c + 1].B + copyPic[r + 1, c].B + copyPic[r + 1, c - 1].B) / 6;

                            transformedPic[r, c] = Color.FromArgb(red, green, blue);

                        }
                        //bottom
                        if (r == transformedPic.GetLength(0) - 1 && c != 0 && c != transformedPic.GetLength(1) - 1)
                        {
                            red = (copyPic[r, c].R + copyPic[r, c - 1].R + copyPic[r, c + 1].R
                                 + copyPic[r - 1, c + 1].R + copyPic[r - 1, c].R + copyPic[r - 1, c - 1].R) / 6;
                            green = (copyPic[r, c].G + copyPic[r, c - 1].G + copyPic[r, c + 1].G
                                 + copyPic[r - 1, c + 1].G + copyPic[r - 1, c].G + copyPic[r - 1, c - 1].G) / 6;
                            blue = (copyPic[r, c].B + copyPic[r, c - 1].B + copyPic[r, c + 1].B
                                 + copyPic[r - 1, c + 1].B + copyPic[r - 1, c].B + copyPic[r - 1, c - 1].B) / 6;

                            transformedPic[r, c] = Color.FromArgb(red, green, blue);
                        }
                        //top left corner
                        if (r == 0 && c == 0)
                        {          //mm              mr                   br                       bm
                            red = (copyPic[r, c].R + copyPic[r, c + 1].R + copyPic[r + 1, c + 1].R + copyPic[r + 1, c].R) / 4;
                            green = (copyPic[r, c].G + copyPic[r, c + 1].G + copyPic[r + 1, c + 1].G + copyPic[r + 1, c].G) / 4;
                            blue = (copyPic[r, c].B + copyPic[r, c + 1].B + copyPic[r + 1, c + 1].B + copyPic[r + 1, c].B) / 4;
                        }
                        //top right corner 
                        if (r == 0 && c == transformedPic.GetLength(1) - 1)
                        {          //mm              ml                   bl                       bm
                            red = (copyPic[r, c].R + copyPic[r, c - 1].R + copyPic[r + 1, c - 1].R + copyPic[r + 1, c].R) / 4;
                            green = (copyPic[r, c].G + copyPic[r, c - 1].G + copyPic[r + 1, c - 1].G + copyPic[r + 1, c].G) / 4;
                            blue = (copyPic[r, c].B + copyPic[r, c - 1].B + copyPic[r + 1, c - 1].B + copyPic[r + 1, c].B) / 4;
                        }
                        //blc
                        if (r == transformedPic.GetLength(0) - 1 && c == 0)
                        {          //mm              mr                   tr                       tm
                            red = (copyPic[r, c].R + copyPic[r, c + 1].R + copyPic[r - 1, c + 1].R + copyPic[r - 1, c].R) / 4;
                            green = (copyPic[r, c].G + copyPic[r, c + 1].G + copyPic[r - 1, c + 1].G + copyPic[r - 1, c].G) / 4;
                            blue = (copyPic[r, c].B + copyPic[r, c + 1].B + copyPic[r - 1, c + 1].B + copyPic[r - 1, c].B) / 4;
                        }
                        //brc
                        if (r == transformedPic.GetLength(0) - 1 && c == transformedPic.GetLength(1) - 1)
                        {          //mm              ml                  tl                       tm
                            red = (copyPic[r, c].R + copyPic[r, c - 1].R + copyPic[r - 1, c - 1].R + copyPic[r - 1, c].R) / 4;
                            green = (copyPic[r, c].G + copyPic[r, c - 1].G + copyPic[r - 1, c - 1].G + copyPic[r - 1, c].G) / 4;
                            blue = (copyPic[r, c].B + copyPic[r, c - 1].B + copyPic[r - 1, c - 1].B + copyPic[r - 1, c].B) / 4;
                        }
                        //middle
                        if (r != 0 && c != 0 && r != transformedPic.GetLength(0) - 1 && c != transformedPic.GetLength(1) - 1)
                        {//         mm                 ml                   mr                    tr                        tm                  tl                         br                       bm                      bl
                            red = (copyPic[r, c].R + copyPic[r, c - 1].R + copyPic[r, c + 1].R + copyPic[r - 1, c + 1].R + copyPic[r - 1, c].R + copyPic[r - 1, c - 1].R + copyPic[r + 1, c + 1].R + copyPic[r + 1, c].R + copyPic[r + 1, c - 1].R) / 9;


                            green = (copyPic[r, c].G + copyPic[r, c - 1].G + copyPic[r, c + 1].G
                                 + copyPic[r - 1, c + 1].G + copyPic[r - 1, c].G + copyPic[r - 1, c - 1].G
                                 + copyPic[r + 1, c + 1].G + copyPic[r + 1, c].G + copyPic[r + 1, c - 1].G) / 9;
                            blue = (copyPic[r, c].B + copyPic[r, c - 1].B + copyPic[r, c + 1].B
                                 + copyPic[r - 1, c + 1].B + copyPic[r - 1, c].B + copyPic[r - 1, c - 1].B
                                 + copyPic[r + 1, c + 1].B + copyPic[r + 1, c].B + copyPic[r + 1, c - 1].B) / 9;

                            transformedPic[r, c] = Color.FromArgb(red, green, blue);
                        }

                    }
                }
            }

            this.Refresh();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Quit?", "Image Process", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
