using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static bool contractDefeated;

    private void Start()
    {
        Instance = this;
    }

    public void ReloadCurrentScene()
    {
        string currentSceneIndex = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadContractOne()
    {
        SceneManager.LoadScene("FloodedGrounds");
    }
    
    public void LoadContractTwo()
    {
        SceneManager.LoadScene("SunTemple");
    }

    public void LoadContraacts()
    {
        if (contractDefeated)
        {
            // Load the "ContractsAll" scene
            SceneManager.LoadScene("ContractsAll");
        }
        else
        {
            // Load the "ContractsLocked" scene
            SceneManager.LoadScene("ContractsLocked");
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
