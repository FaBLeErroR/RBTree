using System;

namespace TreeSauce
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree numbers = new Tree();
            Tree numbers2 = new Tree();
            numbers.NilMaker();
            numbers2.NilMaker();
            numbers.Insert("01.00.05");
            numbers.Insert("02.00.05");
            numbers.Insert("03.00.05");
            numbers.Insert("04.00.05");
            numbers.Insert("05.00.05");
            numbers.Insert("06.00.05");
            numbers.Insert("07.00.05");
            numbers.Insert("08.00.05");
            numbers.Insert("09.00.05");
            numbers.Insert("10.00.05");
            numbers.Insert("11.00.05");
            numbers.Insert("12.00.05");
            numbers.Insert("13.00.05");
            numbers.Insert("14.00.05");
            numbers.Insert("15.00.05");
            numbers.Insert("01.00.07");
            numbers.Insert("01.00.01");

            string n = "";
            string k;
            while (n != "10")
            {
                n = Console.ReadLine();
                switch (n)
                {
                    case "1":
                        numbers.Clean();
                        break;
                    case "2":
                        k = Console.ReadLine();
                        numbers.Insert(k);
                        break;
                    case "3":
                        k = Console.ReadLine();
                        numbers.Delete(k);
                        break;
                    case "4":
                        k = Console.ReadLine();
                        Console.WriteLine(numbers.Find(k));
                        break;
                    case "5":
                        numbers.PrintMinimum();
                        break;
                    case "6":
                        numbers.LRPrint();
                        break;
                    case "7":
                        numbers.RLPrint();
                        break;
                    case "8":
                        numbers.NLRPrint();
                        break;
                    case "9":
                        numbers.Draw();
                        break;
                    case "10":
                        break;
                    default:
                        Console.WriteLine("Такого пункта не существует");
                        break;
                }
                Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            }
        }
    }
}
