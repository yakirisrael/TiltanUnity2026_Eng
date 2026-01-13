using System;
using UnityEditor;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateSouls();
    }

    private void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            TogglePauseMenu(true);
        }
    }

    // Update is called once per frame
    public void TogglePauseMenu(bool pause)
    {
        if (pause)
        {
            HUD.SetActive(false);
            PauseMenu.SetActive(true);
            
            Time.timeScale = 0;
        }
        else
        {
            HUD.SetActive(true);
            PauseMenu.SetActive(false);

            Time.timeScale = 1;
        }
    }

    public void UpdateHealth(int HP, int MaxHP)
    {
        HUD.GetComponent<HUD>().UpdateHealth(HP, MaxHP);
    }

    public void UpdateSouls()
    {
        HUD.GetComponent<HUD>().UpdateSoulsWidth();
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
       // Application.Quit();
    }
}
