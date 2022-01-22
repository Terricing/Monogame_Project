// Author: Eilay Katsnelson
// File Name: ScoreKeeper.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Controls the scores and the score file

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
        // store path to file
        private const string FILE_PATH = "Content/scores.txt";

        // Store scores
        private List<Score> scores;

        // file readers and writers
        private StreamWriter outFile;
        private StreamReader inFile;

        /// <summary>
        /// Create ScoreKeepr object
        /// </summary>
        public ScoreKeeper()
        {
            // Obtain scores from file
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

        /// <summary>
        /// Add score to memory
        /// </summary>
        /// <param name="name">name of player</param>
        /// <param name="score">player's score</param>
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
            // Save score at the end
            scores.Add(new Score(name, score));
        }

        /// <summary>
        /// Write the updated scores from memory to file
        /// </summary>
        public void PerformWrite()
        {
            // open file
            outFile = new StreamWriter(FILE_PATH);

            // Write scores to file
            foreach(Score score in scores)
            {
                outFile.WriteLine($"{score.Name},{score.Val}");
            }

            outFile.Close();
        }

        /// <summary>
        /// Find name from score
        /// </summary>
        /// <param name="score">score to be searched</param>
        /// <returns>
        /// name of player, if found
        /// "" if no player found
        /// </returns>
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

        /// <summary>
        /// Get score from name
        /// </summary>
        /// <param name="name">name of player</param>
        /// <returns>
        /// integer of score, if score found
        /// -1 if score not found
        /// </returns>
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

        /// <summary>
        /// Accessor for scores
        /// </summary>
        public List<Score> Scores
        {
            get { return Scores; }
        }
    }

    // Data class to work with scores
    class Score
    {
        // Store player's name
        private string name;

        // Store player's score
        private int score;

        /// <summary>
        ///  Create score object
        /// </summary>
        /// <param name="name">player's name</param>
        /// <param name="score">player's score</param>
        public Score (string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        /// <summary>
        /// Accessor for anme
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Acessor for score value
        /// </summary>
        public int Val
        {
            get { return score; }
        }
    }
}
