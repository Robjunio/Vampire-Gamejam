using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] GameObject Credits;
    private Animator _animator;
    [SerializeField] Animator animator;
    bool start;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ExitButtonSound();
            Credits.SetActive(false);
        }
        if(start && Input.anyKey)
        {
            EnterButtonSound();
            StartGame();
        }
    }
    public void EndAnimation()
    {
        start = true;
        animator.Play("End");
    }

    public void StartGameAnim()
    {
        _animator.Play("ExitMenu");
    }

    public void StartGame()
    {

        SceneManager.LoadScene("Game");
    }

    public void EnterButtonSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIButtonIn, Vector3.zero);
    }

    public void ExitButtonSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.UIButtonOut, Vector3.zero);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
