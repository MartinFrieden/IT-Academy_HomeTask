using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//общее поведение для всех птичек
public class CommonBird : MonoBehaviour
{
    [Header("Common Parametres")]
    public float delayBeforeRemoving;
    public int scoreBird = 100;

    Rigidbody2D _rigid;
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_rigid.IsSleeping())
        {
            StartCoroutine(DestroyBird());
        }
    }

    IEnumerator DestroyBird()
    {
        yield return new WaitForSeconds(delayBeforeRemoving);
        Destroy(gameObject);
    }
}
