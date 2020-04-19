using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animation animation;
    public Camera camera;

    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
        
    }

    public void Attack()
    {
        animation.Play();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            if (hit.collider.tag == "Food")
            {
                GameObject hitObj = hit.transform.gameObject;
                hitObj.GetComponent<CustomerAI>().MakeRagdoll();
            }
        }

    }
}
