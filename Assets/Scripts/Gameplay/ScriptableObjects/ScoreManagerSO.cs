using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Unity.CreateWithCode.Gameplay;

namespace Unity.CreateWithCode.Gameplay.ScriptableObjects
{
   [CreateAssetMenu(fileName = "Scoresheet", menuName = "GameLoop/Scores")]
   public class ScoreManagerSO : ScriptableObject
   {
      public string ActivePlayerName = default;

      public List<Record> Scores = default;

      public void Add(string name, int score)
      {
         if (Scores != null)
         {
            Scores.Add(new Record(score, name));

            if (Scores.Count > 10)
            {
               // Get Lowest Score
               int lowestScore = int.MaxValue;
               int lowestScoreIdx = 0;

               for (int i = 0; i < Scores.Count; i++)
               {
                  if (Scores[i].Score < lowestScore)
                  {
                     lowestScoreIdx = i;
                     lowestScore = Scores[i].Score;
                  }
               }

               // Remove Lowest Score
               Scores.RemoveAt(lowestScoreIdx);
            }
         }
      }

      public Record GetHighScore()
      {
         if (Scores.Count > 0)
         {
            int highest = int.MinValue;
            Record result = Scores[0];

            foreach (var record in Scores)
            {
               if (record.Score > highest)
               {
                  result = record;
                  highest = record.Score;
               }
            }

            // Never return lower than 0
            return result;
         }
         // Can't return null struct
         return new Record(0, string.Empty);
      }

      public override string ToString()
      {
         var sb = new StringBuilder();

         foreach (var entry in Scores)
         {
            sb.Append($"[{entry.PlayerName}: {entry.Score}] | ");
         }

         if (sb.Length > 3)
         {
            // Remove the last entry of empty space and |
            return sb.ToString().Substring(0, sb.Length - 3);
         }

         return string.Empty;
      }
   }
}