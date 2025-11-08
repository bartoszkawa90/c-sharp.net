using System;
using System.Runtime.CompilerServices;


/*
 *  First code which should do some TODO list in terminal
 */

namespace First
{
    class CommandLinePrinter
    {
        private int _id;
        private StorageClass _storage;

        public string separator;
        public CommandLinePrinter(string separator, ref StorageClass storage)
        {
            this._id = storage.GetId();
            storage.SetId(this._id + 1);
            this._storage = storage;
            this.separator = separator;
        }

        public void PrintMain()
        {
            Console.WriteLine($"Running TODO list session {this._id}");
        }

        public void Print(string message)
        {
            Console.WriteLine($"{this.separator}{message}");
        }

        public void PrintList(List<Item> items)
        {
            foreach (var item in items)
            {
                // var (dateTime, text,  priority) = item.GetText();
                Console.WriteLine(item.ToString());
            }
        }
    }


    class StorageClass
    {
        public static int sid = 1;
        public int Sid;

        public StorageClass()
        {
            // Initialize storage
            this.Sid = sid;
            sid++;
        }
        public int GetId() { return this.Sid; }
        public void SetId(int ids) { this.Sid = ids; }
    }


    class Item
    {
        private DateTime dateTime;
        private string text;
        private int priority;
        public static int id = 1;
        private int Id;
        
        public Item(string text, int priority)
        {
            this.dateTime = DateTime.Now;
            this.text = text;
            this.priority = priority;
            this.Id = id;
            id++;
        }

        public void SetText(string text)
        {
            // Set text of todo list item
            this.text = text;
        }

        public (int Id, DateTime dateTime, string text, int priority) GetText()
        {
            // get text and priority of Item
            return (this.Id, this.dateTime, this.text, this.priority);
        }

        public int GetId()
        {
            return this.Id;
        }

        public override string ToString()
        {
            return $"Id: {this.Id} Create Date: {this.dateTime} Content:{this.text} Priority: {this.priority}";
        }
    }
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            StorageClass storage = new StorageClass();
            CommandLinePrinter printer = new CommandLinePrinter("    ", ref storage);
            List<Item> records = new List<Item>();
            Console.WriteLine($"storage.Sid = {storage.Sid}");
            // Start main loop
            while (true)
            {
                printer.PrintMain();
                if (records.Count == 0)
                {
                    printer.Print("No records found");
                }
                else
                {
                    printer.PrintList(records);
                }

                // collect input
                Console.WriteLine("\n\n");
                Console.WriteLine("***** @ ***** \n" +
                                  "Add item - post {content} {priority}\n" +
                                  "Edit whole item - put {content} {priority}\n" +
                                  "Edit some item fields - patch content:{content}(optional) priority:{priority}(optional)\n" +
                                  "Delete item - delete {Id}" +
                                  "\n***** @ *****");
                Console.WriteLine("Choose an action:");
                var action = Console.ReadLine();
                // Split sting
                List<string> substrings = action.Split(' ').ToList();
                if (substrings.Count < 2)
                {
                    Console.WriteLine("Wrong action passed");
                }

                StorageClass recordStorage = new StorageClass();
                switch (substrings[0])
                {
                    case "post":
                        Console.WriteLine("Adding new item");
                        Item newItem = new Item(substrings[1], int.Parse(substrings[2]));
                        Console.WriteLine($"Adding new item {newItem.GetId()}");
                        records.Add(newItem);
                        break;
                    case "put":
                        Console.WriteLine($"Updating {substrings[1]} item");
                        foreach (var item in records)
                        {
                            //TODO put
                        }
                        break;
                    case "patch":
                        Console.WriteLine($"Updating fields {substrings[2..]} of {substrings[1]} item");
                        //TODO patch
                        break;
                    case "delete":
                        Console.WriteLine("Deleting new item");
                        records.RemoveAll(p => p.GetId() == int.Parse(substrings[1]));
                        break;
                    default:
                        Console.WriteLine("Wrong option was passed");
                        break;
                }
                Console.Clear();
            }
        }
    }
}

