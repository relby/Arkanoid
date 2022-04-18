using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    internal class Brick
    {
        private Grid grid;
        private Rectangle rect;

        public Brick(Grid grid, double width=100, double height=25, double x=300, double y=300)
        {
            this.grid = grid;
            // Create rectangle
            this.rect = new Rectangle();
            this.rect.Name = "Brick";
            this.rect.Width = width;
            this.rect.Height = height;
            this.rect.Margin = new Thickness(x, y, 0, 0);
            this.rect.HorizontalAlignment = HorizontalAlignment.Left;
            this.rect.VerticalAlignment = VerticalAlignment.Top;
            this.rect.Fill = new SolidColorBrush(Colors.Magenta);
            this.rect.Stroke = new SolidColorBrush(Colors.Black);
            // Add rectagle to the grid
            this.grid.Children.Add(rect);
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

    }
}
