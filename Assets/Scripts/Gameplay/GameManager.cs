using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.CreateWithCode.Gameplay.ScriptableObjects;

namespace Unity.CreateWithCode.Gameplay
{
   public class GameManager : MonoBehaviour
   {
      public static GameManager Instance { get; private set; }

      [SerializeField] private int _lineCount = 6;
      [SerializeField] private Rigidbody _ball = default;
      [SerializeField] private Text _scoreText = default;
      [SerializeField] private Brick _brickPrefab = default;
      [SerializeField] private Text _header = default;
      [SerializeField] private GameObject _initialText = default;
      [SerializeField] private GameObject _gameOverText = default;
      [SerializeField] private ScoreManagerSO _scoresheet = default;

      private int m_Points = 0;
      private bool m_Started = false;
      private bool m_GameOver = false;
      public string m_UserName = string.Empty;

      private void Awake()
      {
         if (Instance != null)
         {
            Destroy(Instance);
         }

         Instance = this;
      }

      // Start is called before the first frame update
      private void Start()
      {
         const float step = 0.6f;
         int perLine = Mathf.FloorToInt(4.0f / step);

         int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
         for (int i = 0; i < _lineCount; ++i)
         {
            for (int x = 0; x < perLine; ++x)
            {
               Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
               var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
               brick.SetPointValue(pointCountArray[i]);
               brick.onDestroyed.AddListener(AddPoint);
            }
         }

         Debug.Log(_scoresheet);

         m_UserName = _scoresheet.ActivePlayerName;

         var highestScore = _scoresheet.GetHighScore();
         _header.text = $"Best Score: {highestScore.PlayerName}: {highestScore.Score}";
      }

      private void Update()
      {
         if (!m_Started)
         {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               _initialText.SetActive(false);
               m_Started = true;
               float randomDirection = Random.Range(-1.0f, 1.0f);
               Vector3 forceDir = new Vector3(randomDirection, 1, 0);
               forceDir.Normalize();

               _ball.transform.SetParent(null);
               _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
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
         _scoreText.text = $"Score : {m_Points}";
      }

      public void GameOver()
      {
         m_GameOver = true;
         _gameOverText.SetActive(true);
         _scoresheet.Add(m_UserName, m_Points);
      }
   }
}