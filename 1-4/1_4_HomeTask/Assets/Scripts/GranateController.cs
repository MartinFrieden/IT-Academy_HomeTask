using UnityEngine;

public class GranateController : MonoBehaviour
{
    public GameObject particleBang;
    //сила взрыва
    public float explosionForce;

    void Start()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(particleBang, this.gameObject.transform.position, Quaternion.identity);
        //взрыв
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10f);
        foreach (Collider hit in colliders)
        {
            //исключаем влияние взрыва на саму гранату
            if (hit.tag != "Granate" || hit.tag != "Player")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, this.transform.position, 10, 1, ForceMode.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
}
