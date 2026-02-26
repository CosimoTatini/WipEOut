using System.Collections;
using UnityEngine;

/// <summary>
/// On trigger enter, it stops the timer and show a TextMeshProUGUI text, and with a Coroutine sets the trigger collider on isTrigger=false
/// </summary>
public class FinshLine : MonoBehaviour
{
    public GameObject winnerText;
    private GameTimer _timer;
    public Collider coll;
    private float _disableTriggerDelay = 1f;

    private void Start()
    {
        _timer = FindFirstObjectByType<GameTimer>();
        winnerText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _timer.StopTimer();
            winnerText.SetActive(true);
            StartCoroutine(DisableTrigger());
        }
    }

    private IEnumerator DisableTrigger()
    {
        yield return new WaitForSeconds(_disableTriggerDelay);
        coll.isTrigger = false;
    }
}
