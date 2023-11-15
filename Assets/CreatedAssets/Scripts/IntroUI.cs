using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
    public GameObject IntroPanel;
    public GameObject TutorialPanel;
    private static bool IntroActive = true;

    void Start()
    {
        IntroActive = true;
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


            Time.timeScale = 0;

            IntroActive = false;
        }
    }

    

    public static bool isActive()
    {
        return IntroActive;
    }

}
