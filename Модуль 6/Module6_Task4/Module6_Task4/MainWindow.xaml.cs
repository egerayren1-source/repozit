using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Module6_Task4
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string connStr = "Server=localhost;Database=finance_db;Uid=Zahar;Pwd=3036;charset=utf8;";
        public ObservableCollection<Transaction> Transactions { get; set; }
        private int selectedId = -1;

        public MainWindow()
        {
            InitializeComponent();
            Transactions = new ObservableCollection<Transaction>();
            DgTransactions.ItemsSource = Transactions;
            DpDate.SelectedDate = DateTime.Now;
            LoadData();
        }

        // Загрузка списка транзакций из базы данных
        private void LoadData()
        {
            Transactions.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, type, category, amount, transaction_date, comment FROM transactions";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transactions.Add(new Transaction
                            {
                                Id = reader.GetInt32("id"),
                                Type = reader.GetString("type"),
                                Category = reader.GetString("category"),
                                Amount = reader.GetDecimal("amount"),
                                TransactionDate = reader.GetDateTime("transaction_date"),
                                Comment = reader.IsDBNull(reader.GetOrdinal("comment")) ? "" : reader.GetString("comment")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }

        // Добавление новой транзакции в базу данных
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtCategory.Text) || !decimal.TryParse(TxtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Укажите категорию и корректную сумму!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO transactions (type, category, amount, transaction_date, comment) VALUES (@type, @cat, @amount, @date, @comment)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", (CmbType.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Доход");
                        cmd.Parameters.AddWithValue("@cat", TxtCategory.Text.Trim());
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@date", DpDate.SelectedDate ?? DateTime.Now);
                        cmd.Parameters.AddWithValue("@comment", TxtComment.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        // Обновление данных существующей транзакции
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись для редактирования!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtCategory.Text) || !decimal.TryParse(TxtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Укажите категорию и корректную сумму!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE transactions SET type=@type, category=@cat, amount=@amount, transaction_date=@date, comment=@comment WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.Parameters.AddWithValue("@type", (CmbType.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Доход");
                        cmd.Parameters.AddWithValue("@cat", TxtCategory.Text.Trim());
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@date", DpDate.SelectedDate ?? DateTime.Now);
                        cmd.Parameters.AddWithValue("@comment", TxtComment.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
        }

        // Удаление транзакции из базы данных
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM transactions WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        // Обработка выбора строки в таблице
        private void DgTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgTransactions.SelectedItem is Transaction t)
            {
                selectedId = t.Id;
                TxtCategory.Text = t.Category;
                TxtAmount.Text = t.Amount.ToString();
                TxtComment.Text = t.Comment;
                DpDate.SelectedDate = t.TransactionDate;
                foreach (ComboBoxItem item in CmbType.Items)
                {
                    if (item.Content.ToString() == t.Type)
                    {
                        CmbType.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        // Сброс полей ввода и выбранного ID
        private void ClearFields()
        {
            selectedId = -1;
            TxtCategory.Clear();
            TxtAmount.Clear();
            TxtComment.Clear();
            CmbType.SelectedIndex = -1;
            DpDate.SelectedDate = DateTime.Now;
            DgTransactions.UnselectAll();
        }
    }
}