using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpHitbox : MonoBehaviour {

    public int timer = 0;
    public int timeout;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        timer++;
        if (timer >= timeout)
        {
            Destroy(this.gameObject);
        }
	}
}
