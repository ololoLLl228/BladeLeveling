using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject enemy;
    private IEnumerator coroutine;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TauntZone" && other.transform.parent != null)
        {
            Vector3 direction = other.transform.parent.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            other.transform.parent.gameObject.GetComponent<MonsterS>().MoveToPlayer();
            GetComponent<PlayerFighting>().Monster = other.transform.parent.gameObject;
            GetComponent<PlayerFighting>().TauntZoneToRespawn = other.transform.gameObject;
            enemy = other.transform.parent.gameObject;
            coroutine = GetComponent<PlayerMovement>().MoveToEnemy(enemy);
            StartCoroutine(coroutine);
            other.transform.parent = null;
           
        }
    }
    
}
