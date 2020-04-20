using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviour
{
    public Animation animation;
    public Camera camera;
    public FirstPersonAIO controller;

    private AudioSource audioSource;
    public AudioClip[] hitSounds;
    private AudioClip hitClip; 
    public float attackRange;
    public bool attacked;

    public DragRigidbody drag;
    public bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Are we dragging a body?
        if (GameObject.Find("Rigidbody dragger") != null) isDragging = true;
        //isDragging = drag.isDragging;


        //Move more slowly if you're dragging a body.
        if (isDragging)
        {
            //controller.walkSpeed = 1.5f;
            //controller.sprintSpeed = 1.5f;

            controller.walkSpeed = 4f;
            controller.sprintSpeed = 8f;
        } else
        {
            controller.walkSpeed = 4f;
            controller.sprintSpeed = 8f;
        }

        //Attack if the right mouse button is pressed down.
        if (attacked == true) attacked = false;
        if (Input.GetMouseButtonDown(1))
        {
            attacked = Attack();
        }
        
    }

    public bool Attack()
    {
        animation.Play();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            //If you attack a food person, then you ragdoll them and play a bonk! sound.
            if (hit.collider.tag == "Food")
            {
                GameObject hitObj = hit.transform.gameObject;
                hitObj.GetComponent<CustomerAI>().MakeRagdoll();
                int index = Random.Range(0, hitSounds.Length);
                hitClip = hitSounds[index];
                audioSource.clip = hitClip;
                audioSource.Play();
                return true;
            }

            //If you attack a police officer, then it's game over!
            if (hit.collider.tag == "Police")
            {
                GameObject hitObj = hit.transform.gameObject;
                hitObj.GetComponent<PoliceAI>().attacked = true;
                return true;
            }
        }
        return false;
    }
}
