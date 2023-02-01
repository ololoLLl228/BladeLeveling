using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerOnTauntZone : MonoBehaviour
{

    public GameObject MonsterToDestroy { get; set; }
    private GameObject monsterToSpawn;
    private int typeOfMonster;

    private Vector3 positionToSpawn;

    private void Start()
    {
        MonsterToDestroy = transform.parent.gameObject;

        if (MonsterToDestroy != null)
        {

            positionToSpawn = MonsterToDestroy.transform.position;
        }
        
    }

    

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());

    }

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(5);
        typeOfMonster = Random.Range(0, 3);
        print("type of monster is " + typeOfMonster);
        switch(typeOfMonster)
        {
            case 0:

                print("instantiating quick monster");
                monsterToSpawn = Resources.Load<GameObject>("EnemyPrefabs/QuickEnemy");
                Instantiate(monsterToSpawn, positionToSpawn, Quaternion.identity);
                break;
            case 1:
                print("Instantiating strong monster");
                monsterToSpawn = Resources.Load<GameObject>("EnemyPrefabs/StrongEnemy");
                Instantiate(monsterToSpawn, positionToSpawn, Quaternion.identity);
                break;
            case 2:
                print("Instantiating armored monster");
                monsterToSpawn = Resources.Load<GameObject>("EnemyPrefabs/ArmoredEnemy");
                Instantiate(monsterToSpawn, positionToSpawn, Quaternion.identity);
                break;
            default:
                break;
        }
        Destroy(gameObject);
        Destroy(MonsterToDestroy);
        yield break;
    }
}
