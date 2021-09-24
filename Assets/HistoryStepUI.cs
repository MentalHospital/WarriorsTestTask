using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that sets up the row of history data
/// </summary>
public class HistoryStepUI : MonoBehaviour
{
    [SerializeField] private Text warriorTextComponent;
    [SerializeField] private Text stepTextComponent;
    [SerializeField] private Image background;

    private Dictionary<Allegiance, string> allegianceToString = new Dictionary<Allegiance, string>()
    {
        { Allegiance.Red, "К" },
        { Allegiance.Blue, "С" }
    };

    private Dictionary<Allegiance, Color32> allegianceToColor = new Dictionary<Allegiance, Color32>()
    {
        { Allegiance.Red, new Color32(240, 180, 160, 255) },
        { Allegiance.Blue, new Color32(160, 200, 240, 255) }
    };

    public void SetupTexts(HistoryStepData historyStepData)
    {
        if (historyStepData == null)
        {
            Debug.LogError("Unable to setup texts because Step Data was null");
            return;
        }
        warriorTextComponent.text = $"Существо {allegianceToString[historyStepData.warriorData.allegiance]}{historyStepData.warriorData.cell}\n"
            +$"Инициатива - {historyStepData.warriorData.initiative} Скорость - {historyStepData.warriorData.speed}";
        stepTextComponent.text = $"{historyStepData.step + 1}";
        background.color = allegianceToColor[historyStepData.warriorData.allegiance];
    }
}
