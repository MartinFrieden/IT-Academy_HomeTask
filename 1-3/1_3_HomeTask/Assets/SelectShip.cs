using System.Collections.Generic;
using UnityEngine;

public class SelectShip : MonoBehaviour
{
    public List<GameObject> ships;
    int current;

    void Start()
    {
        ships[0].SetActive(true);
        current = 0;
    }

    public void Next()
    {
        try
        {
            ships[current].SetActive(false);
            ships[++current].SetActive(true);
        }
        catch(System.ArgumentOutOfRangeException)
        {
            current = 0;
            ships[current].SetActive(true);
            ships[ships.Count - 1].SetActive(false);
            
        }
    }

    public void Previous()
    {
        try
        {
            ships[current].SetActive(false);
            ships[--current].SetActive(true);
        }
        catch(System.ArgumentOutOfRangeException)
        {
            current = ships.Count - 1;
            ships[0].SetActive(false);
            ships[current].SetActive(true);
        }
    }

    public void ChangeColor(Texture color)
    {
        ships[current].GetComponent<MeshRenderer>().material.mainTexture = color;
    }
}
