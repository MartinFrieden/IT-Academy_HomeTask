using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public List<RectTransform> windows;
    public Text score;
    public Text finalScore;
    int scoreInt;
    void Start()
    {
        scoreInt = 0;
        if(score != null)
            score.text = scoreInt.ToString();
    }

    void Update()
    {
        
    }

    public void SetActiveMenu(RectTransform targetMenu)
    {
        foreach (var item in windows)
        {
            if (targetMenu == item)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    public void AddScore(int addScore)
    {
        scoreInt += addScore;
        score.text = scoreInt.ToString();
    }

    public void FinalScoreCount()
    {
        foreach(GameObject item in GameManager.instance.birds)
        {
            CommonBird cobBird = item.GetComponent<CommonBird>();
            scoreInt += cobBird.scoreBird;
        }
        finalScore.text = scoreInt.ToString();
    }

}
