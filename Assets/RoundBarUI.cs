using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that sets up the separator bar with round title on it
/// </summary>
public class RoundBarUI : MonoBehaviour
{
    [SerializeField] private Text barTextComponent;
    public void SetBarText(int number)
    {
        barTextComponent.text =$"Раунд №{number}";
    }
}
