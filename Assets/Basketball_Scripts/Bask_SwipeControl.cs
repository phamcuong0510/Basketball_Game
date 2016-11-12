﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bask_SwipeControl : MonoBehaviour
{
    //variables for swipe input detection
    private Vector3 fp; //First finger position
    private Vector3 lp; //Last finger position
    private float dragDistance;  //Distance needed for a swipe to register

    //variables for determining the shot power and position
    public float power;  //power at which the ball is shot
    private Vector3 footballPos; //initial football position for replacing the ball at the same posiiton
    

   
    public static  int scorePlayer = 0;  //score of player
    public int scoreOpponent = 0; //score of oponent
    public int turn = 0;   //0 for striker, 1 for goalie
    public bool isGameOver = false; //flag for game over detection
    private bool returned = true;  //flag to check if the ball is returned to its initial position
    public bool triggered = false;

     float xball;
     Vector3 ballPos;
      public bool click = false;

      //score + highscore:
        public Text scoreText;
        public Text highscoreText;
        public static float highscore;
        public Button[] buttons;

     
    void Start()
    {    
        
        Time.timeScale = 1;    //set it to 1 on start so as to overcome the effects of restarting the game by script
        dragDistance = Screen.height * 3 / 100; //3% of the screen should be swiped to shoot
        Physics.gravity = new Vector3(0, -20, 0); //reset the gravity of the ball to 20
        footballPos = transform.position;  //store the initial position of the football
      
           //High Score:
          if (PlayerPrefs.GetFloat("HighScore") != null) {
            highscore = PlayerPrefs.GetFloat("HighScore"); //Get score
        }
        
    }

    // Update is called once per frame
    void Update()
    {
          //High Score:
         if (scorePlayer > highscore) { //ckeck if score > highscore
            highscore = scorePlayer;
            PlayerPrefs.SetFloat("HighScore", highscore);// Save score
        }
        highscoreText.text = "Best: " + Mathf.Round(highscore);//Show hight score
         scoreText.text = "" + scorePlayer;// Show score
         if(isGameOver){ // check if is gameover
          foreach (Button button in buttons)
        {   
             
             Time.timeScale = 0; //Stop all
          
            button.gameObject.SetActive(true);//SetActive Buttons
            highscoreText.gameObject.SetActive(true); // Set Active high score text 
            
        }
         }

        
            //check if the football is in its initial position
            if (turn == 0 && !isGameOver)
            { //if its users turn to shoot and if the game is not over
                playerLogic();   //call the playerLogic fucntion
            }
            
                  //check if the scores differ by a value of 1
            if(scoreOpponent==scorePlayer+1){ 
                isGameOver = true;
                    }
                    lp = Input.mousePosition;

       
        
    }

void OnMouseDrag() {
  
   
  if(!click){
       
   if (returned)
        {     //check if the football is in its initial position
            if (turn == 0 && !isGameOver)
            { //if its users turn to shoot and if the game is not over
     if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
               {   //It's a drag

               if (lp.y > fp.y)  //If the movement was up
                        // move ball: 
                        {
                            GetComponent<Rigidbody>().AddForce((new Vector3((lp.x-fp.x)/Screen.height*170,28, 8)) * power);
                     AudioSource sound = gameObject.GetComponent<AudioSource>();
                                 sound.Play();
                        
               
                    
                  returned = false;
                
               
               StartCoroutine(ReturnBall());
                click = true;
                        }
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
        yield return new WaitForSeconds(2.3f);  //set a delay of 5 seconds before the ball is returned
        GetComponent<Rigidbody>().velocity = Vector3.zero;   //set the velocity of the ball to zero
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;  //set its angular vel to zero
       transform.rotation = Quaternion.identity;
         returned = true;
           //re positon it to initial position
     if(scorePlayer>4){
      xball = Random.Range(-0.4f, 0.4f);
      ballPos = new Vector3(xball, footballPos.y, footballPos.z);
      
       transform.position = ballPos;
     }
     
     if(scorePlayer>=1 && scorePlayer <=5){
      xball = Random.Range(-0.4f, 0.4f);
      ballPos = new Vector3(xball, footballPos.y, footballPos.z);
      
       transform.position = ballPos;
     }
     if(scorePlayer < 1){
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
    

   //Button functions:
public void OutToMain()
    {
        Application.LoadLevel("MenuScene");//Back Main Sence
    }
public void RePlay()
   
    {
		 Application.LoadLevel(Application.loadedLevel);//ReLoad sence
          scorePlayer = 0;
          Time.timeScale = 1; 
    }



}