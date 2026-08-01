using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
