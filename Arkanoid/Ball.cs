using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Arkanoid
{
    internal class Ball
    {
        private Grid grid;
        private Rectangle rect;
        private double velocityX, velocityY;

        public Ball(Grid grid, Platform platform, double width=24, double velocity=7.5)
        {
            this.grid = grid;
            // Create rectangle
            this.rect = new Rectangle();
            this.rect.Name = "Ball";
            this.rect.Width = width;
            this.rect.Height = width;
            (double x, double y) = platform.GetPos();
            (double w, _) = platform.GetSize();
            this.rect.Margin = new Thickness(x + w / 2 - width / 2, y - width, 0, 0);
            this.rect.HorizontalAlignment = HorizontalAlignment.Left;
            this.rect.VerticalAlignment = VerticalAlignment.Top;
            this.rect.SetResourceReference(Rectangle.FillProperty, "Ball");
            // Add rectangle to the grid
            this.grid.Children.Add(rect);

            this.velocityX = velocity;
            this.velocityY = velocity;
        }

        public (double width, double height) GetSize()
        {
            return (this.rect.Width, this.rect.Height);
        }

        public (double x, double y) GetPos()
        {
            return (this.rect.Margin.Left, this.rect.Margin.Top);
        }

        public void SetPos(Platform platform)
        {
            (double x, double y) = platform.GetPos();
            (double w, _) = platform.GetSize();
            (double width, _) = this.GetSize();
            this.rect.Margin = new Thickness(x + w / 2 - width / 2, y - width, 0, 0);
        }

        public Rectangle GetRectangle()
        {
            return this.rect;
        }

        public (double velocityX, double velocityY) GetVelocity()
        {
            return (velocityX, velocityY);
        }

        public bool CheckIntersection()
        {
            Rect border = new Rect(this.GetPos().x, this.GetPos().y, this.GetSize().width, this.GetSize().height);
            Rect gridBorder = new Rect(0, 0, this.grid.Width, this.grid.Height);
            // Window border collision
            if (!gridBorder.Contains(border))
            {
                if (!gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.TopRight) && !gridBorder.Contains(border.BottomLeft) && !gridBorder.Contains(border.BottomRight))
                {
                    return true;
                }
                else if (!gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.TopRight) && !gridBorder.Contains(border.BottomLeft) ||
                    !gridBorder.Contains(border.TopRight) && !gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.BottomRight) ||
                    !gridBorder.Contains(border.BottomLeft) && !gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.BottomRight) ||
                    !gridBorder.Contains(border.BottomRight) && !gridBorder.Contains(border.TopRight) && !gridBorder.Contains(border.BottomLeft))
                {
                    this.velocityX = -this.velocityX;
                    this.velocityY = -this.velocityY;
                }
                else if (!gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.TopRight)/* || !gridBorder.Contains(border.BottomLeft) && !gridBorder.Contains(border.BottomRight)*/)
                {
                    this.velocityY = -this.velocityY;
                }
                else if (!gridBorder.Contains(border.TopLeft) && !gridBorder.Contains(border.BottomLeft) || !gridBorder.Contains(border.TopRight) && !gridBorder.Contains(border.BottomRight))
                {
                    this.velocityX = -this.velocityX;
                }
                return false;
            }

            foreach (Rectangle elem in this.grid.Children)
            {
                if (elem.Name != "Brick" && elem.Name != "Platform") continue;

                Rect elemBorder = new Rect(elem.Margin.Left, elem.Margin.Top, elem.Width, elem.Height);
                if (border.IntersectsWith(elemBorder))
                {
                    if (elemBorder.Contains(border.TopLeft) && elemBorder.Contains(border.TopRight) || elemBorder.Contains(border.BottomLeft) && elemBorder.Contains(border.BottomRight))
                    {
                        this.velocityY = -this.velocityY;
                    }
                    else if (elemBorder.Contains(border.TopLeft) && elemBorder.Contains(border.BottomLeft) || elemBorder.Contains(border.TopRight) && elemBorder.Contains(border.BottomRight))
                    {
                        this.velocityX = -this.velocityX;
                    }
                    else if (elemBorder.Contains(border.TopLeft))
                    {
                        Point point;
                        foreach (Point p in new Point[] { elemBorder.TopLeft, elemBorder.TopRight, elemBorder.BottomLeft, elemBorder.BottomRight })
                        {
                            if (border.Contains(p))
                            {
                                point = p;
                                break;
                            }
                        }
                        Rect r = new Rect(point, border.TopLeft);
                        if (r.Width == r.Height)
                        {
                            this.velocityX = -this.velocityX;
                            this.velocityY = -this.velocityY;
                        }
                        else if (r.Width > r.Height)
                        {
                            this.velocityY = -this.velocityY;
                        }
                        else
                        {
                            this.velocityX = -this.velocityX;
                        }
                    }
                    else if (elemBorder.Contains(border.TopRight))
                    {
                        Point point;
                        foreach (Point p in new Point[] { elemBorder.TopLeft, elemBorder.TopRight, elemBorder.BottomLeft, elemBorder.BottomRight })
                        {
                            if (border.Contains(p))
                            {
                                point = p;
                                break;
                            }
                        }
                        Rect r = new Rect(point, border.TopRight);
                        if (r.Width == r.Height)
                        {
                            this.velocityX = -this.velocityX;
                            this.velocityY = -this.velocityY;
                        }
                        else if (r.Width > r.Height)
                        {
                            this.velocityY = -this.velocityY;
                        }
                        else
                        {
                            this.velocityX = -this.velocityX;
                        }
                    }
                    else if (elemBorder.Contains(border.BottomLeft))
                    {
                        Point point;
                        foreach (Point p in new Point[] { elemBorder.TopLeft, elemBorder.TopRight, elemBorder.BottomLeft, elemBorder.BottomRight })
                        {
                            if (border.Contains(p))
                            {
                                point = p;
                                break;
                            }
                        }
                        Rect r = new Rect(point, border.BottomLeft);
                        if (r.Width == r.Height)
                        {
                            this.velocityX = -this.velocityX;
                            this.velocityY = -this.velocityY;
                        }
                        else if (r.Width > r.Height)
                        {
                            this.velocityY = -this.velocityY;
                        }
                        else
                        {
                            this.velocityX = -this.velocityX;
                        }
                    }
                    else if (elemBorder.Contains(border.BottomRight))
                    {
                        Point point;
                        foreach (Point p in new Point[] { elemBorder.TopLeft, elemBorder.TopRight, elemBorder.BottomLeft, elemBorder.BottomRight })
                        {
                            if (border.Contains(p))
                            {
                                point = p;
                                break;
                            }
                        }
                        Rect r = new Rect(point, border.BottomRight);
                        if (r.Width == r.Height)
                        {
                            this.velocityX = -this.velocityX;
                            this.velocityY = -this.velocityY;
                        }
                        else if (r.Width > r.Height)
                        {
                            this.velocityY = -this.velocityY;
                        }
                        else
                        {
                            this.velocityX = -this.velocityX;
                        }
                    }
                    if (elem.Name == "Brick") {
                        DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                        myDoubleAnimation.From = 1.0;
                        myDoubleAnimation.To = 0.0;
                        myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(360));
                        elem.BeginAnimation(Rectangle.OpacityProperty, myDoubleAnimation);
                        elem.Name = "DeletedBrick";
                    }
                    return false;
                }
            }
            return false;
        }

        public bool Move()
        {
            bool isLost = this.CheckIntersection();
            (double x, double y) = this.GetPos();
            this.rect.Margin = new Thickness(x + this.velocityX, y + this.velocityY, 0, 0);
            return isLost;
        }
    }
}
