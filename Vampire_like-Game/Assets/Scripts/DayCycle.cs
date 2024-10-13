using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    [SerializeField] Gradient Gradient;
    float durationInSeconds;

    Light2D renderer;

    public float percentage = 0;



    // Start is called before the first frame update
    void Start()
    {
        durationInSeconds = GameManager.Instance.GameTime;
        TryGetComponent(out renderer);
    }

    // Update is called once per frame
    void Update()
    {
        percentage = GameManager.Instance.GetCurrentTime() / durationInSeconds;

        percentage = Mathf.Clamp01(percentage);

        renderer.color = Gradient.Evaluate(percentage);
    }
}
