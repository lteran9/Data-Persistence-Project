using System.Collections;
using System.Collections.Generic;
using Unity.CreateWithCode.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity.CreateWithCode.DataPersistence
{
   // Sets the script to be executed later than all default scripts
   // This is helpful for UI, since other things may need to be initialized before setting the UI
   [DefaultExecutionOrder(1000)]
   public class MenuUIHandler : MonoBehaviour
   {
      [SerializeField] private InputField _name = default;
      [SerializeField] private ScoreManagerSO _scoresheet = default;

      public void StartNew()
      {
         if (!string.IsNullOrEmpty(_name.text))
         {
            _scoresheet.ActivePlayerName = _name.text;
            SceneManager.LoadScene(1);
         }
      }

      public void Exit()
      {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#endif

         Application.Quit();
      }
   }
}