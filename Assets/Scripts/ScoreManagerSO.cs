using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scoresheet", menuName = "GameLoop/Scores")]
public class ScoreManagerSO : ScriptableObject
{
   Dictionary<string, int> Score = default;

   public void Add(string name, int score)
   {
      if (Score != null)
      {
         if (Score.Count < 10)
         {
            Score[name] = score;
         }
         else
         {
            int lowestScore = score;
            string lowestScoreBy = string.Empty;

            foreach (var entry in Score)
            {
               if (entry.Value < lowestScore)
               {
                  lowestScore = entry.Value;
                  lowestScoreBy = entry.Key;
               }
            }

            Score.Remove(lowestScoreBy);
            Score.Add(name, score);
         }
      }
   }
}
