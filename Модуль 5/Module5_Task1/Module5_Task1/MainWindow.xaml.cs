using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Module5_Task1
{
    public partial class MainWindow : Window
    {
        private Point _startPoint;
        private bool _isDrawing = false;
        private Shape _currentShape = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private Brush GetSelectedColor()
        {
            string colorName = (CmbColor.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Классический switch для полной совместимости с C# 7.3
            switch (colorName)
            {
                case "Красный":
                    return Brushes.Red;
                case "Синий":
                    return Brushes.Blue;
                case "Зеленый":
                    return Brushes.Green;
                default:
                    return Brushes.Black;
            }
        }

        private double GetSelectedThickness()
        {
            string thicknessText = (CmbThickness.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (double.TryParse(thicknessText, out double thickness))
            {
                return thickness;
            }
            return 2;
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            _startPoint = e.GetPosition(DrawingCanvas);

            string selectedShape = (CmbShape.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Линия";
            Brush strokeColor = GetSelectedColor();
            double thickness = GetSelectedThickness();

            switch (selectedShape)
            {
                case "Линия":
                    _currentShape = new Line
                    {
                        Stroke = strokeColor,
                        StrokeThickness = thickness,
                        X1 = _startPoint.X,
                        Y1 = _startPoint.Y,
                        X2 = _startPoint.X,
                        Y2 = _startPoint.Y
                    };
                    break;
                case "Круг":
                    _currentShape = new Ellipse
                    {
                        Stroke = strokeColor,
                        StrokeThickness = thickness
                    };
                    Canvas.SetLeft(_currentShape, _startPoint.X);
                    Canvas.SetTop(_currentShape, _startPoint.Y);
                    break;
                case "Квадрат":
                    _currentShape = new Rectangle
                    {
                        Stroke = strokeColor,
                        StrokeThickness = thickness
                    };
                    Canvas.SetLeft(_currentShape, _startPoint.X);
                    Canvas.SetTop(_currentShape, _startPoint.Y);
                    break;
            }

            if (_currentShape != null)
            {
                DrawingCanvas.Children.Add(_currentShape);
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing || _currentShape == null) return;

            Point currentPoint = e.GetPosition(DrawingCanvas);

            if (_currentShape is Line line)
            {
                line.X2 = currentPoint.X;
                line.Y2 = currentPoint.Y;
            }
            else if (_currentShape is Shape shape)
            {
                double x = Math.Min(currentPoint.X, _startPoint.X);
                double y = Math.Min(currentPoint.Y, _startPoint.Y);
                double w = Math.Abs(currentPoint.X - _startPoint.X);
                double h = Math.Abs(currentPoint.Y - _startPoint.Y);

                Canvas.SetLeft(shape, x);
                Canvas.SetTop(shape, y);
                shape.Width = w;
                shape.Height = h;
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
            _currentShape = null;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }
    }
}