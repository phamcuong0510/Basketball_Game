﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour
{
    //variables for swipe input detection
    private Vector3 fp; //First finger position
    private Vector3 lp; //Last finger position
    private float dragDistance;  //Distance needed for a swipe to register

    //variables for determining the shot power and position
    public float power;  //power at which the ball is shot
    private Vector3 footballPos; //initial football position for replacing the ball at the same posiiton
    private float factor = 34f; // keep this factor constant, also used to determine force of shot

   
    public  int scorePlayer = 0;  //score of player
    public int scoreOpponent = 0; //score of oponent
    public int turn = 0;   //0 for striker, 1 for goalie
    public bool isGameOver = false; //flag for game over detection
  
  
    private bool returned = true;  //flag to check if the ball is returned to its initial position
    
    public bool isKickedOpponent = false; //flag to check if the opponent has kicked the ball
    public bool triggered = false;

     GUIStyle guiStyle = new GUIStyle();
     GUIStyle guiStyle1 = new GUIStyle();
  

  
     float xball;
     Vector3 ballPos;
      public bool click = false;

      //highscore:
     
     public static float highscore;

     
    void Start()
    {
        Time.timeScale = 1;    //set it to 1 on start so as to overcome the effects of restarting the game by script
        dragDistance = Screen.height * 20 / 100; //20% of the screen should be swiped to shoot
        Physics.gravity = new Vector3(0, -20, 0); //reset the gravity of the ball to 20
        footballPos = transform.position;  //store the initial position of the football
         
           //High Score
          if (PlayerPrefs.GetFloat("HighScore") != null) {
            highscore = PlayerPrefs.GetFloat("HighScore");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
          //High Score
         if (scorePlayer > highscore) {
            highscore = scorePlayer;
            PlayerPrefs.SetFloat("HighScore", highscore);
        }

        
            //check if the football is in its initial position
            if (turn == 0 && !isGameOver)
            { //if its users turn to shoot and if the game is not over
                playerLogic();   //call the playerLogic fucntion
            }
            
                  //check if the scores differ by a value of 1
            if((scorePlayer==scoreOpponent+2) ||(scoreOpponent==scorePlayer+1)){ 
                isGameOver = true;
                    }
                    lp = Input.mousePosition;

       
        
    }

void OnMouseDrag() {
  
   Debug.Log("drag drag drag");
  if(!click){
       
   if (returned)
        {     //check if the football is in its initial position
            if (turn == 0 && !isGameOver)
            { //if its users turn to shoot and if the game is not over
    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
               {   //It's a drag

               if (lp.y > fp.y)  //If the movement was up
                        // {   //Up move
                            GetComponent<Rigidbody>().AddForce((new Vector3((lp.x-fp.x)/Screen.height*30,30, 8)) * power);
                     AudioSource sound = gameObject.GetComponent<AudioSource>();
                                 sound.Play();
                   
               
                    
                  returned = false;
                
               
               StartCoroutine(ReturnBall());
                click = true;
  }}}}

 }





      void playerLogic()
    {
   


  if (Input.GetButtonDown("Fire1"))
           {
           fp = Input.mousePosition;
          click = false;
        }

        if (Input.GetButtonUp("Fire1"))
           {
          
           

           }
    }

    IEnumerator ReturnBall()
    {   
        yield return new WaitForSeconds(2.0f);  //set a delay of 5 seconds before the ball is returned
        GetComponent<Rigidbody>().velocity = Vector3.zero;   //set the velocity of the ball to zero
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;  //set its angular vel to zero
         returned = true;
           //re positon it to initial position
     if(scorePlayer>1){
      xball = Random.Range(-0.5f, 0.5f);
      ballPos = new Vector3(xball, footballPos.y, footballPos.z);
      
       transform.position = ballPos;
     }
     if(scorePlayer <= 1){
         transform.position = footballPos;
     }
        
        scoreOpponent++; //increment the goals tally of opponent
      
        if (turn == 1)
            turn = 0;
        
    }


   
    //function to check if its a goal
    void OnTriggerEnter(Collider other)
    {
        //check if the football has triggered an object named GoalLine and triggered is not true
        if (other.gameObject.tag == "target" && !triggered)
        {   
             
             scorePlayer++;
           
        }  
    }
    

    void OnGUI()
    {
      
        //check if game is not over, if so, display the score
        if (!isGameOver)
        {
             guiStyle.fontSize = 200;
             guiStyle.normal.textColor = Color.gray;
             guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(400, 1000, 300, 50),(scorePlayer).ToString(),guiStyle);
        }
        else
{
Time.timeScale = 0; //set the timescale to zero so as to stop the game world
//display the final score
if(isGameOver)
 guiStyle.fontSize = 200;
             guiStyle.normal.textColor = Color.gray;
              guiStyle1.fontSize = 100;
             guiStyle1.normal.textColor = Color.yellow;
             guiStyle.alignment = TextAnchor.UpperCenter;
             guiStyle1.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(400,  Screen.height/4+2*Screen.height/10+10, 300, 50),(scorePlayer).ToString(),guiStyle);
             GUI.Label(new Rect(400,  Screen.height/4+Screen.height/10+10, 300, 50),"Best:"+(highscore).ToString(),guiStyle1);
//restart the game on click
if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+4*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "RESTART")){
Application.LoadLevel(Application.loadedLevel);
}
 
//load the main menu, which as of now has not been created
if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+5*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "MAIN MENU")){
Application.LoadLevel("MenuScene");
}
 
// //exit the game
// if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+3*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "EXIT GAME")){
// Application.Quit();
// }   
}     
} 



}