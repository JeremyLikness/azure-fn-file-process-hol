using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }
    }
}
