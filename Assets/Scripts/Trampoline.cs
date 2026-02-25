using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float bounceForce = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.linearVelocity = velocity;

            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}




