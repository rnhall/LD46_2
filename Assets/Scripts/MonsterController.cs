using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator monsterAnim;
    private AudioSource monsteraudioSource;
    private AudioClip monsterhitClip;
    //public AudioClip monsterAudio;
    public bool FeedMe = false;
    public bool Omnomnom = false;
    public GameObject player;
    public float feedtimeT = 0;

    // Start is called before the first frame update
    void Start()
    {
        monsterAnim = gameObject.GetComponent<Animator>();
        monsteraudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        feedtimeT += Time.deltaTime;
        if (feedtimeT > 10)
        {
            monsterAnim.Play("FeedMe");
            monsteraudioSource.Play();
            feedtimeT = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            player.GetComponent<PlayerController>().isDragging = false;
            monsterAnim.Play("Omnomnom");  
        }
    }

}
