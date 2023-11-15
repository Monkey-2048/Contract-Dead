using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Transform PausePanel;
    private bool isPaused;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (!IntroUI.isActive() && !TurorialUI.isActive() && !WinLoseUI.LoseActive && !WinLoseUI.WinActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }

            if (isPaused && Input.GetKeyDown(KeyCode.Return))
            {
                ReturnToMainMenu();
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // Pause the game
            EnablePause();// Show the pause menu
        }
        else
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        DisablePause(); // Hide the pause menu
    }

    void ReturnToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f; // Ensure time scale is normal before loading main menu
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }

    private void EnablePause()
    {
        PausePanel.gameObject.SetActive(true);
    }
    
    private void DisablePause()
    {
        PausePanel.gameObject.SetActive(false);
    }

}
