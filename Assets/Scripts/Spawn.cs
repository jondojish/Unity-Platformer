using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private Rigidbody2D _rigidBody;
    [SerializeField] private Transform InitialSpawn;

    private PlayerHealth _playerHealth;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();
        SpawnAt(InitialSpawn.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealth.getHealth() <= 0)
        {
            SpawnAt(InitialSpawn.position);
            _playerHealth.setHealth(100);
        }
    }

    private void SpawnAt(Vector3 SpawnPoint)
    {
        _rigidBody.transform.position = SpawnPoint;
    }
}
