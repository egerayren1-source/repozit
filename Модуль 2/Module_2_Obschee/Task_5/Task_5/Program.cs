using System;

// делегат для события изменения температуры
delegate void TemperatureChangedEventHandler(double newTemperature);

// класс датчика температуры (издатель события)
class TemperatureSensor
{
    private double currentTemperature;

    // событие изменения температуры
    public event TemperatureChangedEventHandler TemperatureChanged;

    public void SetTemperature(double temp)
    {
        if (currentTemperature != temp)
        {
            currentTemperature = temp;
            Console.WriteLine($"\n[датчик]: температура изменилась на {currentTemperature}°C");

            // вызов события, если есть подписчики
            if (TemperatureChanged != null)
            {
                TemperatureChanged(currentTemperature);
            }
        }
    }
}

// класс термостата (подписчик на событие)
class Thermostat
{
    private double targetTemperature;
    private bool isHeatingOn;

    public Thermostat(double targetTemp)
    {
        targetTemperature = targetTemp;
        isHeatingOn = false;
    }

    // метод-обработчик события
    public void OnTemperatureChanged(double newTemperature)
    {
        if (newTemperature < targetTemperature && !isHeatingOn)
        {
            isHeatingOn = true;
            Console.WriteLine($"[термостат]: температура ниже нормы ({targetTemperature}°C). отопление ВКЛЮЧЕНО.");
        }
        else if (newTemperature >= targetTemperature && isHeatingOn)
        {
            isHeatingOn = false;
            Console.WriteLine($"[термостат]: целевая температура достигнута. отопление ВЫКЛЮЧЕНО.");
        }
        else
        {
            Console.WriteLine($"[термостат]: режим отопления не изменился (отопление {(isHeatingOn ? "включено" : "выключено")}).");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 5: события и термостат\n");

        // создание датчика и термостата с целевой температурой 20°C
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat(20.0);

        // подписка термостата на событие датчика
        sensor.TemperatureChanged += thermostat.OnTemperatureChanged;

        Console.WriteLine("целевая температура термостата: 20°C");

        // имитация изменения температуры
        sensor.SetTemperature(15.0); // ниже 20, включит отопление
        sensor.SetTemperature(18.0); // все еще ниже, отопление остается включенным
        sensor.SetTemperature(21.0); // выше 20, выключит отопление
        sensor.SetTemperature(22.0); // выше 20, отопление остается выключенным
    }
}