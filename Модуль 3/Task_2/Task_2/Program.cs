using System;

namespace Task2
{
    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; } // текстовое содержимое или данные входящего уведомления
        public NotificationEventArgs(string message) => Message = message; // конструктор передачи текста в аргументы
    }

    public class NotificationService
    {
        public event EventHandler<NotificationEventArgs> MessageReceived; // событие при получении текстового сообщения
        public event EventHandler<NotificationEventArgs> CallReceived; // входящего вызова
        public event EventHandler<NotificationEventArgs> EmailReceived; // нового электронного письма

        public void SendMessage(string msg) => MessageReceived?.Invoke(this, new NotificationEventArgs(msg)); // безопасный вызов события смс
        public void MakeCall(string caller) => CallReceived?.Invoke(this, new NotificationEventArgs(caller)); // звонка
        public void SendEmail(string email) => EmailReceived?.Invoke(this, new NotificationEventArgs(email)); // письма
    }

    internal class Program
    {
        static void Main()
        {
            var service = new NotificationService(); // экземпляр издателя событий мобильного приложения
            service.MessageReceived += (s, e) => Console.WriteLine($"[SMS] Получено: {e.Message}"); // подписка анонимного обработчика смс
            service.CallReceived += (s, e) => Console.WriteLine($"[Звонок] Вызов от: {e.Message}"); // звонков
            service.EmailReceived += (s, e) => Console.WriteLine($"[Email] Письмо от: {e.Message}"); //почты

            service.SendMessage("Новое сообщение"); // генерация события с передачей текста сообщения
            service.MakeCall("+375 (8) 800-555-5535"); // номера абонента
            service.SendEmail("filipkov@mail.com"); // почтового адреса
        }
    }
}