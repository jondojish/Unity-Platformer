using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform playerCenter;

    [SerializeField] private int knockBack;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.Equals(Player))
        {
            playerHealth.changeHealth(-10);
            Vector2 forceDirection = new Vector2(playerCenter.position.x - transform.position.x, playerCenter.position.y - transform.position.y); // vector from saw center to place center
            forceDirection.Normalize(); // changes vector to unit vector i.e direction
            print(forceDirection);
            Player.GetComponent<Rigidbody2D>().AddForce(forceDirection * knockBack);
        }
    }
}
