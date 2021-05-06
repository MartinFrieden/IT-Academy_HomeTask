using UnityEngine;

public class GranateController : MonoBehaviour
{
    public ParticleSystem particleBang;
    //сила взрыва
    public float explosionForce;
    public void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaySound(AudioManager.Sounds.explosion, gameObject.transform.position);
        particleBang.Play(true);
        //взрыв
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10f);
        foreach (Collider hit in colliders)
        {   
            //исключаем влияние взрыва на саму гранату
            if (!hit.CompareTag("Granate") && !hit.CompareTag("Player"))
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, this.transform.position, 10, 1, ForceMode.Impulse);
            }
        }
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        particleBang.Stop(true);
    }
}
