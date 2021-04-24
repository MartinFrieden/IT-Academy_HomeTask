using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject startBullet;
    public GameObject endBullet;

    private void Start()
    {
        Instantiate(startBullet, this.gameObject.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(endBullet, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
