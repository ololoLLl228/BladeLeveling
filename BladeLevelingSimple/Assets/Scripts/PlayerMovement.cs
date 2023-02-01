using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private CharacterController controller;
    public float speed = 40f;
    private float tauntedSpeed = 36f;
    [SerializeField] private Transform playerModel;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Transform camera;
    public bool moveToEnemy = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main.transform;
    }

    
    void FixedUpdate()
    {        
        if(!moveToEnemy)
        {
            InputMoving();

        }       
    }
    private void InputMoving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(Vector3.forward.x, 0f, vertical).normalized;

        float targetAngle = /*Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + */camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
    public IEnumerator MoveToEnemy(GameObject enemy)
    {
        moveToEnemy = true;
        while(Vector3.Distance(transform.position, enemy.transform.position) > 3 )
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, tauntedSpeed*Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        GetComponent<PlayerFighting>().StartFighting();
        yield break;
    }

}
