using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class Trainer
    {
        Dictionary<string, List<MuscleGroup>> Days { get; set; }
        string SavePath { get; set; }

        public Trainer()
        {
            Days = new Dictionary<string, List<MuscleGroup>>();
            Days.Add("monday", new List<MuscleGroup>());
            Days.Add("tuesday", new List<MuscleGroup>());
            Days.Add("wednesday", new List<MuscleGroup>());
            Days.Add("thursday", new List<MuscleGroup>());
            Days.Add("friday", new List<MuscleGroup>());
            Days.Add("saturday", new List<MuscleGroup>());
            Days.Add("sunday", new List<MuscleGroup>());
            string fullPath = Path.GetFullPath("Training.exe");
            SavePath = fullPath.Substring(0, fullPath.LastIndexOf("\\Training\\") + 1) + "SaveInfo";
            Read();
        }
        public void AddMuscleGroup(string day, string muscle)
        {
            if (!Days.ContainsKey(day))
            {
                Console.WriteLine("no such day");
                Thread.Sleep(500);
                return;
            }
            var tmp = new MuscleGroup(muscle);
            Days[day].Add(tmp);
        }
        public void AddTask(string day, int ind, string taskName, int repetitions)
        {
            if (!Days.ContainsKey(day) || ind < 0 || ind > Days[day].Count)
            {
                Console.WriteLine("wrong input");
                Thread.Sleep(500);
                return;
            }
            Days[day][ind].Tasks.Add(new TrainingTask(taskName, repetitions));
        }
        public void DeleteMuscleGroup(string day, int ind)
        {
            if (Days.ContainsKey(day) && ind >= 0 && ind < Days[day].Count)
                Days[day].RemoveAt(ind);
            else
            {
                Console.WriteLine("Wrong input");
                Thread.Sleep(500);
            }
        }
        public void DeleteTask(string day, int muscleind, int taskind)
        {
            if (Days.ContainsKey(day) && muscleind >= 0 && muscleind < Days[day].Count && taskind >= 0 && taskind < Days[day][muscleind].Tasks.Count)
                Days[day][muscleind].Tasks.RemoveAt(taskind);
            else
            {
                Console.WriteLine("Wrong input");
                Thread.Sleep(500);
            }
        }
        public void ShowGroups(string day)
        {
            for (int i = 0; i < Days[day].Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Days[day][i].Name}");
            }
        }
        public void ShowTasks(string day, int group)
        {
            for (int i = 0; i < Days[day][group].Tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Days[day][group].Tasks[i].Name}, x{Days[day][group].Tasks[i].Repetitions}");
            }
        }
        public void ShowAll()
        {
            foreach (string day in Days.Keys)
            {
                Console.WriteLine(day);
                for (int i = 0; i < Days[day].Count; i++)
                {
                    Console.WriteLine($"\t{Days[day][i].Name}");
                    foreach (TrainingTask task in Days[day][i].Tasks)
                    {
                        Console.WriteLine($"\t\t{task.Name} x{task.Repetitions}");
                    }
                }
            }
        }

        public void Save()
        {
            foreach (string day in Days.Keys)
            {
                File.WriteAllText(SavePath + $"\\{day}.txt", "");
                foreach (MuscleGroup group in Days[day])
                {
                    File.AppendAllText(SavePath + $@"\{day}.txt", $"={group.Name}\n");
                    foreach (TrainingTask task in group.Tasks)
                    {
                        File.AppendAllText(SavePath + $@"\{day}.txt", $"{task.Name}|{task.Repetitions}\n");
                    }
                }
            }
        }
        public void Read()
        {
            foreach (string day in Days.Keys)
            {
                if (!File.Exists(SavePath + $@"\{day}.txt") || File.ReadAllText(SavePath + $@"\{day}.txt").Length == 0) continue;
                string str = File.ReadAllText(SavePath + $@"\{day}.txt");
                string[] groups = str.Split("=");
                for (int i = 1; i < groups.Length; i++)
                {
                    Days[day].Add(new MuscleGroup(groups[i].Remove(groups[i].IndexOf("\n"))));
                    string[] tasks = groups[i].Split("\n");
                    for (int j = 1; j < tasks.Length - 1; j++)
                    {
                        string[] taskDetails = tasks[j].Split("|");
                        if (taskDetails.Length == 2)
                            Days[day][i - 1].Tasks.Add(new TrainingTask(taskDetails[0], int.Parse(taskDetails[1])));
                    }
                }
            }
        }
    }
}
