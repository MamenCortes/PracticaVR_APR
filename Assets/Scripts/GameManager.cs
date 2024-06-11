using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : Singleton<GameManager>
{
    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public Transform ballInitialPos;
    public Transform ballFreeThrow;
    public GameObject hoop1;
    public GameObject player;
    public GameObject leftRayTeleport;
    public TextMeshProUGUI scoreText;

    private int points;
    private bool gameMode;
    private bool practiceMode;
    private bool mainMenu; 
    public GameObject ball;
    public static GameManager instance;
    private int score; 

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        gameMode = false;
        practiceMode = false;
        mainMenu = true; 
        ball.SetActive(false);
        scoreText.text = null; 
        score = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if(practiceMode)
        {
            
        }
        if (gameMode)
        {
            
            //leftRayTeleport.SetActive(false); 
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
        scoreText.text = $"Score = {score}"; 
        gameMode = true;
        mainMenu = false;
        if (ball.activeSelf == false) RespawnBall(ballFreeThrow);
        hoop1.SetActive(false); //Hide the second hoop
        //Colocar al jugador
        player.transform.position = ballFreeThrow.position;
        player.transform.rotation = ballFreeThrow.rotation;
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
        scoreText.text = $"Score = {score}";
        practiceMode = true;
        mainMenu = false;
        if (ball.activeSelf == false) RespawnBall(ballInitialPos);
        player.transform.position = ballInitialPos.position;
        player.transform.rotation = ballInitialPos.rotation;
        Debug.Log("Enter practice mode");
        //Añadir más objetos
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
        scoreText.text = null;
        ball.SetActive(false); 
        player.transform.position = ballInitialPos.position;
        player.transform.rotation = ballInitialPos.rotation;
    }
    public void updateCounter()
    {
        score++;
        scoreText.text = $"Score = {score}";
        Debug.Log(score); 
    }
}
