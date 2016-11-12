using UnityEngine;
using System.Collections;

public class Bask_boardsound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	 void OnTriggerEnter(Collider other)
    {
        //check if the football has triggered an object named GoalLine and triggered is not true
        if (other.gameObject.tag == "Player")
        {   
              AudioSource sound = gameObject.GetComponent<AudioSource>();
                                 sound.Play();
                                 GetComponent<Animator>().Play("ring");
        }
    }
}
