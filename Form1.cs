using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrialGalleryApp1
{
    public partial class Form1 : Form
    {
        int xUp;
        int yUp;
        int xDown;
        int yDown;
        Rectangle rectCropArea;

        public Form1()
        {
            InitializeComponent(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void On_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = img;
            pictureBox1.Visible = true;
            pictureBox1.Show();
        }

        private void pictureBox1_mouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Invalidate();

            xDown = e.X;
            yDown = e.Y;

        }

        private void pictureBox1_mouseUp(object sender, MouseEventArgs e)
        {
            xUp = e.X;
            yUp = e.Y;
        //checks the points of mouseclicks up and down
            int top = Math.Min(yUp, yDown);
            int bottom = Math.Max(yUp, yDown);
            int left = Math.Min(xUp, xDown);
            int right = Math.Max(xUp, xDown);
        //create rec with top,left point and width, height
            Rectangle rec = new Rectangle(left, top, right-left, bottom-top);
            
            using (Pen pen = new Pen(Color.WhiteSmoke, 2))
            {

                pictureBox1.CreateGraphics().DrawRectangle(pen, rec);
            }

            left = left * pictureBox1.Image.Width / pictureBox1.Width;
            top = top * pictureBox1.Image.Height / pictureBox1.Height;
            
            right = right * pictureBox1.Image.Width / pictureBox1.Width;
            bottom = bottom * pictureBox1.Image.Height / pictureBox1.Height;

            rectCropArea = new Rectangle(left, top, (right - left), (bottom - top));
        }

        private void Crop_Click(object sender, EventArgs e)
        {
            try
            {

                //Prepare a new Bitmap on which the cropped image will be drawn
                Bitmap sourceBitmap = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Graphics g = pictureBox1.CreateGraphics();
                
                //Draw the image on the Graphics object with the new dimensions
                g.DrawImage(sourceBitmap, new Rectangle(0, 0, rectCropArea.Width, rectCropArea.Height), rectCropArea, GraphicsUnit.Pixel);
                pictureBox1.Invalidate();

                sourceBitmap.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
