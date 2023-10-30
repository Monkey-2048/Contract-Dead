using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        string currentSceneIndex = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadContractOne()
    {
        SceneManager.LoadScene("Scene_A");
    }
    
    public void LoadContractTwo()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void LoadContractAll()
    {
        SceneManager.LoadScene("ContractsAll");
    }

    public void LoadContractLocked()
    {
        SceneManager.LoadScene("ContractsLocked");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
