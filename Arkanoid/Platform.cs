using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    internal class Platform
    {
        private Grid grid;
        private Rectangle rect;
        private double velocity;

        public Platform(Grid grid, double width=96, double height=16, double velocity=10)
        {
            this.grid = grid;
            // Create rectangle
            this.rect = new Rectangle();
            this.rect.Name = "Platform";
            this.rect.Width = width;
            this.rect.Height = height;
            double x = this.grid.Margin.Left + this.grid.Width / 2;
            double y = this.grid.Margin.Top + this.grid.Height - 6 * height;
            this.rect.Margin = new Thickness(x, y, 0, 0);
            this.rect.HorizontalAlignment = HorizontalAlignment.Left;
            this.rect.VerticalAlignment = VerticalAlignment.Top;
            this.rect.SetResourceReference(Rectangle.FillProperty, "Platform");
            // Add rectagle to the grid
            this.grid.Children.Add(rect);

            this.velocity = velocity;
        }

        public (double width, double height) GetSize()
        {
            return (this.rect.Width, this.rect.Height);
        }

        public (double x, double y) GetPos()
        {
            return (this.rect.Margin.Left, this.rect.Margin.Top);
        }

        public Rectangle GetRectangle()
        {
            return this.rect;
        }

        public double GetVelocity()
        {
            return velocity;
        }


        public void MoveLeft()
        {
            (double x, double y) = this.GetPos();
            if (x + velocity > 0)
            {
                this.rect.Margin = new Thickness(x - velocity, y, 0, 0);
            }
        }
        public void MoveRight()
        {
            (double x, double y) = this.GetPos();
            (double width, _) = this.GetSize();
            if (x + width + velocity < this.grid.Width)
            {
                this.rect.Margin = new Thickness(x + velocity, y, 0, 0);
            }
        }
    }
}
