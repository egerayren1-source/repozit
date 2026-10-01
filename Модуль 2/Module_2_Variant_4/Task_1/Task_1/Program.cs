using System;

namespace Task1
{
    public class BankAccount
    {
        private string accountNumber = ""; // номер счета
        private string owner = "";         // имя владельца
        private decimal balance;           // баланс счета

        public string AccountNumber
        {
            get { return accountNumber; } // свойство для номера счета
            private set { accountNumber = value ?? ""; }
        }

        public string Owner
        {
            get { return owner; } // свойство для имени владельца
            set { owner = value ?? ""; }
        }

        public decimal Balance
        {
            get { return balance; } // свойство для баланса
            private set { balance = value; }
        }

        public BankAccount(string accountNumber, string owner, decimal initialBalance) // конструктор с начальным балансом
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance >= 0 ? initialBalance : 0; // проверка: баланс не может быть отрицательным
        }

        public BankAccount(string accountNumber, string owner) : this(accountNumber, owner, 0) { } // конструктор по умолчанию (баланс = 0)

        public void Deposit(decimal amount) // пополнение счета
        {
            if (amount > 0)
            {
                Balance += amount; // увеличиваем баланс
                Console.WriteLine($"пополнено на {amount}. текущий баланс: {Balance}");
            }
            else Console.WriteLine("сумма пополнения должна быть больше 0");
        }

        public void Withdraw(decimal amount) // снятие средств
        {
            if (amount <= 0) Console.WriteLine("сумма снятия должна быть больше 0");
            else if (amount <= Balance)
            {
                Balance -= amount; // уменьшаем баланс
                Console.WriteLine($"снято {amount}. остаток: {Balance}");
            }
            else Console.WriteLine("недостаточно средств на счете");
        }

        public void PrintInfo() // вывод информации о счете
        {
            Console.WriteLine($"счет: {AccountNumber} | владелец: {Owner} | баланс: {Balance}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("введите номер счета: ");
            string number = Console.ReadLine() ?? ""; // ввод данных счета

            Console.Write("введите имя владельца: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("введите начальный баланс: ");
            decimal.TryParse(Console.ReadLine(), out decimal money);

            BankAccount account = new BankAccount(number, name, money); // создание объекта счета

            while (true) // главное меню
            {
                Console.WriteLine("\n--- меню ---\n1 - информация о счете\n2 - пополнить\n3 - снять\n0 - выход");
                Console.Write("выбор: ");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1") account.PrintInfo(); // вывод данных
                else if (choice == "2")
                {
                    Console.Write("сумма: ");
                    decimal.TryParse(Console.ReadLine(), out decimal sum);
                    account.Deposit(sum); // пополнение
                }
                else if (choice == "3")
                {
                    Console.Write("сумма: ");
                    decimal.TryParse(Console.ReadLine(), out decimal sum);
                    account.Withdraw(sum); // снятие
                }
                else if (choice == "0") break; // выход из программы
            }
        }
    }
}