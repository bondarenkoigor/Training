using System;

namespace Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Trainer trainer = new Trainer();
            do
            {
                Console.Clear();
                Console.Write("1 - add muscle group\n2 - add task\n3 - delete muscle group\n4 - delete task\n5 - show all\n0 - exit\n");
                choice = int.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        {
                            Console.Write("Week day(as text):");
                            string weekday = Console.ReadLine().ToLower();
                            Console.Write("Muscle group:");
                            string muscleGroup = Console.ReadLine().ToLower();
                            trainer.AddMuscleGroup(weekday, muscleGroup);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Week day(as text):");
                            string weekday = Console.ReadLine().ToLower();
                            trainer.ShowGroups(weekday);
                            Console.Write("Select muscle group:");
                            int ind = int.Parse(Console.ReadLine());
                            Console.Write("Task name:");
                            string taskname = Console.ReadLine().ToLower();
                            Console.Write("Number of task repetitions:");
                            int num = int.Parse(Console.ReadLine());
                            trainer.AddTask(weekday, ind - 1, taskname, num);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Week day(as text):");
                            string weekday = Console.ReadLine().ToLower();
                            trainer.ShowGroups(weekday);
                            Console.Write("Select muscle group:");
                            int ind = int.Parse(Console.ReadLine());
                            trainer.DeleteMuscleGroup(weekday, ind - 1);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Week day(as text):");
                            string weekday = Console.ReadLine().ToLower();
                            trainer.ShowGroups(weekday);
                            Console.Write("Select muscle group:");
                            int group = int.Parse(Console.ReadLine());
                            trainer.ShowTasks(weekday, group);
                            Console.Write("Select task:");
                            int task = int.Parse(Console.ReadLine());
                            trainer.DeleteTask(weekday, group - 1, task - 1);
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            trainer.ShowAll();
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadLine();
                            break;
                        }
                }
                trainer.Save();
            } while (choice != 0);
        }
    }
}
