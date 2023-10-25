using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
    public GameObject IntroPanel;
    public GameObject TutorialPanel;
    private bool IntroActive = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Initialize the tutorial state
        TutorialPanel.SetActive(false);
        IntroPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    void Update()
    {
        if (IntroActive && Input.GetMouseButtonDown(0))
        {
            IntroPanel.SetActive(false);
            TutorialPanel.SetActive(true);

            // Unpause the game
            Time.timeScale = 0;

            IntroActive = false;
        }
    }
}
