using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Transparentizer : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private Color _nativeColor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _nativeColor = _meshRenderer.material.color;
    }

    public void StartTransparentizing(float durationSeconds)
    {
        StopTransparentizing();
        _coroutine = StartCoroutine(Transparentize(durationSeconds));
    }

    public void StopTransparentizing()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Transparentize(float durationSeconds)
    {
        float timerSeconds = 0f;

        do
        {
            timerSeconds += Time.deltaTime;

            float progress = timerSeconds / durationSeconds;
            float alpha = Mathf.Lerp(1f, 0f, progress);

            _meshRenderer.material.color = new Color(_nativeColor.r, _nativeColor.g, _nativeColor.b, alpha);

            yield return null;
        }
        while (timerSeconds < durationSeconds);
    }
}