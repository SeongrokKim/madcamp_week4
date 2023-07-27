using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("gameover");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // 'Main Menu' 버튼을 클릭하면 호출될 함수
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
