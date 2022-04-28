using System;
using System.Threading;
namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Trainer trainer = new Trainer();
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter weekday:");
                    string day = Console.ReadLine();
                    if (!trainer.ShowGroups(day)) continue;
                    Console.Write("Select muscle group:");
                    int choice = int.Parse(Console.ReadLine());
                    trainer.ShowTasks(day, choice - 1);
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wrong input");
                    Thread.Sleep(500);
                }
            } while (true);
        }
    }
}
