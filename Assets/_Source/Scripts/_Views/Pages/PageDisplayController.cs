using UnityEngine;

public class PageDisplayController : MonoBehaviour
{
    [SerializeField] private Page _page;
    [SerializeField] private InputService _inputService;

    private void OnEnable()
        => _inputService.OpenedStatistic += OnOpenedStatistic;

    private void OnDisable()
        => _inputService.OpenedStatistic += OnOpenedStatistic;

    private void OnOpenedStatistic(bool isStatisticOpened)
        => _page.gameObject.SetActive(isStatisticOpened);
}