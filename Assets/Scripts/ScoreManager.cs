using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int collectedCount = 0;
    public int totalCount = 15;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Rings: " + collectedCount + " / " + totalCount;
        if (collectedCount >= totalCount)
        {
            winText.gameObject.SetActive(true);
        }
        else
        {
            winText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedCount++;
            UpdateScore();

            if (collectedCount >= totalCount)
            {
                Debug.Log("You collected all the rings!");
                // Additional actions when all collectibles are collected
            }

            Destroy(gameObject);
        }
    }
}
