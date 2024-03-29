﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace squareChaser
{
    public partial class Form1 : Form
    {
        //global varibles
        Rectangle player1 = new Rectangle(200, 140, 20, 20);
        Rectangle player2 = new Rectangle(230, 20, 20, 20);
        Rectangle pointsquare = new Rectangle(195, 240, 10, 10);
        Rectangle speedboost = new Rectangle(95, 95, 10, 10);
        Rectangle slowdown = new Rectangle(29, 105, 10, 10);
        Rectangle scorereduction = new Rectangle(395, 190, 10, 10);
        Rectangle freeze = new Rectangle(25, 17, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 6;
        int player2Speed = 6;


        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;
        bool showRules = true;

        SolidBrush greenBrush = new SolidBrush(Color.LimeGreen);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);

        //sounds
        SoundPlayer player = new SoundPlayer(Properties.Resources.pointscore);
        SoundPlayer speedPlayer = new SoundPlayer(Properties.Resources.speedBoost);
        SoundPlayer endGame = new SoundPlayer(Properties.Resources.endGame);
        SoundPlayer freezeEffect = new SoundPlayer(Properties.Resources.freexeEffect);
        SoundPlayer pointLost = new SoundPlayer(Properties.Resources.pointLost);
        SoundPlayer slowdownSound = new SoundPlayer(Properties.Resources.slowdownEffect);

        //create a random generator object
        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
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
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, pointsquare);
            e.Graphics.FillRectangle(yellowBrush, speedboost);
            e.Graphics.FillRectangle(darkRedBrush, scorereduction);
            e.Graphics.FillRectangle(purpleBrush, slowdown);
            e.Graphics.FillRectangle(blueBrush, freeze);



        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //display rules 
            if (showRules == true)
            {
                ruleBox.Text = "Red = point loss\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "Purple = speed reduction\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "white = point gained\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "dark red = point lost\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "Yellow = speed boost\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "Blue = freeze\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                ruleBox.Text += "first to 5 wins!\n";
                ruleBox.Refresh();
                Thread.Sleep(1500);
                showRules = false;
            }

            //move player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;

            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }

            if (dDown == true && player1.X > 0)
            {
                player1.X += player1Speed;
            }

            //move player2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }

            if (rightArrowDown == true && player2.X > 0)
            {
                player2.X += player2Speed;
            }

            //player hits ball gets point
            if (player1.IntersectsWith(pointsquare))
            {
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";
                pointsquare.X = randGen.Next(5, 395);
                pointsquare.Y = randGen.Next(5, 395);
                player.Play();

            }
            else if (player2.IntersectsWith(pointsquare))
            {
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";
                pointsquare.X = randGen.Next(5, 395);
                pointsquare.Y = randGen.Next(5, 395);
                player.Play();

            }

            //player hits speedboost
            if (player1.IntersectsWith(speedboost))
            {
                player1Speed++;
                speedboost.X = randGen.Next(5, 395);
                speedboost.Y = randGen.Next(5, 395);
                speedPlayer.Play();
            }
            else if (player2.IntersectsWith(speedboost))
            {
                player2Speed++;
                speedboost.X = randGen.Next(5, 395);
                speedboost.Y = randGen.Next(5, 395);
                speedPlayer.Play();

            }

            //hits slowddown
            if (player1.IntersectsWith(slowdown))
            {
                player1Speed--;
                slowdown.X = randGen.Next(5, 395);
                slowdown.Y = randGen.Next(5, 395);
                slowdownSound.Play();
            }
            else if (player2.IntersectsWith(slowdown))
            {
                player2Speed--;
                slowdown.X = randGen.Next(5, 395);
                slowdown.Y = randGen.Next(5, 395);
                slowdownSound.Play();
            }

            //hits point reduction
            if (player1.IntersectsWith(scorereduction))
            {
                player1Score--;
                player1ScoreLabel.Text = $"{player1Score}";
                scorereduction.X = randGen.Next(5, 395);
                scorereduction.Y = randGen.Next(5, 395);
                pointLost.Play();
            }
            else if (player2.IntersectsWith(scorereduction))
            {
                player2Score--;
                player2ScoreLabel.Text = $"{player2Score}";
                scorereduction.X = randGen.Next(5, 395);
                scorereduction.Y = randGen.Next(5, 395);
                pointLost.Play();
            }
            //freeze
            if (player1.IntersectsWith (freeze))
            {
                player1Speed = 0;
                Thread.Sleep(3000);
                player1Speed = 6;
                freeze.X = randGen.Next(5, 395);
                freeze.Y = randGen.Next(5, 395);
                freezeEffect.Play();
            }
            else if (player2.IntersectsWith (freeze))
            {
                player2Speed = 0;
                Thread.Sleep(3000);
                player2Speed = 6;
                freeze.X = randGen.Next(5, 395);
                freeze.Y = randGen.Next(5, 395);
                freezeEffect.Play();
            }
            
            //who wins?
            if (player1Score == 5)
            {
                timer.Enabled = false;
                winlabel.Visible = true;
                winlabel.Text = "player 1 wins!";
                endGame.Play();
            }
            else if (player2Score == 5)
            {
                timer.Enabled = false;
                winlabel.Visible = true;
                winlabel.Text = " Player 2 wins!";
                endGame.Play();
            }
            Refresh();
        }
    }
}
