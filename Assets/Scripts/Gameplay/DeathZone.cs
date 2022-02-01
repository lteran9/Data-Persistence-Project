using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CreateWithCode.Gameplay
{
   public class DeathZone : MonoBehaviour
   {
      private void OnCollisionEnter(Collision other)
      {
         Destroy(other.gameObject);

         GameManager.Instance.GameOver();
      }
   }
}