using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticBlock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private string _separator = ":";

    [field: SerializeField] public Transform StatisticRowsContainer { get; private set; }

    private string _labelWithoutSeparator;
    private List<StatisticRow> _statisticRows = new List<StatisticRow>();

    public bool IsLabelMatch(string label)
        => label.ToLower() == _labelWithoutSeparator.ToLower();

    public void Display(string statisticsGroupLabel)
    {
        _labelText.text = $"{statisticsGroupLabel}{_separator}";
        _labelWithoutSeparator = statisticsGroupLabel;
    }

    public void AddStatisticRow(StatisticRow statisticRow)
        => _statisticRows.Add(statisticRow);

    public bool TryFindStatisticRow(string statisticLabel, out StatisticRow statisticRow)
    {
        statisticRow = _statisticRows.Find(statisticRow => statisticRow.IsLabelMatch(statisticLabel));
        return statisticRow != null;
    }
}