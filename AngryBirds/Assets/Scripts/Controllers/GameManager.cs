using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent endLevel;

    public bool isPaused = false;
    public CircleCollider2D slingshotColl;

    [Header("BirdsParams")]
    public List<GameObject> birds;          //птички для текущего уровня
    public float offset = 1f;               //расстояние смещения птичек
    public Transform spawnPoint;           //место спауна

    [Header("CastleParams")]
    public GameObject castle;
    public Transform castleSpawnPoint;
    public List<GameObject> pigs;

    void Awake()
    {
        SetBirds();
        if(castle != null)
            SetCastle();
    }

    void Update()
    {
        
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            slingshotColl.enabled = false;
            Time.timeScale = 0;
        }
        else
        { 
            slingshotColl.enabled = true;
            Time.timeScale = 1;
        }
        isPaused = pause; //для других классов
    }

    //перезапуск сцены
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    //размещение птичек
    void SetBirds()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i] = Instantiate(birds[i], this.gameObject.transform);
            birds[i].transform.position = new Vector2(spawnPoint.position.x - offset, spawnPoint.position.y);
            offset += 0.8f;
        }
    }

    void SetCastle()
    {
        castle = Instantiate(castle, this.gameObject.transform);
        castle.transform.position = castleSpawnPoint.position;
        GameObject[] pigsOnScene = GameObject.FindGameObjectsWithTag("Pig");
        foreach (GameObject item in pigsOnScene)
        {
            pigs.Add(item);
        }
    }

    public void PigsWatcher()
    {
        if (pigs.Count == 0)
        {
            Debug.Log("ПОБЕДА");
            endLevel?.Invoke();
        }
    }

    public void LoadNextScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
