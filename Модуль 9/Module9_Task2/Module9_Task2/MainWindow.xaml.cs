using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Module9_Task2
{
    // Модель данных задачи для привязки к таблице
    public class ProjectTaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        public string ProgressFormat => $"{Progress}%";
        public DateTime DueDate { get; set; }
    }

    public partial class MainWindow : Window
    {
        // Строка подключения к базе данных проектного управления
        private string connStr = "Server=localhost;Database=project_management_db;Uid=Zahar;Pwd=3036;charset=utf8;";

        public ObservableCollection<ProjectTaskModel> Tasks { get; set; }
        private int selectedId = -1;

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<ProjectTaskModel>();
            DgTasks.ItemsSource = Tasks;
            DpDueDate.SelectedDate = DateTime.Now.AddDays(3);

            LoadTasks();
            CheckUpcomingDeadlines(); // Проверка уведомлений о сроках при старте
        }

        // Загрузка задач из базы данных
        private void LoadTasks()
        {
            Tasks.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, priority, status, progress, due_date FROM tasks";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tasks.Add(new ProjectTaskModel
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                Priority = reader.GetString("priority"),
                                Status = reader.GetString("status"),
                                Progress = reader.GetInt32("progress"),
                                DueDate = reader.GetDateTime("due_date")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД: " + ex.Message);
            }
        }

        // Проверка уведомлений о приближающихся сроках выполнения (сегодня или просрочено)
        private void CheckUpcomingDeadlines()
        {
            var today = DateTime.Today;
            var urgentTasks = Tasks.Where(t => t.DueDate.Date <= today && t.Status != "Завершена").ToList();

            if (urgentTasks.Count > 0)
            {
                string message = "Внимание! Истекает или истек срок выполнения следующих задач:\n\n";
                foreach (var task in urgentTasks)
                {
                    message += $"- {task.Title} (Срок: {task.DueDate:dd.MM.yyyy})\n";
                }
                MessageBox.Show(message, "Уведомление о сроках", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Добавление новой задачи
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
                    // Привязываем по умолчанию к первому проекту (project_id = 1)
                    string query = "INSERT INTO tasks (project_id, title, description, priority, status, progress, due_date) VALUES (1, @title, @desc, @priority, @status, @progress, @date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", TxtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@priority", (CmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Средний");
                        cmd.Parameters.AddWithValue("@status", (CmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новая");
                        cmd.Parameters.AddWithValue("@progress", (int)SldProgress.Value);
                        cmd.Parameters.AddWithValue("@date", DpDueDate.SelectedDate ?? DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTasks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        // Редактирование существующей задачи
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите задачу для обновления!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE tasks SET title=@title, description=@desc, priority=@priority, status=@status, progress=@progress, due_date=@date WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.Parameters.AddWithValue("@title", TxtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@priority", (CmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Средний");
                        cmd.Parameters.AddWithValue("@status", (CmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новая");
                        cmd.Parameters.AddWithValue("@progress", (int)SldProgress.Value);
                        cmd.Parameters.AddWithValue("@date", DpDueDate.SelectedDate ?? DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTasks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
        }

        // Удаление задачи
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите задачу для удаления!");
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
                LoadTasks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        // Заполнение полей при выборе строки в таблице
        private void DgTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgTasks.SelectedItem is ProjectTaskModel task)
            {
                selectedId = task.Id;
                TxtTitle.Text = task.Title;
                TxtDescription.Text = task.Description;
                SldProgress.Value = task.Progress;
                DpDueDate.SelectedDate = task.DueDate;

                // Установка приоритета в ComboBox
                foreach (ComboBoxItem item in CmbPriority.Items)
                {
                    if (item.Content.ToString() == task.Priority)
                    {
                        CmbPriority.SelectedItem = item;
                        break;
                    }
                }

                // Установка статуса в ComboBox
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

        // Сброс полей ввода формы
        private void ClearFields()
        {
            selectedId = -1;
            TxtTitle.Clear();
            TxtDescription.Clear();
            CmbPriority.SelectedIndex = 1;
            CmbStatus.SelectedIndex = 0;
            SldProgress.Value = 0;
            DpDueDate.SelectedDate = DateTime.Now.AddDays(3);
            DgTasks.UnselectAll();
        }
    }
}