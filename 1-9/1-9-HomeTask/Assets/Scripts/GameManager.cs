using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public Button restartButton;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
    }

    public void showButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
