using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that generates history data
/// </summary>
public class BattleHistoryGenerator : MonoBehaviour
{
    public event System.Action<HistoryStepData> OnWarriorPopped;
    public event System.Action<HistoryStepData> OnStepSkipped;
    public event System.Action OnWarriorKilled;
    public event System.Action<int> OnRoundChanged;

    [SerializeField] private List<WarriorData> redArmy;
    [SerializeField] private List<WarriorData> blueArmy;
    [SerializeField] private List<HistoryStepData> currentHistory;

    private List<WarriorData> remainingWarriors;

    private int currentRound;
    private int currentStep;

    private void Awake()
    {
        currentRound = 1;
        redArmy = new List<WarriorData> { 
            new WarriorData(1, 8, 4),
            new WarriorData(2, 8, 4),
            new WarriorData(3, 9, 5),
            new WarriorData(4, 4, 3),
            new WarriorData(5, 2, 3),
            new WarriorData(6, 3, 4),
            new WarriorData(7, 1, 1)
        };
        blueArmy = new List<WarriorData> {
            new WarriorData(1, 6, 6),
            new WarriorData(2, 8, 5),
            new WarriorData(3, 9, 5),
            new WarriorData(4, 8, 4),
            new WarriorData(5, 2, 3),
            new WarriorData(6, 4, 2),
            new WarriorData(7, 1, 1)
        };
        SetArmyAllegiance(redArmy, Allegiance.Red);
        SetArmyAllegiance(blueArmy, Allegiance.Blue);
        currentStep = 0;
        RegenerateRemainingWarriorsFromRound(currentRound);
    }

    private void Start()
    {
        RegenerateHistoryList();
    }

    public void Skip()
    {
        currentHistory.RemoveAt(0);
        var nextStepData = GetNextHistoryStep();
        OnStepSkipped?.Invoke(nextStepData);
        currentHistory.Add(nextStepData);
    }

    public void KillNextWarrior()
    {
        if (redArmy.Count + blueArmy.Count <= 1) // Остался один герой во всех ячейках
        {
            Debug.LogError("There's no \"Next\" warrior");
            return;
        }

        var target = currentHistory[1].warriorData;
        switch (target.allegiance)
        {
            case Allegiance.Red:
                redArmy.Remove(target);
                break;
            case Allegiance.Blue:
                blueArmy.Remove(target);
                break;
        }
        currentStep = currentHistory[0].step;
        currentRound = currentHistory[0].round;
        OnWarriorKilled?.Invoke();
        RegenerateRemainingWarriorsFromRound(currentRound);
        RegenerateHistoryList();
    }

    private void SetArmyAllegiance(List<WarriorData> army, Allegiance allegiance)
    {
        foreach(var warrior in army)
        {
            warrior.allegiance = allegiance;
        }
    }
        
    private void RegenerateHistoryList()
    {
        OnRoundChanged?.Invoke(currentRound);
        if (currentHistory != null && currentHistory.Count > 0)
        {
            currentRound = currentHistory[0].round;
        }
        else
        {
            currentRound = 1;
        }
        currentStep = 0;
        currentHistory.Clear();
        int historyLength = 20;
        int elementsCounter = 0;
        int roundLength = redArmy.Count + blueArmy.Count;
        int currentCellRound = currentRound;
        for (int i = 0; i < historyLength; i++)
        {
            var nextStep = GetNextHistoryStep();

            if (nextStep == null)
            {
                Debug.LogError("Unable to get next history step");
                return;
            }

            currentHistory.Add(nextStep);
            elementsCounter += 1;
            if (elementsCounter >= roundLength) 
            {
                currentCellRound += 1;
                elementsCounter = 0;
            }
        }
    }

    private void RegenerateRemainingWarriorsFromRound(int roundNumber)
    {
        remainingWarriors = new List<WarriorData>(redArmy);
        remainingWarriors.AddRange(blueArmy);
        if (roundNumber % 2 == 1)
        {
            remainingWarriors = remainingWarriors.OrderByDescending(w => w.initiative)
                .ThenByDescending(w => w.speed)
                .ThenBy(w => w.allegiance)
                .ThenBy(w => w.cell)
                .ToList();
        }
        else
        {
            remainingWarriors = remainingWarriors.OrderByDescending(w => w.initiative)
                .ThenByDescending(w => w.speed)
                .ThenByDescending(w => w.allegiance)
                .ThenBy(w => w.cell)
                .ToList();
        }
    }

    private HistoryStepData GetNextHistoryStep()
    {
        var fittestWarrior = PopFittestWarrior();
        
        if (fittestWarrior == null)
            return null;

        var historyData = new HistoryStepData(
            fittestWarrior,
            currentRound,
            currentStep
            );
        currentStep += 1;
        OnWarriorPopped?.Invoke(historyData);
        if (remainingWarriors.Count == 0)
        {
            currentRound += 1;
            OnRoundChanged?.Invoke(currentRound);
            RegenerateRemainingWarriorsFromRound(currentRound);
        }
        return historyData;
    }

    private WarriorData PopFittestWarrior()
    {
        if (remainingWarriors.Count > 0)
        {
            var poppedWarrior = remainingWarriors[0];
            remainingWarriors.Remove(poppedWarrior);
            return poppedWarrior;
        }
        else
        {
            Debug.LogError("No more warriors to choose next from!");
            return null;
        }
    }
}
