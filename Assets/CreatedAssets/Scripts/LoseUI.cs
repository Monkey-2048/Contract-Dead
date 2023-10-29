using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReloadCurrentScene();
        }
    }

    public void ReloadCurrentScene()
    {
        string currentSceneIndex = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
