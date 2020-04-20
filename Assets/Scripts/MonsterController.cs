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
<<<<<<< HEAD
    public GameObject player;
    public float feedtimeT = 0;
=======
    public PlayerController player;
    public int numFed;
    //public float feedtimeT = 0;
>>>>>>> 380b5920e19d1045650cb16350abdd72ed1d5532

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
<<<<<<< HEAD
        feedtimeT += Time.deltaTime;
        if (feedtimeT > 10)
        {
            monsterAnim.Play("FeedMe");
            monsteraudioSource.Play();
            feedtimeT = 0;
=======
        if(numFed == 3)
        {
            SceneManager.LoadScene(2);
>>>>>>> 380b5920e19d1045650cb16350abdd72ed1d5532
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
<<<<<<< HEAD
            //Omnomnom = true;
            player.isDragging = false;
=======
            player.GetComponent<PlayerController>().isDragging = false;
<<<<<<< HEAD
            monsterAnim.Play("Omnomnom");  
=======
>>>>>>> e4eb24b454a02ae16470e50f2c1c72b6f9a91601
            monsterAnim.Play("Omnomnom");
            numFed += 1;
            //monsterAnim.Play("Omnomnom");    
>>>>>>> 380b5920e19d1045650cb16350abdd72ed1d5532
        }
    }

}
