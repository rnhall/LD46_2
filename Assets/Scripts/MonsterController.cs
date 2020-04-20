using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator monsterAnim;
    public bool FeedMe = false;
    public bool Omnomnom = false;
    public GameObject player;
    //public float feedtimeT = 0;

    // Start is called before the first frame update
    void Start()
    {
        monsterAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            //Omnomnom = true;
            player.GetComponent<PlayerController>().isDragging = false;
            monsterAnim.Play("Omnomnom");
            //monsterAnim.Play("Omnomnom");    
        }
    }

}
