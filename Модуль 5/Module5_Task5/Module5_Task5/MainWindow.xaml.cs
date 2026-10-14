using System.Windows;
using System.Windows.Controls;

namespace Module5_Task5
{
    public partial class MainWindow : Window
    {
        private double _lastNumber = 0;
        private string _selectedOperator = "";
        private bool _isNewEntry = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string btnValue = btn.Content.ToString();

            if (_isNewEntry || TxtDisplay.Text == "0")
            {
                TxtDisplay.Text = btnValue;
                _isNewEntry = false;
            }
            else
            {
                TxtDisplay.Text += btnValue;
            }
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            double result;
            if (double.TryParse(TxtDisplay.Text, out result))
            {
                _lastNumber = result;
                _selectedOperator = btn.Content.ToString();
                _isNewEntry = true;
            }
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            double currentNumber;
            if (double.TryParse(TxtDisplay.Text, out currentNumber))
            {
                double result = 0;

                switch (_selectedOperator)
                {
                    case "+":
                        result = _lastNumber + currentNumber;
                        break;
                    case "-":
                        result = _lastNumber - currentNumber;
                        break;
                    case "*":
                        result = _lastNumber * currentNumber;
                        break;
                    case "/":
                        if (currentNumber == 0)
                        {
                            MessageBox.Show("Деление на ноль запрещено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        result = _lastNumber / currentNumber;
                        break;
                    default:
                        return;
                }

                TxtDisplay.Text = result.ToString();
                _isNewEntry = true;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtDisplay.Text = "0";
            _lastNumber = 0;
            _selectedOperator = "";
            _isNewEntry = true;
        }
    }
}