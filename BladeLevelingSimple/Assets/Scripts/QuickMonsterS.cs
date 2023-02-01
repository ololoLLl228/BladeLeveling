using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMonsterS : MonsterS
{
    private int level;
    private Stats stats;
    private float animationDuration = 3.833f;
    private float speed;
    private float attackSpeed;
    private float intervalBeforeDamage;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<Stats>();
        level = stats.Level;
        animator = GetComponentInChildren<Animator>();
        attackSpeed = stats.AttackSpeed;
        animator.SetFloat("AttackSpeed", animationDuration * 0.3f);
        speed = animationDuration / attackSpeed;
        intervalBeforeDamage = 1 / (animationDuration * 0.7f*0.3f);

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
