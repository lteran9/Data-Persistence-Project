using System.Collections;
using System.Collections.Generic;
using Unity.CreateWithCode.ScriptableObjects;
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
      [SerializeField] InputField Name = default;
      [SerializeField] ScoreManagerSO Scoresheet = default;

      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }

      public void StartNew()
      {
         if (!string.IsNullOrEmpty(Name.text))
         {
            Scoresheet.ActivePlayerName = Name.text;
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