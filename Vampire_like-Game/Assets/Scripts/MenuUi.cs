using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] GameObject Credits;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Credits.SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
