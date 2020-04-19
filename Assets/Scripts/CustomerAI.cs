using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class CustomerAI : MonoBehaviour
{ 
    // public Camera camera;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Animator animator;
    public Rigidbody rootrb;

    public bool roaming;
    public int roamRadius;
    public int roamTimerConst = 100;
    public int roamTimer;

    public bool isRagdoll;

    // Start is called before the first frame update
    void Start()
    {
        roamTimer = 0;
        SetKinematic(true);
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        
        //    if (Physics.Raycast(ray, out hit))
         //   {
           //     agent.SetDestination(hit.point);
            //}
        //}

        if (roaming && agent.enabled)
        {
            if (roamTimer <= 0)
            {
                agent.SetDestination(RandomNavmeshLocation(roamRadius));
                roamTimer = (int)Random.Range(100, 1000);
            }

            roamTimer -= 1;
        }

        if (agent.enabled)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }
    }

    void SetKinematic(bool newValue)
    {
        //Get an array of components that are of type Rigidbody
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //rb.drag = 1;
            //rb.angularDrag = 1;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void MakeRagdoll()
    {
        agent.enabled = false;
        character.enabled = false;
        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        cc.enabled = false;
        animator.enabled = false;
        SetKinematic(false);
        rootrb.isKinematic = true;
        isRagdoll = true;
    }
}

