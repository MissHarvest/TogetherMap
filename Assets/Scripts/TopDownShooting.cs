using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector3.right;

    public GameObject testPrefab;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvenet += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    public void OnShoot()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        float rotZ = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        Instantiate(testPrefab, projectileSpawnPosition.position, Quaternion.Euler(0, 0, rotZ));
    }
}
