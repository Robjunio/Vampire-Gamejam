 using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Panels
    {
        Pause,
        Victory,
        Defeat
    }

    [Serializable]
    public class PanelRef
    {
        public Panels panel;
        public GameObject panelObj;
    }

    [SerializeField] GameObject basePanel;
    [SerializeField] PanelRef[] panelsRef;
    UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData;
    private bool gamePaused;

    private void Start()
    {
        additionalCameraData = Camera.main.transform.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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

    public void DeactivatePausePanel()
    {
        additionalCameraData.SetRenderer(1);
        Time.timeScale = 1;

        basePanel.SetActive(false);

        panelsRef[0].panelObj.SetActive(false);
    }

}
