using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Client
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
            string fullPath = Path.GetFullPath("Client.exe");
            SavePath = fullPath.Substring(0, fullPath.LastIndexOf("\\Client\\") + 1) + "SaveInfo";
            Read();
        }
        public bool ShowGroups(string day)
        {
            if (Days[day].Count == 0)
            {
                Console.WriteLine("no training today");
                Thread.Sleep(500);
                return false;
            }
            for (int i = 0; i < Days[day].Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Days[day][i].Name}");
            }
            return true;
        }
        public void ShowTasks(string day, int group)
        {
            for (int i = 0; i < Days[day][group].Tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Days[day][group].Tasks[i].Name}, x{Days[day][group].Tasks[i].Repetitions}");
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
