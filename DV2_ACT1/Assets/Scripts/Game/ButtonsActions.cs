using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


///<summary>Button actions class.</summary>
[RequireComponent(typeof(AudioSource))]
public class ButtonsActions : MonoBehaviour
{
    private AudioClip _clickAudioClip;
    private AudioSource _audioSource;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        _audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _clickAudioClip = (AudioClip) Resources.Load("Sounds/clickAudioClip");
        _audioSource.clip = _clickAudioClip;
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    ///<summary>Action Menu button</summary>
    public void InitMenu()
    {
        _audioSource.Play();
        StartCoroutine(waitSfx("InitMenu"));
    }

    ///<summary>Action Play button</summary>
    public void PlayGame()
    {
        _audioSource.Play();
        StartCoroutine(waitSfx("LevelsBase"));
    }

    ///Action restart button.</summary>
    public void ReStartGame()
    {
        _audioSource.Play();
        StartCoroutine(waitSfx("InitMenu"));
    }

    ///Action Exit button (Salir).</summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    ///<summary>Until finish sound.</summary>
    IEnumerator waitSfx(string scene)
    {
        float volume = 1f;
        while (volume > 0f)
        {
            _audioSource.volume = volume;
            volume -= 0.1f;
            yield return new WaitForSecondsRealtime(_clickAudioClip.length/10);
            
        }
        SceneManager.LoadScene(scene);
    }

}
