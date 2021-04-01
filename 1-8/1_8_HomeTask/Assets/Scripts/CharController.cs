using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    public Camera charCamera;
    public float offset;
    public float followSpeed;
    AudioSource audioSource;
    public AudioClip idle;
    public AudioClip run;
    public GameObject back;
    public float scaleSpeed;
    public float maxScale;
    float startScale;

    // Start is called before the first frame update
    void Start()
    {
        back.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        if (charCamera != null)
        {
            charCamera.transform.position = new Vector3(this.gameObject.transform.position.x, charCamera.transform.position.y, charCamera.transform.position.z);
            startScale = charCamera.orthographicSize;
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (charCamera != null)
        {
            Vector3 newPosition = charCamera.transform.position;
            float newScale = charCamera.orthographicSize;
            if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2)
            {
                newScale = Mathf.Lerp(newScale, maxScale, scaleSpeed);
                newPosition.x = Mathf.Lerp(newPosition.x, this.gameObject.transform.position.x + offset, followSpeed);  
                
                charCamera.transform.position = newPosition;
                charCamera.orthographicSize = newScale;
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
            {
                newScale = Mathf.Lerp(newScale, maxScale, scaleSpeed);
                newPosition.x = Mathf.Lerp(newPosition.x, this.gameObject.transform.position.x - offset, followSpeed);

                charCamera.transform.position = newPosition;
                charCamera.orthographicSize = newScale;
            }
            else
            {
                newScale = Mathf.Lerp(newScale, startScale, scaleSpeed);
                newPosition.x = Mathf.Lerp(newPosition.x, this.gameObject.transform.position.x, followSpeed);

                charCamera.transform.position = newPosition;
                charCamera.orthographicSize = newScale;
            }
        }

        if (Input.touchCount > 0)
        {
            if (!back.active)
                back.SetActive(true);
            anim.SetBool("isRun", true);


            if (audioSource.enabled == true)
            {
                audioSource.clip = run;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }


            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width/2 && !isFacingRight)
            {
                Flip();    
            }
            else if (touch.position.x < Screen.width/2 && isFacingRight)
            {
                Flip();
            }     
        }
        else
        {
            if (back.active)
                back.SetActive(false);
            anim.SetBool("isRun", false);

            if (audioSource.enabled == true)
            {
                audioSource.clip = idle;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
