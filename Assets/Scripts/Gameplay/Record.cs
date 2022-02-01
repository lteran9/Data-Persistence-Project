using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CreateWithCode.Gameplay
{
   [System.Serializable]
   public struct Record
   {
      public int Score { get; set; }
      public string PlayerName { get; set; }

      public Record(int score, string name)
      {
         this.Score = score;
         this.PlayerName = name;
      }
   }
}