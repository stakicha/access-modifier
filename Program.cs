using System;
using IronBarCode;

internal class Program
{
    public class ConsoleUtility
    {
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        const string _twirl = "-\\|/";
        public static void WriteProgressBar(int percent, bool update = false)
        {
            if (update)
                Console.Write(_back);
            Console.Write("[");
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write(' ');
                else
                    Console.Write(_block);
            }
            Console.Write("] {0,3:##0}%", percent);
        }
        public static void WriteProgress(int progress, bool update = false)
        {
            if (update)
                Console.Write("\b");
            Console.Write(_twirl[progress % _twirl.Length]);
        }
    }
    private static void Main(string[] args)
    {
        int service;
        string? firstName;
        string? lastName;
        string qrValue;
        ConsoleUtility StatusBar = new ConsoleUtility();

        AccessController.TextCenter.Headline("Добре дошли в Контрол на достъп");
        AccessController.TextCenter.Headline("Попълнете данните");

        Console.WriteLine("Моля изберете вид услуга: \n 1. Услуга 1; \n 2. Услуга 2; \n 3. Услуга 3; \n 4. Услуга 4;");
        service = Convert.ToInt32(Console.ReadLine());
        while (service > 4 || service < 0)
        {
            Console.WriteLine("Избраната услуга е извън списъка. Опитайте отново: ");
            service = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("Вие избрахте услуга: " + service);
        Console.WriteLine("Моля въведете име: ");
        firstName = Console.ReadLine();
        Console.WriteLine("Моля въведете фамилия: ");
        lastName = Console.ReadLine();

        while (firstName == "" || lastName == "" || firstName == " " || lastName == " ")
        {
            Console.WriteLine("Името или фамилията не са въведени, моля опитайте отново:");
            Console.WriteLine("Въведете име: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Въведете фамилия: ");
            lastName = Console.ReadLine();
        }
        Console.Clear();

        qrValue = firstName + " " + lastName + " signed up for Service " + Convert.ToString(service);

        Console.WriteLine("Вашият код за достъп се генерира. Моля изчакайте.");
        ConsoleUtility.WriteProgressBar(100);
        for (var i = 0; i <= 100; ++i)
        {
            ConsoleUtility.WriteProgressBar(i, true);
            Thread.Sleep(5);
        }
        Console.WriteLine();
        ConsoleUtility.WriteProgress(0);
        for (var i = 0; i <= 100; ++i)
        {
            ConsoleUtility.WriteProgress(i, true);
            Thread.Sleep(5);
        }

        GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(qrValue, BarcodeEncoding.QRCode);
        barcode.SaveAsPng(firstName + "qrcode.png");

        Console.WriteLine("\nКодът за достъп е успешно генериран!");
        Console.ReadLine();
    }
}
