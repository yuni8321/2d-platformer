using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    static GameManager _instance;

    // �̱���(Singleton) ����
    // ��ü�� �� ���� 2�� �̻� ������ �ʰ� �����ϴ� ����

    public static GameManager Instance
    {
        // ������Ƽ(Property)
        get
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // �ߺ��� ���ӸŴ��� ����
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        // ���ӿ��� �̺�Ʈ
        Debug.Log("���ӿ���");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */

    static GameManager _instance;

    public enum GameState
    {
        Running = 0,
        Paused = 1
    }

    public static GameManager Instance
    {
        get
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public GameState State
    {
        get
        {
            return state;
        }
    }

    inGameHUD hud;
    [SerializeField] GameState state;

    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        hud = FindObjectOfType<inGameHUD>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Running)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        state = GameState.Running;
        hud.ClosePauseMenu();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        state = GameState.Paused;
        hud.OpenPauseMenu();
        Time.timeScale = 0f;
    }

    public void OpenMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
