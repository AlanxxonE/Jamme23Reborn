using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject volumeChecker;

    private bool disableVolume = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void ManageVolume()
    {
        disableVolume = !disableVolume;

        if(disableVolume)
        {
            AudioListener.volume = 0;

            if(volumeChecker != null && !volumeChecker.activeSelf)
            {
                volumeChecker.SetActive(true);
            }
        }
        else
        {
            AudioListener.volume = 100;

            if (volumeChecker != null && volumeChecker.activeSelf)
            {
                volumeChecker.SetActive(false);
            }
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
