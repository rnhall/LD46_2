using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    public Animator monsterAnim;
    public bool FeedMe = false;
    public bool Omnomnom = false;
    public PlayerController player;
    public int numFed;
    //public float feedtimeT = 0;

    // Start is called before the first frame update
    void Start()
    {
        numFed = 0;
        monsterAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(numFed == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            //Omnomnom = true;
            player.isDragging = false;
            monsterAnim.Play("Omnomnom");
            numFed += 1;
            //monsterAnim.Play("Omnomnom");    
        }
    }

}
