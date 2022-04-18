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

        bool LeftMouseKeyPressed = false;
        bool RightMouseKeyPressed = false;
        bool GameStarted = false;
        public MainWindow()
        {
            InitializeComponent();
            InitTimer();
            CreateObjects();
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

            if (GameStarted)
            {
                bool isLost = this.ball.Move();
                if (isLost)
                {
                    this.ball.SetPos(this.platform);
                    GameStarted = false;
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
            double x = 200;
            double y = 200;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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
