﻿using Newtonsoft.Json;
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

        public void SaveScoreToFile(int highScore)
        {

            save.highScore = highScore;
            string json = JsonConvert.SerializeObject(save);


            File.WriteAllText("data/Save.txt", json);
        }

        public void SaveStatsToFile(int enemiesKilled, float timeAlive)
        {
            save.enemiesKilled += enemiesKilled;
            save.timeAlive += timeAlive;
            string json = JsonConvert.SerializeObject(save);

            File.WriteAllText("data/Save.txt", json);
        }

        /// <summary>
        /// Hakee tiedostosta tallenetut tiedot. Mikäli tallenusta ei löydy, luodaan uusi.
        /// </summary>
        public void LoadFromFile()
        {
            if (!File.Exists("data/Save.txt"))
            {
                SaveScoreToFile(0);
            }
            string json = File.ReadAllText("data/Save.txt");
            save = JsonConvert.DeserializeObject<Save>(json);
        }

        public int GetHighScore()
        {
            return save.highScore;
        }

        public float GetTimeAlive()
        {
            return save.timeAlive;
        }

        public int GetEnemiesKilled()
        {
            return save.enemiesKilled;
        }

    }
}
