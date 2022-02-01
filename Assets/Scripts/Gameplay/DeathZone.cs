using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CreateWithCode.Gameplay
{
   public class DeathZone : MonoBehaviour
   {
      [SerializeField] private GameManager _manager;

      private void OnCollisionEnter(Collision other)
      {
         Destroy(other.gameObject);
         _manager.GameOver();
      }
   }
}