using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;
    [SerializeField] float camAngleX;
    [SerializeField] float camAngleY;
    [SerializeField] float maxAngleX;

    bool isMove;

    Animator animator;

    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    void Update()
    {

        LookAround();
        Move();
        
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;


        camAngleX = camAngle.x - mouseDelta.y * 300 * Time.deltaTime;
        camAngleY = camAngle.y + mouseDelta.x * 300 * Time.deltaTime;


        if(camAngleX <= 180f)
        {
           camAngleX =  Mathf.Clamp(camAngleX, -1f, 70f);
        }
        else
        {
            camAngleX = Mathf.Clamp(camAngleX, 335f, 361f);
        }


        cameraArm.rotation = Quaternion.Euler(camAngleX, camAngleY , camAngle.z);
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);
        
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
        
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized, Color.green);
    }
}
