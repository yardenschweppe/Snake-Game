using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        private int score = 0;
        private int speed = 5;
        private int difficulty = 0;
        private int highScore = 0;
        private bool gamePaused = false;
        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private IContainer components;
        private Label labelGameOver;
        private Label labelYouWon = new System.Windows.Forms.Label();
        private directionType direction = directionType.Down;
        private Label lblScore = new Label();
        private PictureBox pbCanvas;

        public Form1()
        {

            InitializeComponent();

            // Set initial snake and food positions
            newSettings();
        }
        private void newSettings()
        {
            Circle food = new Circle();
            Random rand = new Random();
            food.X = rand.Next(0, 29) * 10;
            food.Y = rand.Next(0, 29) * 10;

            List<Circle> snake = new List<Circle>();
            Circle head = new Circle();
            head.X = 100;
            head.Y = 100;
            snake.Add(head);

            labelGameOver.Visible = false;
            labelYouWon.Visible = false;
            
            
            speed = 10;
            score = 0;

            timer1.Interval = 1000 / speed;
            timer1.Tick += updateScreen;
            timer1.Start();
        }
        private void updateScreen(object sender, EventArgs e) { }
        
        public enum directionType
        {
            Up,
            Down,
            Left,
            Right
        };

        

        private void UpdateGameState()
        {
            // Move the snake in the designated direction
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                // Move the head of the snake
                if (i == 0)
                {
                    switch (difficulty)
                    {
                        case 0:
                            Snake[i].X++;
                            break;
                        case 1:
                            Snake[i].X += 2;
                            break;
                        case 2:
                            Snake[i].X += 3;
                            break;
                    }
                    if (Snake[0].X == Snake[i].X && Snake[0].Y == Snake[i].Y)
                    {
                        // collision with snake's body detected
                        // end the game
                        EndGame();
                    }
                    // Check for game over
                    if (Snake[0].X < 0 || Snake[0].Y < 0 || Snake[0].X >= panel1.Width || Snake[0].Y >= panel1.Height)
                    {
                        EndGame();
                    }

                    // Check for food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    // Move the body of the snake
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void Eat()
        {
            Circle food = new Circle();
          
            Snake.Add(food);

            score += 10;
            lblScore.Text = score.ToString();

            GenerateFood();
        }

        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }
        private void DrawGame()
        {
            // Clear the play area
            Graphics canvas = panel1.CreateGraphics();
            canvas.Clear(Color.White);

            // Draw the snake
            for (int i = 0; i < Snake.Count; i++)
            {
                Brush snakeColor;
                if (i == 0)
                {
                    snakeColor = Brushes.Black;
                }
                else
                {
                    snakeColor = Brushes.Green;
                }

                canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * 10, Snake[i].Y * 10, 10, 10));

                // Draw the food
                canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * 10, food.Y * 10, 10, 10));
            }
        }

        private void EndGame()
        {
            if (score > highScore)
            {
                highScore = score;
            }
            MessageBox.Show("Game Over! Your final score was: " + score + "\nThe highest score is: " + highScore, "Game");
        }
        private void InitializeComponent()
        {
            pbCanvas = new PictureBox();
            pbCanvas.BackColor = Color.Black;
            pbCanvas.Location = new Point(10, 10);
            pbCanvas.Size = new Size(300, 300);
            Controls.Add(pbCanvas);



            this.components = new System.ComponentModel.Container();
            this.labelGameOver = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();

            // ...
            // other component initialization

            lblScore.AutoSize = true;
            lblScore.Location = new Point(350, 20);
            lblScore.Text = "Score: 0";
            Controls.Add(lblScore);

            // 
            // labelGameOver
            // 
            this.labelGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelGameOver.ForeColor = System.Drawing.Color.Red;
            this.labelGameOver.Location = new System.Drawing.Point(0, 0);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(278, 244);
            this.labelGameOver.TabIndex = 0;
            this.labelGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGameOver.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(418, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 150);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(848, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelGameOver);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

            this.labelYouWon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelYouWon
            // 
            this.labelYouWon.AutoSize = true;
            this.labelYouWon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYouWon.Location = new System.Drawing.Point(100, 150);
            this.labelYouWon.Name = "labelYouWon";
            this.labelYouWon.Size = new System.Drawing.Size(112, 20);
            this.labelYouWon.TabIndex = 0;
            this.labelYouWon.Text = "You Have Won";
            this.labelYouWon.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelYouWon);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



    }

}