using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
        => _meshRenderer = GetComponent<MeshRenderer>();

    public void RandomChange()
        => _meshRenderer.material.color = Random.ColorHSV();
}