using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _defaulColor;

    private MeshRenderer _meshRenderer;

    private void Awake()
        => _meshRenderer = GetComponent<MeshRenderer>();

    private void OnEnable()
        => _meshRenderer.material.color = _defaulColor;

    public void ChangeRandom()
        => _meshRenderer.material.color = Random.ColorHSV();
}