using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterS : MonoBehaviour
{

    private  float moveSpeed = 10f;
    private  IEnumerator coroutine;
    private protected GameObject player;
    private  float MaxHealth { get; set; }




    [SerializeField]private protected Animator animator;

    [SerializeField] private Transform monsterModel;
    protected void Start()
    {
        coroutine = MoveToPlayerCoroutine();
        player = GameObject.FindGameObjectWithTag("Player");
        //animator = GetComponentInChildren<Animator>();
        MaxHealth = 5;
    }

    // Update is called once per frame

    public void MoveToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        monsterModel.transform.rotation = Quaternion.LookRotation(direction);
        StartCoroutine(MoveToPlayerCoroutine());
    }
    IEnumerator MoveToPlayerCoroutine()
    {
        animator.SetBool("IsWalking", true);
        while (Vector3.Distance(transform.position, player.transform.position) >= 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        if(gameObject.GetComponent<ArmoredMonsterS>())
        {
            StartCoroutine(gameObject.GetComponent<ArmoredMonsterS>().Fighting());
        }
        else if(gameObject.GetComponent<StrongMonsterS>())
        {
            StartCoroutine(gameObject.GetComponent<StrongMonsterS>().Fighting());
        }
        else if(gameObject.GetComponent<QuickMonsterS>())
        {
            StartCoroutine(gameObject.GetComponent<QuickMonsterS>().Fighting());
        }
        yield break;
    }

    public virtual IEnumerator Fighting()
    {
        yield break;
    }

    public void Dying()
    {
        animator.SetBool("IsDying", true);

    }
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
