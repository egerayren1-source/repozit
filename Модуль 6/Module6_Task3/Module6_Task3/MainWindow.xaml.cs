using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Module6_Task3
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string connStr = "Server=localhost;Database=library_db;Uid=Zahar;Pwd=3036;charset=utf8;";
        public ObservableCollection<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>();
            DgBooks.ItemsSource = Books;
            LoadData();
        }

        // Загрузка списка книг из базы данных с учетом поискового запроса
        private void LoadData(string searchQuery = "")
        {
            Books.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, title, author, year_published, is_available FROM books " +
                                   "WHERE title LIKE @search OR author LIKE @search";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{searchQuery}%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Books.Add(new Book
                                {
                                    Id = reader.GetInt32("id"),
                                    Title = reader.GetString("title"),
                                    Author = reader.GetString("author"),
                                    YearPublished = reader.GetInt32("year_published"),
                                    IsAvailable = reader.GetBoolean("is_available")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => LoadData(TxtSearch.Text.Trim());

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Clear();
            LoadData();
        }

        // Оформление аренды выбранной книги
        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            if (DgBooks.SelectedItem is Book selectedBook)
            {
                if (!selectedBook.IsAvailable)
                {
                    MessageBox.Show("Эта книга уже находится в аренде!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                UpdateBookStatus(selectedBook.Id, false);
            }
            else
            {
                MessageBox.Show("Выберите книгу для аренды!");
            }
        }

        // Возврат выбранной книги в библиотеку
        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (DgBooks.SelectedItem is Book selectedBook)
            {
                if (selectedBook.IsAvailable)
                {
                    MessageBox.Show("Эта книга и так находится в библиотеке!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                UpdateBookStatus(selectedBook.Id, true);
            }
            else
            {
                MessageBox.Show("Выберите книгу для возврата!");
            }
        }

        // Изменение статуса доступности книги в базе данных
        private void UpdateBookStatus(int bookId, bool isAvailable)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE books SET is_available=@status WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", isAvailable);
                        cmd.Parameters.AddWithValue("@id", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData(TxtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
        }
    }
}