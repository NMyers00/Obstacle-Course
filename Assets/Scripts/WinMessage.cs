using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinMessage : MonoBehaviour
{
    public Text winText; // Reference to the Text UI component

    private void Start()
    {
        winText.enabled = false; // Hide the text initially
    }

    public void ShowWinMessage()
    {
        winText.enabled = true; // Show the win message
        winText.text = "Congrats! You've Collected All Rings!!!"; // Customize the win message text
    }
}