using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sceneManager = UnityEngine.SceneManagement.SceneManager;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip suspenseMusic;

    public AudioClip sereneMusic;

    public AudioClip genericMusic;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Dopermine") && GameObject.Find("Dopermine").GetComponent<FinalState>().isEnemy)
        {
            audioSource.Stop();
            Destroy(gameObject);
        }
        
    }

    public void ChangeAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;

        audioSource.Play();
    }

}
