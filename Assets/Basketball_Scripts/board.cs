using UnityEngine;
using System.Collections;

public class board : MonoBehaviour {
public float timer;
public int newtarget;
public float speed;
public NavMeshAgent nav;
public Vector3 Target;
public bool right = false;
	// Use this for initialization
	void Start () {
	nav = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	if(SwipeControl.scorePlayer >=10){
	timer += Time.deltaTime;
	if(timer >= newtarget){
		newTarget();
		timer = 0;
		nav.speed = 1;
	}
	}

	 if(transform.position.x < -1.3f){
			right = true;
		}
		if( transform.position.x > 1.3f){
			right = false;
		}
	if(  SwipeControl.scorePlayer >=5 && SwipeControl.scorePlayer < 11 )
	{
	if(right == false){
	 transform.Translate(Vector3.right * Time.deltaTime /2);
	} 
	 if(right == true){
		transform.Translate(Vector3.left * Time.deltaTime / 2);
	 }
	}
	}

	void newTarget(){
		
		
		

		float xPos = Random.Range(-1.3f,1.3f)
		;
		
		Target = new Vector3(xPos,gameObject.transform.position.y, gameObject.transform.position.z);
		nav.SetDestination(Target);
		nav.updateRotation = false;
		
	}
	//  void OnTriggerEnter(Collider other)
    // {
    //     //check if the football has triggered an object named GoalLine and triggered is not true
    //     if (other.gameObject.tag == "Player")
    //     {   
    //           GetComponent<Animator>().Play("ring");
    //     }
    // }
}