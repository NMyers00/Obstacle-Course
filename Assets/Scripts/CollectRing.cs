using System;
using UnityEngine;

public class CollectRing : MonoBehaviour
{
    public static event Action OnCollected;
    public ScoreManager scoreManager; // Reference to the ScoreManager script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.collectedCount++;
            scoreManager.UpdateScore();

            if (scoreManager.collectedCount >= scoreManager.totalCount)
            {
                // All collectibles have been collected, perform any necessary actions
            }

            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}