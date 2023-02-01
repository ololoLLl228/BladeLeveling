using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ArmoredMonsterS : MonsterS
{
    private int level;
    private Stats stats;
    private float animationDuration = 1.1f;
    private float speed;
    private float attackSpeed;
    private float intervalBeforeDamage;
    private float strength;
    private Stats playerStats;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Stats>();
        stats = GetComponent<Stats>();
        level = stats.Level;
        animator = GetComponentInChildren<Animator>();
        strength = stats.Strength;
        attackSpeed = stats.AttackSpeed;
        animator.SetFloat("AttackSpeed", animationDuration * 0.5f);
        intervalBeforeDamage = 1 / (animationDuration * 0.5f * 2.5f);

    }
    
    

    public override IEnumerator Fighting()
    { 
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(intervalBeforeDamage);

            if (player.GetComponent<Stats>().Level < GetComponent<Stats>().Level)
            {
                StartCoroutine(player.GetComponent<PlayerFighting>().Dying());
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", false);
                yield break;
            }

        yield break;
    }
}
