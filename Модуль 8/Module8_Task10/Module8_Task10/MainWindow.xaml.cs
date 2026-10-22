using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Module8_Task10
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string connStr = "Server=localhost;Database=task_db;Uid=Zahar;Pwd=3036;charset=utf8;";

        private List<TaskModel> allTasks = new List<TaskModel>();
        public ObservableCollection<TaskModel> Tasks { get; set; }
        private int selectedId = -1;

        // Переменная для запоминания режима фильтрации (по сроку или по дате создания)
        private string currentFilterType = "due";

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskModel>();
            DgTasks.ItemsSource = Tasks;
            DpDueDate.SelectedDate = DateTime.Now.AddDays(1);

            LoadData();
            CheckReminders();
        }

        // Загрузка всех задач из базы данных в память программы
        private void LoadData()
        {
            allTasks.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, status, due_date, created_at FROM tasks";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allTasks.Add(new TaskModel
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                Status = reader.GetString("status"),
                                DueDate = reader.GetDateTime("due_date"),
                                CreatedAt = reader.GetDateTime("created_at")
                            });
                        }
                    }
                }
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }

        // Проверка задач на сегодняшний день при запуске программы
        private void CheckReminders()
        {
            var today = DateTime.Today;
            var urgentTasks = allTasks.Where(t => t.DueDate.Date == today && t.Status != "Завершена").ToList();

            if (urgentTasks.Count > 0)
            {
                string message = $"У вас есть задачи, срок выполнения которых истекает сегодня ({today:dd.MM.yyyy}):\n\n";
                foreach (var task in urgentTasks)
                {
                    message += $"- {task.Title}\n";
                }
                MessageBox.Show(message, "Напоминание о задачах", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Переключение фильтра на дату срока выполнения
        private void BtnFilterDue_Click(object sender, RoutedEventArgs e)
        {
            currentFilterType = "due";
            TxtFilterMode.Text = "Фильтр: по сроку";
            ApplyFilter();
        }

        // Переключение фильтра на дату создания задачи
        private void BtnFilterCreated_Click(object sender, RoutedEventArgs e)
        {
            currentFilterType = "created";
            TxtFilterMode.Text = "Фильтр: по созданию";
            ApplyFilter();
        }

        // Срабатывает при выборе дня в календаре
        private void TaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        // Полная отмена фильтра по датам
        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            TaskCalendar.SelectedDate = null;
            ApplyFilter();
        }

        // Отбор задач для отображения в таблице в зависимости от выбранного режима и даты
        private void ApplyFilter()
        {
            Tasks.Clear();
            IEnumerable<TaskModel> filtered = allTasks;

            if (TaskCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = TaskCalendar.SelectedDate.Value.Date;

                if (currentFilterType == "due")
                {
                    filtered = allTasks.Where(t => t.DueDate.Date == selectedDate);
                }
                else
                {
                    filtered = allTasks.Where(t => t.CreatedAt.Date == selectedDate);
                }
            }

            foreach (var task in filtered)
            {
                Tasks.Add(task);
            }
        }

        // Добавление новой записи в базу данных
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

        // Сохранение изменений в выбранной задаче
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

        // Удаление выбранной задачи из базы данных
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

        // Перенос данных из выбранной строки таблицы обратно в поля ввода
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

        // Очистка формы ввода и снятие выделения с таблицы
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