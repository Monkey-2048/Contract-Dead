using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;

    private bool tutorialActive = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Initialize the tutorial state
        tutorialPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    void Update()
    {
        if (tutorialActive && Input.GetMouseButtonDown(0))
        {
            // Hide the tutorial panel
            tutorialPanel.SetActive(false);

            // Unpause the game
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            tutorialActive = false;
        }
    }
}
