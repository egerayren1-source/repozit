using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Module6_Task2
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string connStr = "Server=localhost;Database=task_db;Uid=Zahar;Pwd=3036;charset=utf8;";
        public ObservableCollection<TaskModel> Tasks { get; set; }
        private int selectedId = -1;

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskModel>();
            DgTasks.ItemsSource = Tasks;
            DpDueDate.SelectedDate = DateTime.Now.AddDays(1);
            LoadData();
        }

        // Загрузка списка задач из базы данных
        private void LoadData()
        {
            Tasks.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, status, due_date FROM tasks";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tasks.Add(new TaskModel
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                Status = reader.GetString("status"),
                                DueDate = reader.GetDateTime("due_date")
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

        // Добавление новой задачи в базу данных
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO tasks (title, description, status, due_date) VALUES (@title, @desc, @status, @date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", TxtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", (CmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новая");
                        cmd.Parameters.AddWithValue("@date", DpDueDate.SelectedDate ?? DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        // Обновление данных существующей задачи
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите задачу для обновления!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE tasks SET title=@title, description=@desc, status=@status, due_date=@date WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.Parameters.AddWithValue("@title", TxtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", (CmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новая");
                        cmd.Parameters.AddWithValue("@date", DpDueDate.SelectedDate ?? DateTime.Now);
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

        // Удаление задачи из базы данных
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите задачу!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM tasks WHERE id=@id";
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
        private void DgTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgTasks.SelectedItem is TaskModel task)
            {
                selectedId = task.Id;
                TxtTitle.Text = task.Title;
                TxtDescription.Text = task.Description;
                DpDueDate.SelectedDate = task.DueDate;
                foreach (ComboBoxItem item in CmbStatus.Items)
                {
                    if (item.Content.ToString() == task.Status)
                    {
                        CmbStatus.SelectedItem = item;
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
            TxtTitle.Clear();
            TxtDescription.Clear();
            CmbStatus.SelectedIndex = -1;
            DpDueDate.SelectedDate = DateTime.Now.AddDays(1);
            DgTasks.UnselectAll();
        }
    }
}