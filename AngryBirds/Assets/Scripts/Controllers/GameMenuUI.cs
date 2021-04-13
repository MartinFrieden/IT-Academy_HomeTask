using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    public GameObject endButton; //кнопка закрытия уровня
    public int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        finalScore = 0;
        endButton.gameObject.SetActive(false);
    }

    //показать кнопку закрытия уровня
    public void ShowButton()
    {
        endButton.gameObject.SetActive(true);
    }

}
