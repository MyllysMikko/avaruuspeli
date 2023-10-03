using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class SaveManager
    {
        Save save = new Save();


        public void SaveToFile(int highScore)
        {

            save.highScore = highScore;
            string json = JsonConvert.SerializeObject(save);


            File.WriteAllText("data/Save.txt", json);
            Console.WriteLine(json);
        }

        public void LoadFromFile()
        {
            if (!File.Exists("data/Save.txt"))
            {
                SaveToFile(0);
            }
            string json = File.ReadAllText("data/Save.txt");
            save = JsonConvert.DeserializeObject<Save>(json);
            Console.WriteLine(save.highScore);
        }

        public int GetHighScore()
        {
            return save.highScore;
        }

    }
}
