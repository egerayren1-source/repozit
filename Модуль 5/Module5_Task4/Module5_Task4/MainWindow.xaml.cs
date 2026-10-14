using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Module5_Task4
{
    public partial class MainWindow : Window
    {
        private double _scaleFactor = 1.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                    ImageViewer.Source = bitmap;
                    _scaleFactor = 1.0;
                    ApplyZoom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (ImageViewer.Source != null)
            {
                _scaleFactor += 0.2;
                ApplyZoom();
            }
        }

        private void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (ImageViewer.Source != null && _scaleFactor > 0.4)
            {
                _scaleFactor -= 0.2;
                ApplyZoom();
            }
        }

        private void ApplyZoom()
        {
            ImageViewer.LayoutTransform = new ScaleTransform(_scaleFactor, _scaleFactor);
        }
    }
}