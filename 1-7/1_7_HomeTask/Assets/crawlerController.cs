using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public AudioClip scream;
    Rigidbody rigid;
    bool attack;
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            rigid.AddForce(Vector3.right * 2, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asdasdasdasdas");
        animator.SetTrigger("Pounce");
        attack = true;
        audioSource.clip = scream;
        audioSource.Play();
        StartCoroutine(Delete());
    }


    IEnumerator Delete()
    {
        Debug.Log("delete");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
}
