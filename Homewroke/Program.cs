using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Homewroke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Proga";
            FileHelper.DirectoryExists(path);
            FileHelper.FileExists(path + @"\place.log");
            FileHelper.FileExists(path + @"\taken.log");
            FileHelper.FileExists(path + @"\moved.log");
            FileHelper.FileExists(path + @"\failed.log");

            bool programWorking = true;

            Shelf shelfA = new Shelf();
            Shelf shelfB = new Shelf();
            Journal<PlacedEvent> placedJournal = new Journal<PlacedEvent>();
            Journal<TakenEvent> takenJournal = new Journal<TakenEvent>();
            Journal<MovedEvent> movedJournal = new Journal<MovedEvent>();
            
            while (programWorking)
            {
                foreach (string item in shelfA.Items)
                {
                    Console.Write($" |_{item}_| ");
                }
                Console.Write("\n");
                foreach (string item in shelfB.Items)
                {
                    Console.Write($" |_{item}_| ");
                }

                Console.Write("\n");
                int actionNumb = ChooseMenu();

                switch (actionNumb)
                {
                    case 1:
                        char shelfPlace = CharABInput();
                        Console.WriteLine("Введите номер слота, в который положить предмет: ");
                        int slotNumbPlace = IntInput();
                        Console.WriteLine("Введите название предмета");
                        string itemNamePlace = Console.ReadLine();
                        placedJournal.Add(new PlacedEvent(shelfPlace, slotNumbPlace, itemNamePlace));
                        if (shelfPlace == 'A')
                        {
                            shelfA.Place(slotNumbPlace, itemNamePlace);
                        }
                        else
                        {
                            shelfB.Place(slotNumbPlace, itemNamePlace);
                        }
                        break;
                    case 2:
                        char shelfTake = CharABInput();
                        Console.WriteLine("Введите номер слота, с которого забрать предмет: ");
                        int slotNumbTake = IntInput();
                        if (shelfTake == 'A')
                        {
                            takenJournal.Add(new TakenEvent(shelfTake, slotNumbTake, shelfA.Read(slotNumbTake)));
                            shelfA.Take(slotNumbTake);
                        }
                        else
                        {
                            takenJournal.Add(new TakenEvent(shelfTake, slotNumbTake, shelfB.Read(slotNumbTake)));
                            shelfB.Take(slotNumbTake);
                        }
                        break;
                    case 3:
                        char shelfMoveOut = CharABInput();
                        Console.WriteLine("Введите номер слота, с которого перенести предмет: ");
                        int slotNumbMoveOut = IntInput();
                        Console.WriteLine("Введите номер слота, на который перенести предмет: ");
                        int slotNumbMoveIn = IntInput();

                        if (shelfMoveOut == 'A')
                        {

                            movedJournal.Notices.Add(new MovedEvent('A', 'B', slotNumbMoveOut, slotNumbMoveIn, shelfA.Read(slotNumbMoveOut)));
                            shelfB.Place(slotNumbMoveIn, shelfA.Read(slotNumbMoveOut));
                            shelfA.Take(slotNumbMoveOut);
                        }
                        else
                        {
                            movedJournal.Notices.Add(new MovedEvent('B', 'A', slotNumbMoveOut, slotNumbMoveIn, shelfB.Read(slotNumbMoveOut)));
                            shelfA.Place(slotNumbMoveIn, shelfB.Read(slotNumbMoveOut));
                            shelfB.Take(slotNumbMoveOut);
                        }
                        break;
                    case 4:
                        Console.WriteLine("placed:");
                        placedJournal.Show();
                        Console.WriteLine("taken:");
                        takenJournal.Show();
                        Console.WriteLine("moved:");
                        movedJournal.Show();
                         break;
                    case 5:
                        programWorking = false;
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }

        }

        public static char CharABInput()
        {
            Console.WriteLine("Выбериты полку (A/B): ");
            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out char ch) & (ch == 'A' | ch == 'B'))
                {
                    return ch;
                }
                else
                {
                    Console.WriteLine("Неподходящее значение");
                }
            }
        }

        public static int IntInput()
        {
            while (true)
            {

                if (int.TryParse(Console.ReadLine(), out int input) & input >= 1 & input <= 10)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Неподходящее значение");
                }
            }
            
        }
        public static int ChooseMenu()
        {
            Console.WriteLine
                ($"1 - Положить товар\n" +
                $"2 - Забрать товар\n" +
                $"3 - Перенести товар\n" +
                $"4 - Показать журналы\n" +
                $"5 - Выход");
            while (true)
            {
                int choosedAction = IntInput();
                if (choosedAction >= 1 & choosedAction <= 5)
                {
                    return choosedAction;
                }
            }
        }


    }
}
