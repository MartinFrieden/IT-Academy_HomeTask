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
        ships[current].SetActive(false);
        current = (int)Mathf.Repeat(++current, ships.Count);
        ships[current].SetActive(true);
    }

    public void Previous()
    {
        ships[current].SetActive(false);
        current = (int)Mathf.Repeat(--current, ships.Count);
        ships[current].SetActive(true);
    }

    public void ChangeColor(Texture color)
    {
        ships[current].GetComponent<MeshRenderer>().material.mainTexture = color;
    }
}
