 using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 

    public enum Panels
    {
        Pause,
        Victory,
        Defeat,
        LevelUp
    }

    [SerializeField] GameObject pauseTutorial;

    [Serializable]
    public class PanelRef
    {
        public Panels panel;
        public GameObject panelObj;
    }

    [SerializeField] GameObject basePanel;
    [SerializeField] PanelRef[] panelsRef;

    [SerializeField] Animator hit;
    [SerializeField] GameObject CautionWarning;

    [SerializeField] Slider life;
    [SerializeField] TMP_Text currentLife;

    UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData;
    private bool gamePaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        additionalCameraData = Camera.main.transform.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            CheckPausePanel();
        }
    }

    private void CheckPausePanel()
    {
        if (panelsRef[0].panelObj.activeSelf)
        {
            DeactivatePanel(Panels.Pause);
        }
        else
        {
            ActivatePanel(Panels.Pause);
        }
    }

    public void ActivatePanel(Panels panel)
    {
        additionalCameraData.SetRenderer(2);
        Time.timeScale = 0;

        basePanel.SetActive(true);

        foreach(PanelRef panelRef in panelsRef)
        {
            if(panelRef.panel == panel)
            {
                panelRef.panelObj.SetActive(true);
            }
        }
    }

    public void ActivateDefeatPanel()
    {
        gamePaused = true;
        ActivatePanel(Panels.Defeat);
    }

    public void DeactivatePanel(Panels panel)
    {
        additionalCameraData.SetRenderer(1);
        Time.timeScale = 1;

        basePanel.SetActive(false);

        foreach (PanelRef panelRef in panelsRef)
        {
            if (panelRef.panel == panel)
            {
                panelRef.panelObj.SetActive(false);
            }
        }
    }

    public void ActivateText(bool value)
    {
        if(pauseTutorial.activeSelf == value)
        {
            return;
        }

        else pauseTutorial.SetActive(value);
    }

    public void DeactivatePausePanel()
    {
        additionalCameraData.SetRenderer(1);
        Time.timeScale = 1;

        basePanel.SetActive(false);

        panelsRef[0].panelObj.SetActive(false);
    }

    public void PlayerGotHit()
    {
        hit.SetTrigger("HIT");
    }

    public void SetPlayerCauntion(bool value)
    {
        CautionWarning.SetActive(value);
    }

    public void UpdateLife(float MaxLife, float CurrentLife)
    {
        int max = (int)MaxLife;
        int min = (int)CurrentLife;

        currentLife.text = min.ToString() + "/" +  max.ToString();

        life.maxValue = max;
        life.minValue = 0;
        life.value = min;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

}
