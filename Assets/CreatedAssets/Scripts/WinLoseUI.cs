using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private Transform WinUI;
    [SerializeField] private Transform LoseUI;

    // Start is called before the first frame update
    void Start()
    {
        WinUI.gameObject.SetActive(false);
        LoseUI.gameObject.SetActive(false);
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
    }

    private void PlayerHealth_OnPlayerDeath(object sender, System.EventArgs e)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoseUI.gameObject.SetActive(true);
    }

    private void BossHealth_OnBossDeath(object sender, System.EventArgs e)
    {
        Invoke("ActivateWin", 8f);
    }

    private void ActivateWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        WinUI.gameObject.SetActive(true);
    }

}