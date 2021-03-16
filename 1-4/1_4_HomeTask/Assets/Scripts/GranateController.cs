using UnityEngine;

public class GranateController : MonoBehaviour
{
    //вспышка от взрыва
    Light lightBang;
    //сила взрыва
    public float explosionForce;

    void Start()
    {
        lightBang = this.GetComponent<Light>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //вспышка от взрыва
        lightBang.enabled = true;
        //взрыв
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10f);
        foreach (Collider hit in colliders)
        {
            //исключаем влияние взрыва на саму гранату
            if (hit.tag != "Granate")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, this.transform.position, 10, 1, ForceMode.Impulse);
            }
        }
        Destroy(this.gameObject, 0.05f);
    }
}
