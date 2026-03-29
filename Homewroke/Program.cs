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
            string o = FileHelper.ReadFile(path + @"\place.log");
            if (o != string.Empty)
            {
                string[] placedJournalText = FileHelper.ReadFile(path + @"\place.log").Split('\n');
                for (int i = 0; i < placedJournalText.Length; i++)
                {
                    string[] elements = placedJournalText[i].Split('-');
                    string itemName = elements[0];
                    int slotNumb = int.Parse(elements[1]);
                    char shelf = char.Parse(elements[2]);
                    placedJournal.Add(new PlacedEvent(itemName, slotNumb, shelf));
                }
            }
            
            //foreach (string notice  in FileHelper.ReadFile(path + @"\place.log").Split('\n'))
            //{
            //    string[] elements = notice.Split('-');
            //    string itemName = elements[0];
            //    int slotNumb = int.Parse(elements[1]);
            //    char shelf = char.Parse(elements[2]);
            //    placedJournal.Add(new PlacedEvent(itemName, slotNumb, shelf));
            //}

            Journal<TakenEvent> takenJournal = new Journal<TakenEvent>();
            o = FileHelper.ReadFile(path + @"\taken.log");
            if (o != string.Empty)
            {
                string[] takenJournalText = o.Split('\n');
                for (int i = 0; i < takenJournalText.Length; i++)
                {
                    string[] elements = takenJournalText[i].Split('-');
                    string itemName = elements[0];
                    int slotNumb = int.Parse(elements[1]);
                    char shelf = char.Parse(elements[2]);
                    takenJournal.Add(new TakenEvent(itemName, slotNumb, shelf));
                }
            }

            //foreach (string notice in FileHelper.ReadFile(path + @"\taken.log").Split('\n'))
            //{
            //    string[] elements = notice.Split('-');
            //    string itemName = elements[0];
            //    int slotNumb = int.Parse(elements[1]);
            //    char shelf = char.Parse(elements[2]);
            //    takenJournal.Add(new TakenEvent(itemName, slotNumb, shelf));
            //}

            Journal<MovedEvent> movedJournal = new Journal<MovedEvent>();
            o = FileHelper.ReadFile(path + @"\moved.log");
            if (o != string.Empty)
            {
                string[] movedJournalText = o.Split('\n');
                for (int i = 0; i < movedJournalText.Length; i++)
                {
                    string[] elements = movedJournalText[i].Split('-');
                    string itemName = elements[0];
                    char shelfOut = char.Parse(elements[1]);
                    int slotNumbOut = int.Parse(elements[2]);
                    char shelfIn = char.Parse(elements[3]);
                    int slotNumbIn = int.Parse(elements[4]);
                    movedJournal.Add(new MovedEvent(itemName, shelfOut, slotNumbOut, shelfIn, slotNumbIn));
                }
            }
                
            //foreach (string notice in FileHelper.ReadFile(path + @"\moved.log").Split('\n'))
            //{
            //    string[] elements = notice.Split('-');
            //    string itemName = elements[0];
            //    char shelfOut = char.Parse(elements[1]);
            //    int slotNumbOut = int.Parse(elements[2]);
            //    char shelfIn = char.Parse(elements[3]);
            //    int slotNumbIn = int.Parse(elements[4]);
            //    movedJournal.Add(new MovedEvent(itemName, shelfOut, slotNumbOut, shelfIn, slotNumbIn));
            //}

            Journal<FailedAttemptEvent> failedJournal = new Journal<FailedAttemptEvent>();
            o = FileHelper.ReadFile(path + @"\failed.log");
            if (o != string.Empty)
            {
                string[] failedJournalText = o.Split('\n');
                for (int i = 0; i < failedJournalText.Length; i++)
                {
                    string[] elements = failedJournalText[i].Split('-');
                    string type = elements[0];
                    char shelf = char.Parse(elements[1]);
                    int slotNumb = int.Parse(elements[2]);
                    string description = elements[3];
                    failedJournal.Add(new FailedAttemptEvent(type, shelf, slotNumb, description));
                }
            }
                
            //foreach (string notice in FileHelper.ReadFile(path + @"\failed.log").Split('\n'))
            //{
            //    string[] elements = notice.Split('-');
            //    string type = elements[0];
            //    char shelf = char.Parse(elements[1]);
            //    int slotNumb = int.Parse(elements[2]);
            //    string description = elements[3];
            //    failedJournal.Add(new FailedAttemptEvent(type, shelf, slotNumb, description));
            //}

            while (programWorking)
            {
                Console.WriteLine($"=== Склад ===");
                Console.Write($"Полка А:");
                for (int i = 1; i <= shelfA.Items.Count; i++)
                {
                    Console.Write($" [{i}] {shelfA.Read(i)}  ");
                }
                Console.Write("\n");
                Console.Write($"Полка B:");
                for (int i = 1; i <= shelfB.Items.Count; i++)
                {
                    Console.Write($" [{i}] {shelfB.Read(i)}  ");
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
                        if (shelfPlace == 'A')
                        {
                            if (shelfA.Items[slotNumbPlace-1]  == "пусто")
                            {
                                placedJournal.Add(new PlacedEvent(itemNamePlace, slotNumbPlace, shelfPlace));
                                shelfA.Place(slotNumbPlace, itemNamePlace);
                            }
                            else
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот занят", 'A', slotNumbPlace, "Слот уже занят другим предметом, размещение нового невозможно"));
                                Console.WriteLine("Слот занят");
                                Console.ReadKey();
                            }
                            
                        }
                        else
                        {
                            if (shelfB.Items[slotNumbPlace-1] == "пусто")
                            {
                                placedJournal.Add(new PlacedEvent(itemNamePlace, slotNumbPlace, shelfPlace));
                                shelfB.Place(slotNumbPlace, itemNamePlace);
                            }
                            else
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот занят", 'B', slotNumbPlace, "Слот уже занят другим предметом. Размещение нового невозможно"));
                                Console.WriteLine("Слот занят");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case 2:
                        char shelfTake = CharABInput();
                        Console.WriteLine("Введите номер слота, с которого забрать предмет: ");
                        int slotNumbTake = IntInput();
                        if (shelfTake == 'A')
                        {
                            if (shelfA.Items[slotNumbTake-1] != "пусто")
                            {
                                takenJournal.Add(new TakenEvent(shelfA.Read(slotNumbTake), slotNumbTake, shelfTake));
                                shelfA.Take(slotNumbTake);
                            }
                            else
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот пуст", 'A', slotNumbTake, "Слот ничем не занят. Невозможно взять то, чего нет"));
                                Console.WriteLine("Слот пуст");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            if (shelfB.Items[slotNumbTake-1] != "пусто")
                            {
                                takenJournal.Add(new TakenEvent(shelfB.Read(slotNumbTake), slotNumbTake, shelfTake));
                                shelfB.Take(slotNumbTake);
                            }
                            else
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот пуст", 'B', slotNumbTake, "Слот ничем не занят. Невозможно взять то, чего нет"));
                                Console.WriteLine("Слот пуст");
                                Console.ReadKey();
                            }
                            takenJournal.Add(new TakenEvent(shelfA.Read(slotNumbTake), slotNumbTake, shelfTake));
                            shelfB.Take(slotNumbTake);
                        }
                        break;
                    case 3:
                        char shelfMoveOut = CharABInput();
                        Console.WriteLine("Введите номер слота, с которого перенести предмет: ");
                        int slotNumbMoveOut = IntInput();
                        if (shelfMoveOut == 'A')
                        {
                            if (shelfA.Items[slotNumbMoveOut-1] == "пусто")
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот пуст", 'A', slotNumbMoveOut, "Нечего перекладывать. Слот пуст."));
                                Console.WriteLine("Нечего перекладывать");
                                Console.ReadKey();
                                break;
                            } 
                        }
                        else
                        {
                            if (shelfB.Items[slotNumbMoveOut-1] == "пусто")
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот пуст", 'B', slotNumbMoveOut, "Нечего перекладывать. Слот пуст."));
                                Console.WriteLine("Нечего перекладывать");
                                Console.ReadKey();
                                break;
                            }
                        }
                        char shelfMoveIn = CharABInput();
                        Console.WriteLine("Введите номер слота, на который перенести предмет: ");
                        int slotNumbMoveIn = IntInput();
                        if (shelfMoveIn == 'A')
                        {
                            if (shelfA.Items[slotNumbMoveIn-1] != "пусто")
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот занят", 'A', slotNumbMoveIn, "Слот уже занят. Нельзя поставить больше"));
                                Console.WriteLine("Слот занят");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            if (shelfB.Items[slotNumbMoveIn-1] != "пусто")
                            {
                                failedJournal.Add(new FailedAttemptEvent("Слот занят", 'B', slotNumbMoveIn, "Слот уже занят. Нельзя поставить больше"));
                                Console.WriteLine("Слот занят");
                                Console.ReadKey();
                                break;
                            }
                        }
                        if (shelfMoveOut == 'A' & shelfMoveIn == 'A')
                        {
                            movedJournal.Add(new MovedEvent(shelfA.Read(slotNumbMoveOut), 'A', slotNumbMoveOut, 'A', slotNumbMoveIn));
                            shelfA.Place(slotNumbMoveIn, shelfA.Read(slotNumbMoveOut));
                            shelfA.Take(slotNumbMoveOut);
                        } else if (shelfMoveOut == 'A' & shelfMoveIn == 'B')
                        {
                            movedJournal.Add(new MovedEvent(shelfA.Read(slotNumbMoveOut), 'A', slotNumbMoveOut, 'B', slotNumbMoveIn));
                            shelfB.Place(slotNumbMoveIn, shelfA.Read(slotNumbMoveOut));
                            shelfA.Take(slotNumbMoveOut);
                        } else if (shelfMoveOut == 'B' & shelfMoveIn == 'A')
                        {
                            movedJournal.Add(new MovedEvent(shelfB.Read(slotNumbMoveOut), 'B', slotNumbMoveOut, 'A', slotNumbMoveIn));
                            shelfA.Place(slotNumbMoveIn, shelfB.Read(slotNumbMoveOut));
                            shelfB.Take(slotNumbMoveOut);
                        }
                        else
                        {
                            movedJournal.Add(new MovedEvent(shelfB.Read(slotNumbMoveOut), 'B', slotNumbMoveOut, 'B', slotNumbMoveIn));
                            shelfB.Place(slotNumbMoveIn, shelfB.Read(slotNumbMoveOut));
                            shelfB.Take(slotNumbMoveOut);
                        }
                        break;
                    case 4:
                        Console.WriteLine("--- Размещения ---");
                        foreach (PlacedEvent eventP in placedJournal.Notices)
                        {
                            Console.WriteLine(eventP.ToScreenLine());
                        }
                        Console.WriteLine("--- Изъятия ---");
                        foreach (TakenEvent eventT in takenJournal.Notices)
                        {
                            Console.WriteLine(eventT.ToScreenLine());
                        }
                        Console.WriteLine("--- Переносы ---");
                        foreach (MovedEvent eventM in movedJournal.Notices)
                        {
                            Console.WriteLine(eventM.ToScreenLine());
                        }
                        Console.WriteLine("--- Неуспешные попытки ---");
                        foreach (FailedAttemptEvent eventF in failedJournal.Notices)
                        {
                            Console.WriteLine(eventF.ToScreenLine());
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        programWorking = false;
                        placedJournal.SaveToFile(path + @"\place.log");
                        takenJournal.SaveToFile(path + @"\taken.log");
                        movedJournal.SaveToFile(path + @"\moved.log");
                        failedJournal.SaveToFile(path + @"\failed.log");
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
