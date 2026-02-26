using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// OnTriggerEnter, when the player gets in the pond, it comes back to the beginning of the level. SceneManager LoadScene reset the scene.
/// </summary>
public class Pond : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = spawnPoint.position;
            }
            else
            {
                other.transform.position = spawnPoint.position;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}

