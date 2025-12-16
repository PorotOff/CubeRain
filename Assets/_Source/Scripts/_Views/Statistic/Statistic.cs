using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    [SerializeField] private StatisticBlock _statisticBlockPrefab;
    [SerializeField] private StatisticRow _statisticRowPrefab;

    private List<StatisticBlock> _statisticBlocks = new List<StatisticBlock>();

    public void AddValue(string statisticsBlockLabel, string statisticLabel, string value)
    {
        StatisticBlock statisticBlock;

        if (TryFindStatisticBlock(statisticsBlockLabel, out statisticBlock) == false)
        {
            statisticBlock = Instantiate(_statisticBlockPrefab, transform);
            statisticBlock.Display(statisticsBlockLabel);
            _statisticBlocks.Add(statisticBlock);
        }

        StatisticRow statisticRow = Instantiate(_statisticRowPrefab, statisticBlock.StatisticRowsContainer);
        statisticRow.Display(statisticLabel, value);
        statisticBlock.AddStatisticRow(statisticRow);
    }

    public void UpdateValue(string statisticBlockLabel, string statisticLabel, string value)
    {
        if (TryFindStatisticBlock(statisticBlockLabel, out StatisticBlock statisticBlock))
        {
            if (statisticBlock.TryFindStatisticRow(statisticLabel, out StatisticRow statisticRow))
                statisticRow.Display(statisticLabel, value);
        }
    }

    private bool TryFindStatisticBlock(string label, out StatisticBlock statisticBlock)
    {
        statisticBlock = _statisticBlocks.Find(statisticBlock => statisticBlock.IsLabelMatch(label));
        return statisticBlock != null;
    }
}