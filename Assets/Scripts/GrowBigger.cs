using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBigger : MonoBehaviour
{
    Vector3 size;
    public float timeT = 0;
    bool recentlyFed = false;

    void OnTriggernEnter(Collider collider)
    {
        Debug.Log(collider.gameObject);
        if (collider.gameObject.tag == "Food")
        {
            recentlyFed = true;
            Destroy(collider.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Food")
        {
            recentlyFed = true;
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        if (recentlyFed == true)
        {
            timeT += Time.deltaTime;
            size = transform.localScale;
            size.x += 0.05f;
            size.y += 0.05f;
            size.z += 0.05f;
            transform.localScale = size;
            if (timeT > 1)
            {
                recentlyFed = false;
                timeT = 0;
            }
        }
    }
}