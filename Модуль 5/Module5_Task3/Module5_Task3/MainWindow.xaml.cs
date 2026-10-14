using System.Collections.ObjectModel;
using System.Windows;

namespace Module5_Task3
{
    public class TaskItem
    {
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>();
            TaskListBox.ItemsSource = Tasks;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string taskText = TxtTaskInput.Text.Trim();
            if (string.IsNullOrEmpty(taskText))
            {
                MessageBox.Show("Поле задачи не может быть пустым!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Tasks.Add(new TaskItem { Text = taskText, IsCompleted = false });
            TxtTaskInput.Clear();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = TaskListBox.SelectedItem as TaskItem;
            if (selectedTask != null)
            {
                Tasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления из списка!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}