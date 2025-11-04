using UnityEngine;

[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Cube))]
public class GroundCollisionHandler : MonoBehaviour
{
    private GroundChecker _groundChecker;
    private ColorChanger _colorChanger;
    private Cube _cube;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _colorChanger = GetComponent<ColorChanger>();
        _cube = GetComponent<Cube>();
    }

    private void OnEnable()
        => _groundChecker.Grounded += OnGrounded;

    private void OnDisable()
        => _groundChecker.Grounded -= OnGrounded;

    private void OnGrounded()
    {
        _colorChanger.RandomChange();
        _cube.StartDelayedDestroy();
        Destroy(this);
    }
}