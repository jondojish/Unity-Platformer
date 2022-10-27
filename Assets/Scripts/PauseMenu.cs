using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    private GameObject[] PauseMenuObjects;

    private void Awake()
    {
        PauseMenuObjects = GameObject.FindGameObjectsWithTag("PauseMenuObject");
        hideMenu();
    }

    public void Resume()
    {
        hideMenu();
        Time.timeScale = 1;
        _isPaused = false;
    }

    public void Pause()
    {
        showMenu();
        Time.timeScale = 0;
        _isPaused = true;
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // GetKeyDown happens once even if held down
        {
            if (!_isPaused)
                Pause();
            else
                Resume();
        }
    }



    private void showMenu()
    {
        foreach (GameObject pauseMenuObject in PauseMenuObjects)
        {
            pauseMenuObject.SetActive(true);
        }
    }

    public void hideMenu()
    {
        foreach (GameObject pauseMenuObject in PauseMenuObjects)
        {
            pauseMenuObject.SetActive(false);
        }
    }

}
