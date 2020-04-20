using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    public Animator monsterAnim;
    private AudioSource monsteraudioSource;
    private AudioClip monsterhitClip;
    //public AudioClip monsterAudio;
    public bool FeedMe = false;
    public bool Omnomnom = false;
    public float feedtimeT = 0;
    public PlayerController player;
    public int numFed;
    //public float feedtimeT = 0;

    // Start is called before the first frame update
    void Start()
    {
        numFed = 0;
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
        if(numFed == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            player.isDragging = false;
            player.GetComponent<PlayerController>().isDragging = false;
            monsterAnim.Play("Omnomnom");  
            monsterAnim.Play("Omnomnom");
            numFed += 1;
        }
    }

}
