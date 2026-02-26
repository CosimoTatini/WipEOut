using UnityEngine;

public class HeavyShot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Collided with:{gameObject.name}");
            float impact = GetComponent<Rigidbody>().linearVelocity.magnitude;
           
            Debug.Log(impact);

            if(impact >=0)
            {
              collision.rigidbody.AddForce(transform.forward * 10f,ForceMode.Impulse);
            }
        }
    }

    //private Rigidbody rb;
    //[SerializeField] Rigidbody playerRigidbody;
    //private float impact;
    //private float impulseForce = 10f;
    //private Rigidbody rb;
    //[SerializeField] Rigidbody playerRigidbody;
    //private float impact;
    //private float impulseForce = 10f;

    //private void Start()
    //{
    //    rb= GetComponentInParent<Rigidbody>();
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        impact=rb.angularVelocity.magnitude;

    //        if(impact>0)
    //        {
    //            playerRigidbody = other.attachedRigidbody;

    //            if(playerRigidbody!=null)
    //            {
    //                playerRigidbody.AddForce(-transform.forward * impulseForce,ForceMode.Impulse);
    //            }
    //        }
    //    }
    //}
}
