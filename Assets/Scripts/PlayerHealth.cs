using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    private int _health;
    [SerializeField] private TextMeshProUGUI healthText;


    private void Awake()
    {
        _health = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        healthText.text = _health.ToString();
        if (_health <= 0)
        {

        }
    }

    public void changeHealth(int healthChange)
    {
        _health += healthChange;
    }

    public void setHealth(int health)
    {
        _health = health;
    }

    public int getHealth()
    {
        return _health;
    }
}
