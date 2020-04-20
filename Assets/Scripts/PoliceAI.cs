using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class PoliceAI : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;

    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Animator animator;
    public Rigidbody rootrb;
    public Transform linecastPoint;

    public bool roaming;
    public int roamRadius;
    public int roamTimerConst = 100;
    public int roamTimer;

    public bool attacked;

    public bool alerted;
    public int alertTimer;
    public int alertMax = 1000;

    public AudioSource audioSource;
    public AudioClip sirenClip;
    public AudioClip stopRightThere;

    // Start is called before the first frame update
    void Start()
    {
        roamTimer = 0;
        SetKinematic(true);
        agent.updateRotation = false;
        alerted = false;
        alertTimer = alertMax;
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
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        //Handles the alerted status if an officer sees you dragging a body.
        if ((PlayerInSight() && playerController.isDragging) || PlayerInSight() && playerController.attacked)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = 4.0f;
            character.Move(agent.desiredVelocity, false, false);
            if (alerted == false)
            {
                audioSource.clip = sirenClip;
                audioSource.Play();
            }
            alerted = true;
        } else
        {
            agent.speed = 1.25f;
        }

        if (alerted && alertTimer > 0)
        {
            agent.SetDestination(player.transform.position);
            audioSource.volume = (float)alertTimer / alertMax;
            alertTimer -= 1;
        } else
        {
            audioSource.Stop();
            alerted = false;
            alertTimer = alertMax;
        }

        //Handles being attacked
        if (attacked)
        {
            SceneManager.LoadScene(3);
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

    public bool PlayerInSight()
    {
        RaycastHit hit;
        if (Physics.Linecast(this.transform.position, player.transform.position, out hit))
        {
            float angle = Vector3.Dot(agent.transform.forward, linecastPoint.position - player.transform.position);
            Debug.Log(angle);
            if (hit.collider.gameObject.tag == "Player" && angle < 0)
            {
                Debug.Log(hit.collider.gameObject.layer);
                Debug.DrawLine(linecastPoint.position, player.transform.position, Color.black);
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && alerted)
        {
            //audioSource.clip = stopRightThere;
            //audioSource.Play();
            playerController.controller.enabled = false;
            StartCoroutine(Example());
        }
    }

    IEnumerator Example()
    {
        audioSource.clip = stopRightThere;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(6);
        SceneManager.LoadScene(3);
    }
}




