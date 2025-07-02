using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsButton : UISelectableButton
{
    [SerializeField] private Setting setting;
    [SerializeField] private Text titleText;
    [SerializeField] private Text valueText;
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        ApplyProperty(setting);
    }

    public void SetNextValueSetting() 
    {
        setting?.SetNextValue();
        setting?.Apply();
        UpdateInfo();
    }
    public void SetPreviousValueSetting() 
    { 
        setting?.SePreviousValue();
        setting?.Apply();
        UpdateInfo();
    } 

    

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }

    public void ApplyProperty(Setting property)
    {
        if (property == null) return;

        setting = property;

        UpdateInfo();

    }

}
