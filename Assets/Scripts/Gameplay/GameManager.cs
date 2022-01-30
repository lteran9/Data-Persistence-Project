using System.Collections;
using System.Collections.Generic;
using Unity.CreateWithCode.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity.CreateWithCode.DataPersistence
{
   public class GameManager : MonoBehaviour
   {
      [SerializeField] int LineCount = 6;
      [SerializeField] Rigidbody Ball = default;
      [SerializeField] Text ScoreText = default;
      [SerializeField] Brick BrickPrefab = default;
      [SerializeField] Text Header = default;
      [SerializeField] GameObject InitialText = default;
      [SerializeField] GameObject GameOverText = default;
      [SerializeField] ScoreManagerSO Scoresheet = default;

      private int m_Points = 0;
      private bool m_Started = false;
      private bool m_GameOver = false;
      public string m_UserName = string.Empty;

      public static GameManager Instance;

      private void Awake()
      {
         Instance = this;
      }

      // Start is called before the first frame update
      private void Start()
      {
         const float step = 0.6f;
         int perLine = Mathf.FloorToInt(4.0f / step);

         int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
         for (int i = 0; i < LineCount; ++i)
         {
            for (int x = 0; x < perLine; ++x)
            {
               Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
               var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
               brick.PointValue = pointCountArray[i];
               brick.onDestroyed.AddListener(AddPoint);
            }
         }

         Debug.Log(Scoresheet);

         m_UserName = Scoresheet.ActivePlayerName;

         var highestScore = Scoresheet.GetHighScore();
         Header.text = $"Best Score: {highestScore.PlayerName}: {highestScore.Score}";
      }

      private void Update()
      {
         if (!m_Started)
         {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               InitialText.SetActive(false);
               m_Started = true;
               float randomDirection = Random.Range(-1.0f, 1.0f);
               Vector3 forceDir = new Vector3(randomDirection, 1, 0);
               forceDir.Normalize();

               Ball.transform.SetParent(null);
               Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
         }
         else if (m_GameOver)
         {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
         }
      }

      private void AddPoint(int point)
      {
         m_Points += point;
         ScoreText.text = $"Score : {m_Points}";
      }

      public void GameOver()
      {
         m_GameOver = true;
         GameOverText.SetActive(true);
         Scoresheet.Add(m_UserName, m_Points);
      }
   }
}