using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for displaying history data
/// </summary>
public class BattleHistoryUI : MonoBehaviour
{
    [SerializeField] private GameObject roundBarPrefab;
    [SerializeField] private GameObject historyElementPrefab;
    [SerializeField] private Transform scrollContentTransform;

    private List<GameObject> scrollContentList;

    void Awake()
    {
        scrollContentList = new List<GameObject>();
    }

    public void AddRoundBar(int roundNumber)
    {
        var bar = Instantiate(roundBarPrefab, scrollContentTransform);
        bar.GetComponent<RoundBarUI>().SetBarText(roundNumber);
        scrollContentList.Add(bar);
    }

    public void AddHistoryElement(HistoryStepData historyStep)
    {
        var historyStepUI = Instantiate(historyElementPrefab, scrollContentTransform).GetComponent<HistoryStepUI>();
        historyStepUI.SetupTexts(historyStep);
        scrollContentList.Add(historyStepUI.gameObject);
    }

    public void RemoveFirstHistoryElement(HistoryStepData historyStep)
    {
        if (scrollContentList[0].GetComponent<RoundBarUI>())
        {
            Destroy(scrollContentList[1]);
            scrollContentList.RemoveAt(1);
        }
        Destroy(scrollContentList[0]);
        scrollContentList.RemoveAt(0);
    }

    public void Clear()
    {
        foreach(var obj in scrollContentList)
        {
            Destroy(obj);
        }
        scrollContentList.Clear();
    }
}
