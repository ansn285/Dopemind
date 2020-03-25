using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public Canvas settings;
    public Canvas keyBindings;

    public AudioMixer audioMixer;

    public Slider volumeSlider;

    private void Awake()
    {
        volumeSlider.value = GlobalControl.Instance.volume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settings.isActiveAndEnabled && !keyBindings.isActiveAndEnabled)
        {

            GameObject.Find("Settings").SetActive(false);

        }

        if (Input.GetKey(KeyCode.Escape) && keyBindings.isActiveAndEnabled)
        {

            GameObject.Find("KeyBindings").SetActive(false);

        }

        


    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        GlobalControl.Instance.volume = volume;
    }

}
