using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace AirHockey
{
    public partial class Form1 : Form
    {
        SoundPlayer hitPlayer = new SoundPlayer(Properties.Resources.hit);
        SoundPlayer goalPlayer = new SoundPlayer(Properties.Resources.horn);
        SoundPlayer cheerPlayer = new SoundPlayer(Properties.Resources.clap);

        Random ranGen = new Random();

        Rectangle player1center = new Rectangle(10, 235, 30, 30);
        Rectangle player1front = new Rectangle(40, 235, 1, 30);
        Rectangle player1back = new Rectangle(10, 235, 1, 30);
        Rectangle player1top = new Rectangle(10, 235, 30, 1);
        Rectangle player1bottom = new Rectangle(10, 264, 30, 1);

        Rectangle player2center = new Rectangle(765, 235, 30, 30);
        Rectangle player2front = new Rectangle(765, 235, 1, 30);
        Rectangle player2back = new Rectangle(795, 235, 1, 30);
        Rectangle player2top = new Rectangle(765, 235, 30, 1);
        Rectangle player2bottom = new Rectangle(765, 264, 30, 1);

        Rectangle ball = new Rectangle(100, 245, 10, 10);
        Rectangle net1 = new Rectangle(0, 100, 5, 300);
        Rectangle net2 = new Rectangle(795, 100, 5, 300);

        Font scoreFont = new Font("MS Gothic", 16, FontStyle.Bold);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 8;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        int balltopspeed = 10;

        int ballhit = 100;
        int ballStill = 0;

        int ballprevX;
        int ballprevY;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool leftArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Pen whitePen = new Pen(Color.White, 3);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(blueBrush, player1center);
            e.Graphics.FillEllipse(redBrush, player2center);
            //e.Graphics.DrawRectangle(whitePen, player1front);
            //e.Graphics.DrawRectangle(whitePen, player2front);
            //e.Graphics.DrawRectangle(whitePen, player1back);
            //e.Graphics.DrawRectangle(whitePen, player2back);
            //e.Graphics.DrawRectangle(whitePen, player1top);
            //e.Graphics.DrawRectangle(whitePen, player2top);
            //e.Graphics.DrawRectangle(whitePen, player1bottom);
            //e.Graphics.DrawRectangle(whitePen, player2bottom);
            e.Graphics.FillRectangle(whiteBrush, ball);

            e.Graphics.FillRectangle(whiteBrush, net1);
            e.Graphics.FillRectangle(whiteBrush, net2);

            e.Graphics.DrawString($"{player1Score}", scoreFont, whiteBrush, player1center.X + 6, player1center.Y + 5);
            e.Graphics.DrawString($"{player2Score}", scoreFont, whiteBrush, player2center.X + 6, player2center.Y + 5);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (ball.X > -5 && ball.X < 805)
            {
                ball.X += ballXSpeed;
            }
            if (ball.Y > -5 && ball.Y < 505)
            {
                ball.Y += ballYSpeed;
            }


            if (wDown == true && player1center.Y > 10)
            {
                player1center.Y -= playerSpeed;
                player1front.Y -= playerSpeed;
                player1back.Y -= playerSpeed;
                player1top.Y -= playerSpeed;
                player1bottom.Y -= playerSpeed;
            }
            if (aDown == true && player1center.X > 5)
            {
                player1center.X -= playerSpeed;
                player1front.X -= playerSpeed;
                player1back.X -= playerSpeed;
                player1top.X -= playerSpeed;
                player1bottom.X -= playerSpeed;
            }
            if (sDown == true && player1center.Y < 460)
            {
                player1center.Y += playerSpeed;
                player1front.Y += playerSpeed;
                player1back.Y += playerSpeed;
                player1top.Y += playerSpeed;
                player1bottom.Y += playerSpeed;
            }
            if (dDown == true && player1center.X < 364)
            {
                player1center.X += playerSpeed;
                player1front.X += playerSpeed;
                player1back.X += playerSpeed;
                player1top.X += playerSpeed;
                player1bottom.X += playerSpeed;
            }

            if (upArrowDown == true && player2center.Y > 10)
            {
                player2center.Y -= playerSpeed;
                player2front.Y -= playerSpeed;
                player2back.Y -= playerSpeed;
                player2top.Y -= playerSpeed;
                player2bottom.Y -= playerSpeed;
            }
            if (leftArrowDown == true && player2center.X > 405)
            {
                player2center.X -= playerSpeed;
                player2front.X -= playerSpeed;
                player2back.X -= playerSpeed;
                player2top.X -= playerSpeed;
                player2bottom.X -= playerSpeed;
            }
            if (downArrowDown == true && player2center.Y < 460)
            {
                player2center.Y += playerSpeed;
                player2front.Y += playerSpeed;
                player2back.Y += playerSpeed;
                player2top.Y += playerSpeed;
                player2bottom.Y += playerSpeed;
            }
            if (rightArrowDown == true && player2center.X < 765)
            {
                player2center.X += playerSpeed;
                player2front.X += playerSpeed;
                player2back.X += playerSpeed;
                player2top.X += playerSpeed;
                player2bottom.X += playerSpeed;
            }

            if (ball.Y < 10 || ball.Y > 480)
            {
                ballYSpeed *= -1;
                hitPlayer.Play();
            }
            if (ball.X < 5 || ball.X > 785)
            {
                ballXSpeed *= -1;
                hitPlayer.Play();
            }

            if (ballXSpeed == 0 && ballYSpeed == 0)
            {
                if (player1front.IntersectsWith(ball))
                {
                    ballXSpeed = balltopspeed;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ball.X = player1front.X + ball.Width;
                    hitPlayer.Play();
                }
                if (player1back.IntersectsWith(ball))
                {
                    ballXSpeed = -balltopspeed;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ball.X = player1back.X - ball.Width;
                    hitPlayer.Play();
                }
                if (player1top.IntersectsWith(ball))
                {
                    ballYSpeed = -balltopspeed;
                    ball.Y = player1top.Y - ball.Width;
                    hitPlayer.Play();
                }
                if (player1bottom.IntersectsWith(ball))
                {
                    ballYSpeed = balltopspeed;
                    ball.Y = player1bottom.Y + ball.Width;
                    hitPlayer.Play();
                }

                if (player2front.IntersectsWith(ball))
                {
                    ballXSpeed = -balltopspeed;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ball.X = player2front.X - ball.Width;
                    hitPlayer.Play();
                }
                if (player2back.IntersectsWith(ball))
                {
                    ballXSpeed = balltopspeed;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ball.X = player2back.X + ball.Width;
                    hitPlayer.Play();
                }
                if (player2top.IntersectsWith(ball))
                {
                    ballYSpeed = -balltopspeed;
                    ball.Y = player2top.Y - ball.Width;
                    hitPlayer.Play();
                }
                if (player2bottom.IntersectsWith(ball))
                {
                    ballYSpeed = balltopspeed;
                    ball.Y = player2bottom.Y + ball.Width;
                    hitPlayer.Play();
                }
            }
            else if (ballXSpeed != 0 || ballYSpeed != 0)
            {
                if (player1front.IntersectsWith(ball))
                {
                    ballXSpeed = (balltopspeed + ranGen.Next(1, 5));
                    ball.X = player1front.X + ball.Width;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player1back.IntersectsWith(ball))
                {
                    ballXSpeed = -(balltopspeed + ranGen.Next(1, 5));
                    ball.X = player1back.X - ball.Width;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player1top.IntersectsWith(ball))
                {
                    ballYSpeed = -(balltopspeed + ranGen.Next(1, 5));
                    ball.Y = player1top.Y - ball.Width;
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player1bottom.IntersectsWith(ball))
                {
                    ballYSpeed = (balltopspeed + ranGen.Next(1, 5));
                    ball.Y = player1bottom.Y + ball.Width;
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }

                if (player2front.IntersectsWith(ball))
                {
                    ballXSpeed = -(balltopspeed + ranGen.Next(1, 5));
                    ball.X = player2front.X - ball.Width;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player2back.IntersectsWith(ball))
                {
                    ballXSpeed = (balltopspeed + ranGen.Next(1, 5));
                    ball.X = player2back.X + ball.Width;
                    ballYSpeed = ballYSpeed + ranGen.Next(-10, 11);
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player2top.IntersectsWith(ball))
                {
                    ballYSpeed = -(balltopspeed + ranGen.Next(1, 5));
                    ball.Y = player2top.Y - ball.Width;
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
                if (player2bottom.IntersectsWith(ball))
                {
                    ballYSpeed = (balltopspeed + ranGen.Next(1, 5));
                    ball.Y = player2bottom.Y + ball.Width;
                    ballhit = 100;
                    ballStill = 0;
                    hitPlayer.Play();
                }
            }

            ballhit -= 7;

            if (ballhit <= 0)
            {
                if (ballXSpeed > 0)
                {
                    ballXSpeed--;
                }
                if (ballXSpeed < 0)
                {
                    ballXSpeed++;
                }
                if (ballYSpeed > 0)
                {
                    ballYSpeed--;
                }
                if (ballYSpeed < 0)
                {
                    ballYSpeed++;
                }
                ballhit = 100;
            }

            if (ball.IntersectsWith(net1))
            {
                goalPlayer.Play();
                

                ballXSpeed = 0;
                ballYSpeed = 0;

                ball.X = 700;
                ball.Y = 245;

                player1center.Y = 235;
                player1front.Y = 235;
                player1back.Y = 235;
                player1top.Y = 235;
                player1bottom.Y = 264;

                player1center.X = 10;
                player1front.X = 40;
                player1back.X = 10;
                player1top.X = 10;
                player1bottom.X = 10;

                player2center.Y = 235;
                player2front.Y = 235;
                player2back.Y = 235;
                player2top.Y = 235;
                player2bottom.Y = 264;

                player2center.X = 765;
                player2front.X = 765;
                player2back.X = 795;
                player2top.X = 765;
                player2bottom.X = 765;

                player2Score++;

                if (player2Score == 5)
                {
                    cheerPlayer.Play();
                    centerLabel.Visible = false;
                    winLabel.Visible = true;
                    winLabel.Text = "Player 2 Wins!";
                    gameTimer.Enabled = false;
                    resetButton.Visible = true;
                }
            }

            if (ball.IntersectsWith(net2))
            {
                goalPlayer.Play();

                ballXSpeed = 0;
                ballYSpeed = 0;

                ball.X = 100;
                ball.Y = 245;

                player1center.Y = 235;
                player1front.Y = 235;
                player1back.Y = 235;
                player1top.Y = 235;
                player1bottom.Y = 264;

                player1center.X = 10;
                player1front.X = 40;
                player1back.X = 10;
                player1top.X = 10;
                player1bottom.X = 10;

                player2center.Y = 235;
                player2front.Y = 235;
                player2back.Y = 235;
                player2top.Y = 235;
                player2bottom.Y = 264;

                player2center.X = 765;
                player2front.X = 765;
                player2back.X = 795;
                player2top.X = 765;
                player2bottom.X = 765;

                player1Score++;
                if (player1Score == 5)
                {
                    cheerPlayer.Play();
                    centerLabel.Visible = false;
                    winLabel.Visible = true;
                    winLabel.Text = "Player 1 Wins!";
                    gameTimer.Enabled = false;
                    resetButton.Visible = true;
                }
            }

            if (ball.X == ballprevX && ball.Y == ballprevY)
            {
                ballStill++;
            }

            if (ballStill > 250 || ball.X > 805 || ball.Y > 505 || ball.Y < -5)
            {
                resetButton.Visible = true;
                noButton.Visible = true;
                ballStill = 0;
                this.Focus();
                gameTimer.Enabled = false;
            }

            ballprevX = ball.X;
            ballprevY = ball.Y;

            this.Focus();
            Refresh();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ballXSpeed = 0;
            ballYSpeed = 0;

            ball.X = 100;
            ball.Y = 245;

            player1center.Y = 235;
            player1front.Y = 235;
            player1back.Y = 235;
            player1top.Y = 235;
            player1bottom.Y = 264;

            player1center.X = 10;
            player1front.X = 40;
            player1back.X = 10;
            player1top.X = 10;
            player1bottom.X = 10;

            player2center.Y = 235;
            player2front.Y = 235;
            player2back.Y = 235;
            player2top.Y = 235;
            player2bottom.Y = 264;

            player2center.X = 765;
            player2front.X = 765;
            player2back.X = 795;
            player2top.X = 765;
            player2bottom.X = 765;


            resetButton.Visible = false;
            noButton.Visible = false;
            centerLabel.Visible = true;
            winLabel.Visible = false;
            if (player1Score == 5 || player2Score == 5)
            {
                player1Score = 0;
                player2Score = 0;
            }
            if (gameTimer.Enabled == false)
            {
                gameTimer.Enabled = true;
            }
            this.Focus();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = true;
            resetButton.Visible = false;
            noButton.Visible = false;
            ballStill = 0;
            this.Focus();
        }
    }
}
