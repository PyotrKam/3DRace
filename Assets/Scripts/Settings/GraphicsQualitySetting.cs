using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GraphicsQualitySetting : Setting
{
    private int currentLevelIndex = 0;
    public override bool isMinValue { get => currentLevelIndex == 0; }
    public override bool isMaxValue { get => currentLevelIndex == QualitySettings.names.Length - 1; }

    public override void SetNextValue()
    {
        if (isMaxValue == false)
        {
            currentLevelIndex++;
        }
    }

    public override void SePreviousValue()
    {
        if (isMinValue == false)
        {
            currentLevelIndex--;
        }
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentLevelIndex);
    }
}
