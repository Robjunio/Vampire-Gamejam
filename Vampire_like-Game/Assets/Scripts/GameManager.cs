using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void TimeElapsed(int currentHour);
    public static event TimeElapsed HourPassed;
    public static event TimeElapsed LastHour;

    public float GameTime;

    [SerializeField] float counter;
    int currentHour;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnHourPassed(0);
    }

    private void OnHourPassed(int currentHour)
    {
        if (currentHour == 9)
        {
            LastHour?.Invoke(currentHour);
        }
        HourPassed?.Invoke(currentHour);
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if(Mathf.FloorToInt(counter / 60f) > currentHour)
        {
            currentHour++;
            OnHourPassed(currentHour);
        }
    }

    public float GetCurrentTime()
    {
        return counter;
    }
}
