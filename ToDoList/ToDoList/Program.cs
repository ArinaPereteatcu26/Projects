using System.Collections.Generic;

class Program
{
    private static List<string> toDoList = new List<string>();

    enum UserChoice
    {
        AddTask = 1,
        DeleteTask,
        Exit
    }

    static void Main(string[] args)
    {
        
        while (true)
        {
            if (toDoList.Count > 0)
            {
                Console.WriteLine("To do list:");
                for (int i = 0; i < toDoList.Count; i++)
                {
                    Console.WriteLine("-"+ toDoList[i]);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No tasks to do");
            }

           // Menu options 
            Console.WriteLine("1.Add task");
            Console.WriteLine("2.Delete task");
            Console.WriteLine("3.Exit program");
            
            int choice = int.Parse(Console.ReadLine());
            if (choice == (int)UserChoice.AddTask)
            {
                Console.WriteLine("Enter task:");
                string task = Console.ReadLine();
                toDoList.Add(task);
                Console.Clear();
                Console.WriteLine("Task added");
            }
            else if (choice == (int)UserChoice.DeleteTask)
            {
                if (toDoList.Count > 0)
                {
                    Console.WriteLine("Enter task to delete:");
                    for (int i = 0; i < toDoList.Count; i++)
                    {
                        Console.WriteLine("(" + (i + 1) + ")" + toDoList[i]);
                    }

                    int taskNum = int.Parse(Console.ReadLine());
                    taskNum--;

                    if (taskNum >= 0 && taskNum < toDoList.Count)
                    {
                        toDoList.RemoveAt(taskNum);
                        Console.Clear();
                        Console.WriteLine("Task deleted");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid task number");
                    }
                }
            }

            else if (choice == (int)UserChoice.Exit)
            {
                break;
            }
        }
        
    }
}