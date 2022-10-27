using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private PauseMenu pauseMenu;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        pauseMenu = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack() && !pauseMenu.IsPaused())
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //object pooling creats multiple instances at launch
        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}



