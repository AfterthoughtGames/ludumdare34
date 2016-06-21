using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class ScoreSystem
    {
        public static void SaveScore(int value)
        {
            if (ReadScore() < value)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\score.txt")) File.Delete(Directory.GetCurrentDirectory() + "\\score.txt");
                System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\score.txt", value.ToString(), System.Text.Encoding.ASCII);
            }
        }

        public static int ReadScore()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\score.txt"))
            {
                String scoreText = string.Empty;

                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\score.txt"))
                {
                    scoreText = sr.ReadToEnd();
                }

                return Convert.ToInt32(scoreText);
            }

            return 0;
        }
    }
}
