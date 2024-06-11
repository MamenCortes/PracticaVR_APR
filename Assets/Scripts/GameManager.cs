using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public Transform ballInitialPos;
    public Transform ballFreeThrow;
    public GameObject hoop1;
    public GameObject player;
    public GameObject leftRayTeleport; 

    private int points;
    private bool gameMode;
    private bool practiceMode;
    private bool mainMenu; 
    public GameObject ball; 

    void Start()
    {
        gameMode = false;
        practiceMode = false;
        mainMenu = true; 
        ball.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(practiceMode)
        {
            if(ball.activeSelf == false) RespawnBall(ballInitialPos);
            player.transform.position = ballInitialPos.position;
            player.transform.rotation = ballInitialPos.rotation;
        }
        if (gameMode)
        {
            if (ball.activeSelf == false) RespawnBall(ballFreeThrow);
            hoop1.SetActive(false); //Hide the second hoop
            //Colocar al jugador
            player.transform.position = ballFreeThrow.position;
            player.transform.rotation = ballFreeThrow.rotation;
            leftRayTeleport.SetActive(false); 
        }
    }

    public void RespawnBall(Transform position)
    {

        ball.transform.position = position.transform.position; 
        ball.transform.rotation = position.transform.rotation;
        ball.SetActive(true);
    }

    public void enterGameMode()
    {
        gameMode = true;
        mainMenu = false;
        Debug.Log("Enter game mode"); 
        //Start counter 
    }
    public void exitGameMode()
    {
        gameMode = false; 
        //Stop counter
    }
    public void enterPracticeMode()
    {
        practiceMode = true;
        mainMenu = false; 
        Debug.Log("Enter prctice mode");
    }
    public void exitPracticeMode()
    {
        practiceMode = false;
        //Stop counter
    }
    public void backToMainMenu()
    {
        exitPracticeMode();
        exitGameMode();
        mainMenu = true;
        player.transform.position = ballInitialPos.position;
        player.transform.rotation = ballInitialPos.rotation;
    }
}
