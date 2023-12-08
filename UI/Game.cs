
namespace UI
{
    public class Game
    {
        public string Name { get; set; }
        public string ProcessName { get; set; }
        public string Offsets { get; set; }
        public Game(string name, string processName, string offsets) 
        { 
            Name = name;
            ProcessName = processName;
            Offsets = offsets;
        }   
    }
}
