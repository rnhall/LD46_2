using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class DoctorAI : MonoBehaviour
{

    public List<GameObject> customers;

    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Animator animator;
    public Rigidbody rootrb;

    public bool roaming;
    public int roamRadius;
    public int roamTimerConst = 100;
    public int roamTimer;

    public bool healing;
    public Vector3 healingTarget;
    public GameObject customerToHeal;
    public bool atDestination;
    public bool attacked;

    public AudioSource audioSource;
    public AudioClip sirenClip;

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
        //Manages idle roaming state.
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
                atDestination = false;
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                atDestination = true;
                character.Move(Vector3.zero, false, false);
            }
        }

        customerToHeal = RagdollsInSight();

        if (customerToHeal != null)
        {
            healingTarget = customerToHeal.transform.position;
            if (healing == false)
            {
                agent.SetDestination(healingTarget);
                agent.speed = 2.0f;
            }
            healing = true;
            roaming = false;

            if (atDestination)
            {
                HealCustomer(customerToHeal);
                customerToHeal = null;
                healing = false;
                roaming = true;
            }
        }
        else
        {
            agent.speed = 1.25f;
        }

        //Handles being attacked
        if (attacked)
        {
            //TO IMPLEMENT//
            Debug.Log("GAME OVER!");
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

    public GameObject RagdollsInSight()
    {
        foreach(GameObject customer in customers)
        {
            RaycastHit hit;
            if (Physics.Linecast(this.transform.position, customer.transform.position, out hit))
            {
                float angle = Vector3.Dot(agent.transform.forward, agent.transform.position - customer.transform.position);
                if (hit.collider.gameObject.layer != 9 && angle < 0 && customer.GetComponent<CustomerAI>().isRagdoll)
                {
                    Debug.DrawLine(agent.transform.position, customer.transform.position, Color.red);
                    return customer;
                }
            }
        }
        return null;
    }

    private void HealCustomer(GameObject customer)
    {

        Debug.Log("In the name of Jesus be healed!");
        customers.Remove(customer);

        //customers.Add(Instantiate(customer, customer.transform.position, customer.transform.rotation));

        Destroy(customer);
        
    }
}




