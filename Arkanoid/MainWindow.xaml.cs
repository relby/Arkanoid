using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Arkanoid.components;

namespace Arkanoid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        Ball ball;
        List<Brick> bricks;
        Platform platform;

        int lifes;

        bool LeftMouseKeyPressed = false;
        bool RightMouseKeyPressed = false;
        bool GameStarted = false;

        private void CreateEndGameDialog(string text)
        {
            EndGameDialog endGameDialog = new EndGameDialog();
            endGameDialog.Text = text;
            bool res = endGameDialog.ShowDialog() == true;
            if (res)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
            {
                MainMenu mm = new MainMenu();
                mm.Show();
            }
            timer.Stop();
            Close();
        }

        public MainWindow()
        {
            lifes = 3;

            InitializeComponent();
            CreateObjects();
            InitTimer();
        }
        public void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            if (LeftMouseKeyPressed) this.platform.MoveLeft();
            else if (RightMouseKeyPressed) this.platform.MoveRight();
            lifes_label.Content = $"Lifes: {lifes}";
            int brick_count = 0;
            foreach (Rectangle rect in grid.Children)
            {
                if (rect.Name == "Brick") brick_count++;
            }
            if (brick_count == 0)
            {
                CreateEndGameDialog("You Won!");
                return;
            }
            if (GameStarted)
            {
                bool isLost = this.ball.Move();
                if (isLost)
                {
                    this.ball.SetPos(this.platform);
                    GameStarted = false;
                    lifes--;
                    if (lifes < 0)
                    {
                        CreateEndGameDialog("You Lost!");
                        return;
                    }
                }
            }
            else
            {
                this.ball.SetPos(this.platform);
            }
        }
        public void CreateObjects()
        {
            this.platform = new Platform(grid);
            this.ball = new Ball(grid, this.platform);

            this.bricks = new List<Brick>();
            double width = 100;
            double height = 50;
            double x = 100;
            double y = 100;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.bricks.Add(new Brick(grid, width, height, x + width * i, y + height * j));
                }
            }
        }

        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.Left) LeftMouseKeyPressed = true;
            if (e.Key == Key.D || e.Key == Key.Right) RightMouseKeyPressed = true;
            if (e.Key == Key.Space) GameStarted = true;
        }

        private void window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.Left) LeftMouseKeyPressed = false;
            if (e.Key == Key.D || e.Key == Key.Right) RightMouseKeyPressed = false;
        }
    }
}
