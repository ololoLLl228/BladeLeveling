using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject fastMonster;
    [SerializeField] private GameObject strongMonster;
    [SerializeField] private GameObject armoredMonster;
    private Collider ground;
    private float x, z, xTemporary, zTemporary;

    private Vector3 spawnPosition;
    private Vector3 sizeOfMonsterZone = new Vector3(30, 2f, 30);
    private Vector3 center = new Vector3(0, 0.5f, 0);

    private float currentMonsterAmount = 0;
    private float monsterAmount = 60;

    private Collider[] colliders;

    bool check;

    private float typeOfMonster;
    private float checkSpawnPointCounter = 0;

    private float iterations = 0;

    private float nextPositionNumber;

    private int checkInGround = 5;

    private List<Vector3> bannedZones = new List<Vector3>();
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>();
        StartPos();
    }

    private void StartPos()
    {
        if (checkSpawnPointCounter > 500)
        {
            print("spawn check limit is over, monster spawned: " + currentMonsterAmount);
            return;
        }
        else
        {
            check = false;

            while (!check || checkInGround != 0)
            {
                iterations++;
                if(iterations > 10)
                {
                    print("ti debil");
                    break;
                }
                if(checkInGround != 0)
                {
                    print(iterations + "iteration and checkground isnt zero");
                    if(checkInGround == 1)
                    {
                        xTemporary = x + 100;
                        zTemporary = z + 100;
                    }
                    else if(checkInGround == 2)
                    {
                        xTemporary = x - 100;
                        zTemporary = z - 100;
                    }
                    else if(checkInGround == 3)
                    {
                        xTemporary = x + 100;
                        zTemporary = z - 100;
                    }
                    else if(checkInGround == 4)
                    {
                        xTemporary = x - 100;
                        zTemporary = z + 100;
                    }
                    else
                    {
                        
                    }

                }
                if (currentMonsterAmount == 0)
                {
                    x = Random.Range(ground.transform.position.x - Random.Range(0, ground.bounds.extents.x - 15f), ground.transform.position.x + Random.Range(0, ground.bounds.extents.x - 15f));
                    z = Random.Range(ground.transform.position.z - Random.Range(0, ground.bounds.extents.z - 15f), ground.transform.position.z + Random.Range(0, ground.bounds.extents.z - 15f));
                    xTemporary = x;
                    zTemporary = z;
                }
                else
                {
                    nextPositionNumber = iterations % 6;
                    switch (nextPositionNumber)
                    {
                        case 1:
                            xTemporary = x + 35;
                            break;
                        case 2:
                            zTemporary = z + 35;
                            break;
                        case 3:
                            xTemporary = x - 35;
                            break;
                        case 4:
                            zTemporary = z - 35;
                            break;
                        default:
                            break;
                    }
                }
                spawnPosition = new Vector3(xTemporary, 0.7f, zTemporary);
                check = CheckSpawnPoint(spawnPosition);
                checkInGround = CheckInGround(xTemporary, zTemporary);
            }

            x = xTemporary;
            z = zTemporary;


            if (check)
            {
                typeOfMonster = Random.Range(1, 4);
                if (currentMonsterAmount < monsterAmount)
                {
                    switch (typeOfMonster)
                    {
                        case 1:
                            GameObject monster = Instantiate(fastMonster, spawnPosition, Quaternion.identity);
                            currentMonsterAmount++;
                            StartPos();
                            break;
                        case 2:
                            GameObject monster2 = Instantiate(strongMonster, spawnPosition, Quaternion.identity);
                            currentMonsterAmount++;
                            StartPos();
                            break;
                        case 3:
                            GameObject monster3 = Instantiate(armoredMonster, spawnPosition, Quaternion.identity);
                            currentMonsterAmount++;
                            StartPos();
                            break;
                        default:
                            StartPos();
                            print("Changing position");
                            break;
                    }
                }
            }
            else
            {

                StartPos();
                
                
            }
        }
    }

    private bool CheckSpawnPoint(Vector3 spawnPos)
    {
        checkSpawnPointCounter++;
        print("position checking");
        colliders = Physics.OverlapBox(spawnPos, sizeOfMonsterZone);
        if(colliders.Length > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
    private int CheckInGround(float x, float z)
    {
        /*if((x < (ground.transform.position.x - ground.bounds.extents.x - 15f) || x > (ground.transform.position.x + ground.bounds.extents.x - 15f)) ||
            (z < (ground.transform.position.x - ground.bounds.extents.x - 15f) || z > (ground.transform.position.x + ground.bounds.extents.x - 15f)))
        {
            return false;
        }
        else
        {
            return true;
        }*/
        if(x < (ground.transform.position.x - ground.bounds.extents.x - 15f) && z < (ground.transform.position.z - ground.bounds.extents.z - 15f))
        {
            return 1;
        }
        else if (x > (ground.transform.position.x + ground.bounds.extents.x - 15f) && z > (ground.transform.position.z + ground.bounds.extents.z - 15f))
        {
            return 2;
        }
        else if(x < (ground.transform.position.x - ground.bounds.extents.x - 15f) && z > (ground.transform.position.z - ground.bounds.extents.z - 15f))
        {
            return 3;
        }
        else if(x > (ground.transform.position.x - ground.bounds.extents.x - 15f) && z < (ground.transform.position.z - ground.bounds.extents.z - 15f))
        {
            return 4;
        }
        else 
        {
            return 0;
        }
    }
    
}
