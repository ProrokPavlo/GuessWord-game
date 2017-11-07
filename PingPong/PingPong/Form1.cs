using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        public static bool START = true;
        bool up, left;
        public string KEY;

        Sprite[] sprites = new Sprite[10];

        public Form1()
        {
            InitializeComponent();
        }
        public void loadSprite(string file,int num, int x, int y)
        {
            sprites[num] = new Sprite(file, x, y);
        }

        public void loadSprite(string file, int num, int x, int y,int w,int h)
        {
            sprites[num] = new Sprite(file, x, y, w, h);
        }
        Random g = new Random();
        public void SetupGame()
        {
            int br = g.Next(0, 5);
            if (br == 0)
            {
                left = true;
                up = true;
            }
            else if (br == 1)
            {
                left = false;
                up = true;
            }
            else if (br == 2)
            {
                left = true;
                up = false;
            }
            else
            { 
                left = false;
                up = false;
            }
            loadSprite("paddle.png", 1, 5, 210);
            loadSprite("paddle.png", 2, 780, 210);
            loadSprite("ball.png", 3, 390, g.Next(20,480),22,22);
 
        }

        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.Font = new Font("Snap ITC", 42);

        }

        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.Font = new Font("Snap ITC", 36);

        }

        private void lblStart_MouseEnter(object sender, EventArgs e)
        {
            lblStart.Font = new Font("Snap ITC", 42);

        }

        private void lblStart_MouseLeave(object sender, EventArgs e)
        {
            lblStart.Font= new Font("Snap ITC", 36);
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            tmrRefresh.Start();
            START = true;
            lblExit.Hide();
            lblStart.Hide();
            lblPl1.Show();
            lblPl2.Show();
            SetupGame();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            Graphics g= e.Graphics;
            foreach (Sprite s in sprites)
            {
                if(s!= null)
                    g.DrawImage(s.CurrentSprite, new Rectangle(s._x, s._y, s._width, s._height));
            }

        }
        public bool KeyPressed(Keys k)
        {
            if (KEY == k.ToString())
                return true;
            else return false;
        }

        public void MoveSprite(int num, int x, int y)
        {
            sprites[num]._x = x;
            sprites[num]._y = y;
        }
        public int SpriteY(int num)
        {
            return sprites[num]._y;
        }
        public int SpriteX(int num)
        {
            return sprites[num]._x;
        }
        int pl1 = 0, pl2 = 0;
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;//for faster performance
            this.Refresh();


            if (KeyPressed(Keys.Escape))
            {
                Application.Exit();
            }

            //MoveSprite(1, sensing.Mouse.X+7,sensing.Mouse.Y-40);
            if (KeyPressed(Keys.Up) && SpriteY(1) > 10)
                MoveSprite(1, SpriteX(1), SpriteY(1) - 6);
            if (KeyPressed(Keys.Down) && SpriteY(1) < 410)
                MoveSprite(1, SpriteX(1), SpriteY(1) + 6);
            if (KeyPressed(Keys.Left) && SpriteY(1) > 9 && SpriteX(1) > -20)
                MoveSprite(1, SpriteX(1) - 7, SpriteY(1));
            if (KeyPressed(Keys.Right) && SpriteY(1) < 415 && SpriteX(1) < 390)
                MoveSprite(1, SpriteX(1) + 4, SpriteY(1));

            if (left)
                MoveSprite(3, SpriteX(3) - 6, SpriteY(3));
            if (!left)
                MoveSprite(3, SpriteX(3) + 6, SpriteY(3));
            if (up)
                MoveSprite(3, SpriteX(3), SpriteY(3)-5);
            if (!up)
                MoveSprite(3, SpriteX(3), SpriteY(3)+6);

            if (up && SpriteY(3) < 11)
                up = false;
            if (!up && SpriteY(3) > 475)
                up = true;

            if (sprites[3].SpriteCollision(sprites[1]) && left)
                left = false;
            if (sprites[3].SpriteCollision(sprites[2]) && left==false)
                left = true;


            if (SpriteY(3) < SpriteY(2)+sprites[2]._height/2 && SpriteY(2) > 10 && left == false)
                MoveSprite(2, SpriteX(2)-4, SpriteY(2) - 8);
            if (SpriteY(3) > SpriteY(2)+sprites[2]._height/2 && SpriteY(2) < 410 && left == false)
                MoveSprite(2, SpriteX(2)+3, SpriteY(2) + 8);

            if (SpriteX(3) < -30)
            {
                pl2 += 1;
                lblPl2.Text = pl2.ToString();
                SetupGame();
                Thread.Sleep(1000);
            }
            if (SpriteX(3) > 830)
            {
                pl1 += 1;
                lblPl1.Text = pl1.ToString();
                SetupGame();
                Thread.Sleep(1000);
            }
            if (pl1 >= 5 && pl2 != 5)
            {
                START = false;
                lblEnd.Text = "  You won!\n R = restart\n ESC = quit";
                lblEnd.Show();
                if (KeyPressed(Keys.Escape))
                {
                    Application.Exit();
                }
                if (KeyPressed(Keys.R))
                {
                    SetupGame();
                    pl1 = 0;
                    pl2 = 0;
                    lblPl1.Text = 0.ToString();
                    lblPl2.Text = 0.ToString();
                    lblEnd.Hide();
                    START = true;
                    
                }
            }
            if (pl1 != 5 && pl2 >= 5)
            {
                START = false;
                lblEnd.Text = "  You lost!\n R = restart\n ESC = quit";
                lblEnd.Show();
                if(KeyPressed(Keys.Escape))
                {
                    Application.Exit();
                }
                if (KeyPressed(Keys.R))
                {
                    SetupGame();
                    pl1 = 0;
                    pl2 = 0;
                    lblPl1.Text = 0.ToString();
                    lblPl2.Text = 0.ToString();
                    lblEnd.Hide();
                    START = true;
                }
            }
            if (START == false && (pl1<5 && pl2<5))
            {
                START = true;
                tmrRefresh.Start();
                lblEnd.Hide();
                START = true;
                SetupGame();
            }


        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            this.DoubleBuffered = true;
            KEY = "";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.DoubleBuffered = true;
            KEY = e.KeyCode.ToString();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblPl1.Hide();
            lblPl2.Hide();
        }

    }
}
