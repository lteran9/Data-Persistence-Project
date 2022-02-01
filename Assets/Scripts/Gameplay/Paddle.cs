using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CreateWithCode.Gameplay
{
   public class Paddle : MonoBehaviour
   {
      [SerializeField] private float _speed = 2.0f;
      [SerializeField] private float _maxMovement = 2.0f;

      // Update is called once per frame
      private void Update()
      {
         float input = Input.GetAxis("Horizontal");

         Vector3 pos = transform.position;
         pos.x += input * _speed * Time.deltaTime;

         if (pos.x > _maxMovement)
            pos.x = _maxMovement;
         else if (pos.x < -_maxMovement)
            pos.x = -_maxMovement;

         transform.position = pos;
      }
   }
}