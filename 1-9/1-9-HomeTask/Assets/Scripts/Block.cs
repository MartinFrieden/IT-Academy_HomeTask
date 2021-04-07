using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float bounceheight = 0.5f;
    public float bounceSpeed = 4f;
    private Vector2 originalPosition;

    public float coinMoveSpeed = 8f;
    public float coinMoveHeight = 3f;
    public float coinFalldistance = 2f;

    private bool canBounce = true;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PresentCoin()
    {
        GameObject spinningCoin = (GameObject)Instantiate(Resources.Load("coinPrefab", typeof(GameObject)));
        spinningCoin.transform.SetParent(this.transform.parent);
        spinningCoin.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y + 1);
        StartCoroutine(MoveCoin(spinningCoin));
    }

    public void BlockBounce()
    {
        if (canBounce)
        {
            canBounce = false;
            StartCoroutine(Bounce());
        }
    }

    IEnumerator Bounce()
    {
        PresentCoin();
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y+bounceheight)
            {
                break;
            }
            yield return null;
        }
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y <= originalPosition.y)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }
    IEnumerator MoveCoin(GameObject coin)
    {
        while(true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinMoveSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= originalPosition.y+coinMoveHeight+1)
            {
                break;
            }
            yield return null;
        }

        while(true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinMoveSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= originalPosition.y + coinFalldistance + 1);
            {
                Destroy(coin.gameObject);
                break;
            }
            yield return null;
        }
    }
}
