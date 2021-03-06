using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CreateWithCode.DataPersistence
{
   public class DeathZone : MonoBehaviour
   {
      [SerializeField] GameManager Manager;

      private void OnCollisionEnter(Collision other)
      {
         Destroy(other.gameObject);
         Manager.GameOver();
      }
   }
}