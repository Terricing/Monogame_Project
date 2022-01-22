using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen
{
    class ScoreKeeper
    {
        private List<Score> scores;
        private const string FILE_PATH = "Content/scores.txt";
        private StreamWriter outFile;
        private StreamReader inFile;

        public ScoreKeeper()
        {
            scores = new List<Score> { };
            // if file does not exist, create it
            if (!File.Exists(FILE_PATH))
            {
                File.Create(FILE_PATH);
            }
            else
            {
                // if file exists, read it and obtain data
                string text;
                string [] data;
                inFile = File.OpenText(FILE_PATH);
                while ((text = inFile.ReadLine()) != null)
                {
                    data = text.Split(',');
                    scores.Add(new Score(data[0], Convert.ToInt32(data[1])));
                }
                inFile.Close();

            }
        }

        public void AddScore(string name, int score)
        {
            // cycle through the scores
            for (int i = 0; i < scores.Count; i++)
            {
                // if the score is higher, save it at that index
                if (score > scores[i].Val)
                {
                    scores.Insert(i, new Score(name, score));
                    return;
                }
            }

            scores.Add(new Score(name, score));
        }

        public void PerformWrite()
        {
            // open file
            outFile = new StreamWriter(FILE_PATH);
            foreach(Score score in scores)
            {
                outFile.WriteLine($"{score.Name},{score.Val}");
            }

            outFile.Close();
        }

        public string ObtainName(int score)
        {
            // perform binary search to obtain name
            int l = 0;
            int r = scores.Count - 1;
            int mid;

            while (l <= r)
            {
                mid = (l + r) / 2;

                if (score < scores[mid].Val)
                {
                    l = mid + 1;
                }
                else if (score > scores[mid].Val)
                {
                    r = mid - 1;
                }
                else
                {
                    return scores[mid].Name;
                }

            }
           

            // return empty value if not found
            return "";

        }

        public int ObtainScore(string name)
        {
            // perform linear search to obtain score

            foreach (Score score in scores)
            {
                if (score.Name.Equals(name))
                {
                    return score.Val;
                }
            }

            // if result is not found, return invalid value
            return -1;
        }


        public List<Score> Scores
        {
            get { return Scores; }
        }
    }

    class Score
    {
        private string name;
        private int score;

        public Score (string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get { return name; }
        }

        public int Val
        {
            get { return score; }
        }
    }
}
