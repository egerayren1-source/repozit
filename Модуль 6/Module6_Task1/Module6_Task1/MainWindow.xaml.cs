using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Module6_Task1
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string connStr = "Server=localhost;Database=employee_db;Uid=Zahar;Pwd=3036;charset=utf8;";
        public ObservableCollection<Employee> Employees { get; set; }
        private int selectedId = -1;

        public MainWindow()
        {
            InitializeComponent();
            Employees = new ObservableCollection<Employee>();
            DgEmployees.ItemsSource = Employees;
            LoadData();
        }

        // Загрузка списка сотрудников из базы данных
        private void LoadData()
        {
            Employees.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT id, full_name, position, department, salary FROM employees";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employees.Add(new Employee
                            {
                                Id = reader.GetInt32("id"),
                                FullName = reader.GetString("full_name"),
                                Position = reader.GetString("position"),
                                Department = reader.GetString("department"),
                                Salary = reader.GetDecimal("salary")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Добавление нового сотрудника в базу данных
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFullName.Text) || !decimal.TryParse(TxtSalary.Text, out decimal salary))
            {
                MessageBox.Show("Заполните корректно ФИО и Зарплату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO employees (full_name, position, department, salary) VALUES (@name, @pos, @dept, @sal)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", TxtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pos", TxtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@dept", TxtDepartment.Text.Trim());
                        cmd.Parameters.AddWithValue("@sal", salary);
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

        // Обновление данных существующего сотрудника
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите сотрудника для редактирования!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtFullName.Text) || !decimal.TryParse(TxtSalary.Text, out decimal salary))
            {
                MessageBox.Show("Заполните корректно ФИО и Зарплату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE employees SET full_name=@name, position=@pos, department=@dept, salary=@sal WHERE id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.Parameters.AddWithValue("@name", TxtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pos", TxtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@dept", TxtDepartment.Text.Trim());
                        cmd.Parameters.AddWithValue("@sal", salary);
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

        // Удаление сотрудника из базы данных
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите сотрудника для удаления!");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM employees WHERE id=@id";
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
        private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgEmployees.SelectedItem is Employee emp)
            {
                selectedId = emp.Id;
                TxtFullName.Text = emp.FullName;
                TxtPosition.Text = emp.Position;
                TxtDepartment.Text = emp.Department;
                TxtSalary.Text = emp.Salary.ToString();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        // Сброс полей ввода и выбранного ID
        private void ClearFields()
        {
            selectedId = -1;
            TxtFullName.Clear();
            TxtPosition.Clear();
            TxtDepartment.Clear();
            TxtSalary.Clear();
            DgEmployees.UnselectAll();
        }
    }
}