using UnityEngine;

/// <summary>
/// Class that links up history generator and history display
/// </summary>
public class BattleHistoryController : MonoBehaviour
{
    [SerializeField] private BattleHistoryGenerator generator;
    [SerializeField] private BattleHistoryUI display;

    void Start()
    {
        generator.OnWarriorPopped += display.AddHistoryElement;
        generator.OnRoundChanged += display.AddRoundBar;
        generator.OnStepSkipped += display.RemoveFirstHistoryElement;
        generator.OnWarriorKilled += display.Clear;
    }
}
