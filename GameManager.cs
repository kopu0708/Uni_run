using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;
    public Text scoreText;
    public Text gameoverUI;

    private int score = 0;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver && Input.GetKeyDown(KeyCode.R))
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void AddScore(int newscore)
    {
      if(!isGameOver)
        {
            score += newscore;
            scoreText.text = "Score: " + score;
        }
    }

    public void OnPlayerDead()
    {
        isGameOver = true;
        gameoverUI.gameObject.SetActive(true);
    }
}