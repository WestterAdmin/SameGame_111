using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

namespace SameGame
{
    public partial class frmSameGame : Form
    {
        private const int duongKinh = 40;
        private int oldIndex = 0;
        private int countSameIndex = 0;
        List<PictureBox> lstSame = new List<PictureBox>();

        List<PictureBox> oldLstBall = new List<PictureBox>();

        private static readonly object syncLock = new object();
        private static readonly Random random = new Random();

        List<SameGameDto> _lstBall = new List<SameGameDto>();
        private string pathCurrentProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public frmSameGame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Width = 30 * duongKinh + 20;
            this.Height = 16* duongKinh;
            this.Text = "Game Same";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CountPointDrawingInLayout();
        }

        private void CountPointDrawingInLayout()
        {
            int width = this.Width;
            int height = this.Height;

            // tinh so Cot
            double countCol = Math.Floor((double)(width / duongKinh));
            double countRow = Math.Floor((double)(height / duongKinh));
            //4.5
            for (int i = 0; i < countCol; i++)
            {
                for (int j = 0; j < countRow; j++)
                {
                    DrawingWithPictureBox(i, j);
                }
            }
            var dt = _lstBall;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void DrawingWithPictureBox(int c, int d)
        {
            SameGameDto dto = new SameGameDto();
            PictureBox clonPicBox = new PictureBox();
            // + "\\Images\\";


            int IndexDrawx = (c * duongKinh);
            int IndexDrawy = (d * duongKinh);
            string img = GetRandomImage();
            clonPicBox.Location = new Point(IndexDrawx, IndexDrawy);
            clonPicBox.Width = duongKinh;
            clonPicBox.Height = duongKinh;
           // clonPicBox.Image = Image.FromFile(pathCurrentProject + "\\Images\\" + img);
            clonPicBox.ImageLocation = pathCurrentProject + "\\Images\\" + img;
            clonPicBox.BackColor = Color.Black;
            clonPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(clonPicBox);

            // remember ball to list ball

            dto.C = c;
            dto.D = d;
            dto.index_C = IndexDrawx;
            dto.index_D = IndexDrawy;
            dto.Ball = clonPicBox;
            dto.Image = img;

            if (!_lstBall.Contains(dto))
            {
                _lstBall.Add(dto);
            }


            clonPicBox.MouseClick += new MouseEventHandler(Pic_MouseClick);
            clonPicBox.MouseHover += new EventHandler(Pic_MouseHover);
            clonPicBox.MouseMove += new MouseEventHandler(this.Pic_MouseMove);
            clonPicBox.MouseLeave += new System.EventHandler(this.PIC_MouseLeave);

          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetRandomImage()
        {
            int index;
            lock (syncLock)
            {
               index = random.Next(0, 4);
            }
            if (index == oldIndex)
            {
                countSameIndex++;
            }
            else
            {
                countSameIndex = 0;
                oldIndex =index ;

            }
            if (countSameIndex > 4)
            { 
                GetRandomImage();
            }          
                
            string path = string.Format("ball_{0}.png", index +1);
            string file = string.Format("FILE_LOG_{0}",DateTime.Now.ToString("yyyyMMddHH"));

            if (!Directory.Exists(pathCurrentProject+"\\"+ file))
            {
                Directory.CreateDirectory(pathCurrentProject + "\\" + file);
            }


            using (StreamWriter w = File.AppendText(pathCurrentProject + string.Format("\\FILE_LOG_{0}\\log_Time.txt", DateTime.Now.ToString("yyyyMMddHH"))))
            {
                Log(index, path, w);
            }
            return path;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="logMessage"></param>
        /// <param name="txtWriter"></param>
        private void Log(int index, string logMessage, StreamWriter txtWriter)
        {
            try
            {
               // if(Path.co)
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine(" random index: {0}", index);
                txtWriter.WriteLine(" path image:{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }

        #region Comment
        //private void Tesst(out int a, ref int c)
        //{
        //    a = a+9;
        //    c = c + 9;
        //}

        //private void DrawinginLayout(int col, int row)
        //{
        //    Random rnd = new Random();
        //    int month = rnd.Next(0, 4);
        //    Color getColor = colors[month];

        //    Pen but = new Pen(getColor, 1);
        //    Graphics gr = CreateGraphics();

        //    //
        //    int IndexDrawx = (col * duongKinh);
        //    int IndexDrawy =( row * duongKinh);
        //    gr.DrawEllipse(but, IndexDrawx, IndexDrawy, duongKinh-5, duongKinh-5);
        //    gr.FillEllipse(new SolidBrush(getColor), IndexDrawx, IndexDrawy, duongKinh - 5, duongKinh - 5);
        //   // gr.DrawString("1", new Font(FontFamily.GenericSerif, 13, FontStyle.Bold, GraphicsUnit.Pixel, 8, false), Brushes.DarkBlue, 14, 14);

        //}

        //protected override void OnMouseMove(MouseMoveEventArgs e)
        //{
        //    bool needToRedraw = false;

        //    using (Graphics g = CreateGraphics())
        //    {
        //        foreach (GraphicalObject obj in Object)
        //        {
        //            if (!obj.IsVisible)
        //                continue;

        //            Rectangle rect = obj.GetBounds(e.Graphics);
        //            if (rect.Contains(e.Location))
        //            {
        //                if (!obj.IsFocused)
        //                {
        //                    obj.IsFocused = true;
        //                    needToRedraw = true;
        //                }
        //            }
        //            else
        //            {
        //                if (obj.IsFocused)
        //                {
        //                    obj.IsFocused = false;
        //                    needToRedraw = true;
        //                }
        //            }

        //            obj.Draw(e.Graphics);
        //        }
        //    }

        //    if (needToRedraw)
        //        Invalidate();
        //}
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_MouseClick(object sender, MouseEventArgs e)
        {
            if(sender is PictureBox)
            e.X.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_MouseHover(object sender, EventArgs e)
        {
            // Ko dùng
            //var getPoint = (PictureBox)sender;

            
            ////getPoint.Image = null;
            //e.ToString();
        }

        #region Process Search Ball
        private bool getBallBottom(PictureBox BallIndex)
        {
            bool Ischeck = false;
            if (lstSame.Count ==0)
            {
                lstSame.Add(BallIndex);
            }          
            var getBall = _lstBall.Where(a => a.index_C == BallIndex.Location.X && a.index_D == BallIndex.Location.Y).FirstOrDefault();
            if(getBall !=null)
            {
                var dt = _lstBall.Where(a => a.D == (getBall.D+1) && a.C == getBall.C).FirstOrDefault();
                if (dt != null)
                {
                    if (getBall.Image == dt.Image)
                    {
                        Ischeck = true;
                        lstSame.Add(dt.Ball);
                        dt.IsHover = true;
                        // truyền lại note để tiếp tục tìm kiêm ball ở dưới
                        getBallBottom(dt.Ball);
                    }
                }               
            }
            return Ischeck;
        }

        private void GetBallTop()
        {

        }

        private void GetBallRight()
        {

        }

        private void GetBallLeft()
        {

        }
        #endregion

        #region Move Ball
        private void MoveLeft()
        {

        }

        private void MoveDown()
        {

        }
        #endregion

        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox ball = sender as PictureBox;

            if (lstSame.Where(c => c.ImageLocation == ball.ImageLocation).Count() == 0)
            {
                oldLstBall = new List<PictureBox>(lstSame);
                lstSame = new List<PictureBox>();
               // lstSame.Add(ball); 
            }

            // find note top == using linq ==> 

            var mincol = _lstBall.Where(q => q.Ball.ImageLocation == ball.ImageLocation
                                          && q.index_C == ball.Location.X
                                          );
            var point = _lstBall.Where(q => q.Ball.ImageLocation == ball.ImageLocation
                                          && q.index_C == ball.Location.X
                                          && q.index_D == ball.Location.Y
                                          ).DefaultIfEmpty();

            // Find Ball is same with ball index
            bool addBall = getBallBottom(ball);

            oldLstBall.Select(c => { c.BackColor = Color.Black; return c; }).ToList();
           
            lstSame.Select(c => { c.BackColor = Color.White; return c; }).ToList();

            oldLstBall = new List<PictureBox>();
            
        }
        
        private void PIC_MouseLeave(object sender, EventArgs e)
        {
            PictureBox ball = sender as PictureBox;
            ball.BackColor = Color.Black;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

        }
        
    }
}
