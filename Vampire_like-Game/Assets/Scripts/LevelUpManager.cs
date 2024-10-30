using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance;

    [System.Serializable]
    public class Choice
    {
        public GameObject New;
        public TMP_Text Level;
    }

    [SerializeField] Button[] Choices;
    [SerializeField] Choice[] ChoicesInfo;
    bool NoChoicesLeft;
    
    void Awake()
    {
        Instance = this;
    }
    public void LevelUPAvalible()
    {
        if (NoChoicesLeft) return;

        if(WeaponController.Instance.stakeLevel == 2)
        {
            Choices[0].interactable = false;
        }
        if(WeaponController.Instance.garlicLevel == 2)
        {
            Choices[1].interactable = false;
        }
        if(WeaponController.Instance.holyWaterLevel == 2)
        {
            Choices[2].interactable = false;
        }

        foreach(var button in Choices)
        {
            if (button.interactable)
            {
                LevelUP();
                return;
            }
        }
        NoChoicesLeft = true;
    }

    public void LevelUP()
    {
        ChoicesInfo[0].Level.text = "Level " + (WeaponController.Instance.stakeLevel + 1).ToString();
        ChoicesInfo[1].Level.text = "Level " + (WeaponController.Instance.garlicLevel + 1).ToString();
        ChoicesInfo[1].New.SetActive(WeaponController.Instance.garlicLevel == 0);
        ChoicesInfo[2].Level.text = "Level " + (WeaponController.Instance.holyWaterLevel + 1).ToString();
        ChoicesInfo[2].New.SetActive(WeaponController.Instance.holyWaterLevel == 0);

        UIManager.Instance.ActivateLevelUpPanel();
    }
}
