using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBigger : MonoBehaviour
{
    Vector3 size;
    Vector3 newposition;
    public float timeT = 0;
    bool recentlyFed = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            recentlyFed = true;
            Destroy(collider.transform.root.gameObject);
            Destroy(GameObject.Find("Rigidbody dragger"));
        }
    }

    void Update()
    {
        if (recentlyFed == true)
        {
            timeT += Time.deltaTime;
            size = transform.localScale;
            size.x += 0.01f;
            size.y += 0.01f;
            size.z += 0.01f;
            transform.localScale = size;
            newposition = transform.position;
            newposition.y += 0.015f;
            transform.position = newposition;
            if (timeT > 1)
            {
                recentlyFed = false;
                timeT = 0;
            }
        }
    }
}