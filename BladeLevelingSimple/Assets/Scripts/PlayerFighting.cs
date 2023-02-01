using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFighting : MonoBehaviour
{

    private IEnumerator damagingCoroutine;
    private float strength;
    private Animator animator;
    private float animationDuration = 2.267f;
    private float speed;
    private float attackSpeed;
    private float intervalBeforeDamage;
    [SerializeField] private Transform playerModel;

    public GameObject TauntZoneToRespawn { get; set; }

    public GameObject Monster { get; set; }

    private void Start()
    {
        damagingCoroutine = Damaging();
        strength = GetComponent<PlayerStats>().Strength;
        animator = GetComponentInChildren<Animator>();
        //animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        SetAttackSpeed();
    }

    private void SetAttackSpeed()
    {
        attackSpeed = GetComponent<PlayerStats>().AttackSpeed;
        animator.SetFloat("AttackSpeed", animationDuration);
        speed = animationDuration / attackSpeed;
        intervalBeforeDamage = 1/(animationDuration);
    }

    public void StartFighting()
    {
        GetComponentInChildren<Animator>().SetBool("IsAttacking", true);
        print("startfighting");
        SetAttackSpeed();
        StartCoroutine(Damaging());

    }
    public IEnumerator Damaging()
    {

        
        
        yield return new WaitForSeconds(intervalBeforeDamage);
        while(true)
        {
            

            if(GetComponent<Stats>().Level >= Monster.GetComponent<Stats>().Level)
            {
                break;
            }
            yield return new WaitForSeconds(1/attackSpeed);      
        }
        Monster.GetComponent<MonsterS>().Dying();
        GetComponent<PlayerMovement>().speed+=1;
        Debug.Log(GetComponent<PlayerMovement>().speed);
        int monsterLevel = Monster.GetComponent<Stats>().Level;
        float level = GetComponent<Stats>().Level;
        PlayerStats playerStats = GetComponent<PlayerStats>();
        playerStats.GetExperience(1);
        
        TauntZoneToRespawn.GetComponent<RespawnerOnTauntZone>().Respawn();
        animator.SetBool("IsAttacking", false);
        GetComponent<PlayerMovement>().moveToEnemy = false;
        yield break;
    }
    public IEnumerator Dying()
    {
        PlayerPrefs.SetInt("IsBegining", 1);
        print("player dying");
        animator.SetBool("IsDying", true);
        StopAllCoroutines();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
