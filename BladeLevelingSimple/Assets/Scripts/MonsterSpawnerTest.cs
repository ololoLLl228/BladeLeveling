using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerTest : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    private float x, z, xTemporary, zTemporary;
    private Collider ground;
    private int iterations = 0;

    private float distanceBetweenTauntZones = 50;

    private Vector3 lastPosition;

    private Collider[] colliders;

    private Vector3 sizeOfMonsterZone = new Vector3(30, 2f, 30);
    private Vector3 spawnPosition;


    private List<Vector3> allPositions = new List<Vector3>();
    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>();
        StartPosition();
        NextPosition();
    }

    private bool isPositionOnGround;
    private bool checkSpawnPointCollider;

    private void StartPosition()
    {
        while (!isPositionOnGround || !checkSpawnPointCollider)
        {
            FindRandomPoint();
            spawnPosition = new Vector3(x, 0.7f, z);
            isPositionOnGround = IsPointOnGround(x, z);
            checkSpawnPointCollider = CheckSpawnPointCollider(spawnPosition);
        }
        Instantiate(monster, spawnPosition, Quaternion.identity);
        lastPosition = spawnPosition;
        allPositions.Add(lastPosition);
        isPositionOnGround = false;
        checkSpawnPointCollider = false;

    }


    private void NextPosition()
    {
        while (iterations < 1000)
        {
            switch (CheckFreePoint())
            {
                case 1:
                    xTemporary = x - distanceBetweenTauntZones;
                    zTemporary = z;
                    spawnPosition = new Vector3(xTemporary, 0.7f, zTemporary);
                    Instantiate(monster, spawnPosition, Quaternion.identity);
                    break;
                case 2:
                    xTemporary = x + distanceBetweenTauntZones;
                    zTemporary = z;
                    spawnPosition = new Vector3(xTemporary, 0.7f, zTemporary);
                    Instantiate(monster, spawnPosition, Quaternion.identity);
                    break;
                case 3:
                    zTemporary = z - distanceBetweenTauntZones;
                    xTemporary = x;
                    spawnPosition = new Vector3(xTemporary, 0.7f, zTemporary);
                    Instantiate(monster, spawnPosition, Quaternion.identity);
                    break;
                case 4:
                    zTemporary = z + distanceBetweenTauntZones;
                    xTemporary = x;
                    spawnPosition = new Vector3(xTemporary, 0.7f, zTemporary);
                    Instantiate(monster, spawnPosition, Quaternion.identity);
                    break;
                default:
                    break;
            }
            iterations++;
            if(iterations%4 == 0)
            {
                x = xTemporary;
                z = zTemporary;
            }
        }
    }

    void FindRandomPoint()
    {
        x = Random.Range(ground.transform.position.x - Random.Range(0, ground.bounds.extents.x - 15f), ground.transform.position.x + Random.Range(0, ground.bounds.extents.x - 15f));
        z = Random.Range(ground.transform.position.z - Random.Range(0, ground.bounds.extents.z - 15f), ground.transform.position.z + Random.Range(0, ground.bounds.extents.z - 15f));
    }




    private bool CheckSpawnPointCollider(Vector3 spawnPos)
    {
        
        print("position checking");
        colliders = Physics.OverlapBox(spawnPos, sizeOfMonsterZone);
        if (colliders.Length > 1)
        {
            for(int i = 0; i  < colliders.Length; i++)
            {
                print(colliders[i]);
            }
            return false;
        }
        else
        {
            return true;
        }

    }
    private bool IsPointOnGround(float x, float z)
    {
        if ((x > (ground.transform.position.x - ground.bounds.extents.x + 15f) && z > (ground.transform.position.z - ground.bounds.extents.z + 15f)) &&
            x < (ground.transform.position.x + ground.bounds.extents.x - 15f) && z < (ground.transform.position.z + ground.bounds.extents.z - 15f))
        {
            return true;
        }
        
        else
        {
            return false;
        }
    }


    private int CheckFreePoint()
    {
        print("checking free point");
        if(CheckLeftPoint())
        {
            print("leftPointChecked");
            return 1;
        }
        else if(CheckRightPoint())
        {
            return 2;
        }
        else if(CheckDownPoint())
        {
            return 3;
        }
        else if(CheckUpPoint())
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    private bool CheckLeftPoint()
    {
        if(IsPointOnGround(x - distanceBetweenTauntZones, z) && CheckSpawnPointCollider(new Vector3(x - distanceBetweenTauntZones, 0.7f, z)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckRightPoint()
    {
        if(IsPointOnGround(x + distanceBetweenTauntZones, z) && CheckSpawnPointCollider(new Vector3(x + distanceBetweenTauntZones, 0.7f, z)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckDownPoint()
    {
        if (IsPointOnGround(x, z - distanceBetweenTauntZones) && CheckSpawnPointCollider(new Vector3(x, 0.7f, z - distanceBetweenTauntZones)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckUpPoint()
    {
        if (IsPointOnGround(x, z + distanceBetweenTauntZones) && CheckSpawnPointCollider(new Vector3(x, 0.7f, z + distanceBetweenTauntZones)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
