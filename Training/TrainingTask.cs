using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class TrainingTask
    {
        public string Name { get; set; }
        public int Repetitions { get; set; }
        public TrainingTask(string name, int repetitions)
        {
            Name = name;
            Repetitions = repetitions;
        }
    }
}
