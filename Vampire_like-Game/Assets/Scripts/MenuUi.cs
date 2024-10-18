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
            Credits.SetActive(false);
        }
        if(start && Input.anyKey)
        {
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

    public void Quit()
    {
        Application.Quit();
    }
}
