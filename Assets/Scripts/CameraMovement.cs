using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;


    private void Awake()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
    }
}
