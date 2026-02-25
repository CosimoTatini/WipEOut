using UnityEngine;
using UnityEngine.SceneManagement;

public class Pond : MonoBehaviour
{
    public Transform spawnPoint;
    //[SerializeField] PlayerController player;
    //[SerializeField] GameTimer gameTimer;
    //[SerializeField] ResetPosition _resetPos;

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

