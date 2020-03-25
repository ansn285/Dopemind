using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Canvas pauseMenu;

    public Canvas settings;
    public Canvas keyBindings;

    public AudioMixer audioMixer;

    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = GlobalControl.Instance.volume;
    }


    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.isActiveAndEnabled && !settings.isActiveAndEnabled && !keyBindings.isActiveAndEnabled)
        {
            Time.timeScale = 0;

            pauseMenu.gameObject.SetActive(true);


        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.isActiveAndEnabled && !settings.isActiveAndEnabled && !keyBindings.isActiveAndEnabled)
        {
            Time.timeScale = 1;

            pauseMenu.gameObject.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.isActiveAndEnabled && settings.isActiveAndEnabled && !keyBindings.isActiveAndEnabled)
        {
            settings.gameObject.SetActive(false);

            pauseMenu.gameObject.SetActive(true);

        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.isActiveAndEnabled && !settings.isActiveAndEnabled && keyBindings.isActiveAndEnabled)
        {
            keyBindings.gameObject.SetActive(false);

            settings.gameObject.SetActive(true);
        }

    }


    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;

        pauseMenu.gameObject.SetActive(false);        
    }

    public void InventoryButtonPressed()
    {
        gameObject.GetComponent<InventorySystem>().OpenInventory();


    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        GlobalControl.Instance.volume = volume;
    }

    public void QuitButtonPressed()
    {
        Application.Quit();

    }


}
