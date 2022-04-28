using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class MuscleGroup
    {
        public string Name { get; set; }
        public List<TrainingTask> Tasks { get; set; }
        public MuscleGroup(string name)
        {
            Name = name;
            Tasks = new List<TrainingTask>();
        }
    }
}
