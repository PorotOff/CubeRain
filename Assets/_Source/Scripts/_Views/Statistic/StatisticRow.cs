using TMPro;
using UnityEngine;

public class StatisticRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statisticLabelText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private string _separator = ": ";

    private string _labelWithoutSeparator;

    public bool IsLabelMatch(string label)
        => label.ToLower() == _labelWithoutSeparator.ToLower();

    public void Display(string statisticLabel, string value)
    {
        _statisticLabelText.text = $"{statisticLabel}{_separator}";
        _valueText.text = value;

        _labelWithoutSeparator = statisticLabel;
    }
}