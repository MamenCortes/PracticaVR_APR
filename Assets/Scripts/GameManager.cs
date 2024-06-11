using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : Singleton<GameManager>
{
    public Transform ballInitialPos;
    public Transform ballFreeThrow;
    public Transform tennisInitialPos; 
    public GameObject hoop1;
    public GameObject player;
    public GameObject leftRayTeleport;
    public GameObject ball;
    public GameObject tennisBall; 
    private GameObject myEventSystem;

    //Controlers
    public GameObject rightRayController;
    public GameObject rightDirectController; 

    //HUD
    public GameObject scoreHUD;
    public GameObject timerHUD; 
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timeText;

    //UI
    public TextMeshProUGUI feedBackText;
    public GameObject panel1;
    public GameObject panel2; 

    public static GameManager instance;
    private bool gameMode;
    private int score;
    private int maxScore = 21; 

    //State variables
    [HideInInspector] public bool inArea1;
    [HideInInspector] public bool inArea2;
    [HideInInspector] public bool ballOnHand;

    //Variables para el temporizador
    private int minutos;
    private int segundos;
 


    void Start()
    {
        //Initial setup
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        gameMode = false;
        ball.SetActive(false);
        tennisBall.SetActive(false);
        score = 0;
        ballOnHand = false;
        myEventSystem = GameObject.Find("EventSystem");

        //HUD
        scoreHUD.SetActive(false);
        timerHUD.SetActive(false); 
        scoreText = scoreHUD.GetComponentInChildren<TextMeshProUGUI>();
        timeText = timerHUD.GetComponentInChildren<TextMeshProUGUI>();
        scoreText.text = null;

        //UI
        panel1.SetActive(true);
        panel2.SetActive(false); 

        //Timer
        segundos = 0;
        minutos = 5;

        //Controllers
        rightDirectController.SetActive(true);
        rightRayController.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode && ballOnHand)
        {
            leftRayTeleport.SetActive(false);
        }
        else
        {
            leftRayTeleport.SetActive(true); 
        }

        if(gameMode && score >= maxScore)
        {
            backToMainMenu(); 
        }
    }

    public void RespawnBall(Transform position)
    {

        ball.transform.position = position.position; 
        ball.transform.rotation = position.rotation;
        ball.SetActive(true);
    }
    public void RespawnPlayer(Transform position)
    {
        player.transform.position = position.position;
        player.transform.rotation = position.rotation;
    }

    public void enterGameMode()
    {
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        panel1.SetActive(false);
        panel2.SetActive(true);
        scoreHUD.SetActive(true);
        timerHUD.SetActive(true); 

        StartCoroutine(timer());
        scoreText.text = $"Score = {score}"; 
        gameMode = true;
        if (ball.activeSelf == false) RespawnBall(ballFreeThrow);
        hoop1.SetActive(false); //Hide the second hoop
        //Colocar al jugador
        RespawnPlayer(ballFreeThrow); 
        Debug.Log("Enter game mode"); 
    }
    public void exitGameMode()
    {
        gameMode = false;
        //ball.SetActive(false); 
        hoop1.SetActive(true);
        //RespawnPlayer(ballInitialPos); 
        endGame(); 
    }
    public void enterPracticeMode()
    {

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        panel1.SetActive(false);
        panel2.SetActive(true);
        scoreHUD.SetActive(true);
        rightDirectController.SetActive(false);
        rightRayController.SetActive(true); 

        scoreText.text = $"Score = {score}";
        if (ball.activeSelf == false) RespawnBall(ballInitialPos);
        tennisBall.SetActive(true);
        tennisBall.transform.position = tennisInitialPos.position; 
        RespawnPlayer(ballInitialPos); 
        Debug.Log("Enter practice mode");
        //Añadir más objetos
    }

    private void exitPracticeMode()
    {
        rightDirectController.SetActive(true);
        rightRayController.SetActive(false);
        feedBackText.text = "Ready To Play 21?";
        feedBackText.color = Color.white;
        tennisBall.SetActive(false); 
    }
    public void backToMainMenu()
    {
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        if (gameMode)
        {
            exitGameMode();
        }
        else
        {
            exitPracticeMode(); 
        }

        panel1.SetActive(true);
        panel2.SetActive(false);
        scoreHUD.SetActive(false);
        timerHUD.SetActive(false);
        ball.SetActive(false);
        RespawnPlayer(ballInitialPos);
        score = 0; 
    }
    public void updateCounter()
    {
        if (inArea1)
        {
            score++;
            Debug.Log("In area 1"); 
        }
        else if(inArea2){
            score = score + 2;
            Debug.Log("In area 2"); 
        }
        else
        {
            score = score + 3;
            Debug.Log("In area 3");
        }
        scoreText.text = $"Score = {score}"; 
    }

    //Corrutina para controlar el tiempo de juego 
    IEnumerator timer()
    {
        while (minutos >= 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            segundos--;
            //timeText.color = new Color(186,62,53); 

            if (segundos <= 0)
            {
                segundos = 59;
                minutos--;
                if (minutos == 0)
                {
                    timeText.color = Color.red;
                }
            }
            timeText.text = minutos + ":" + segundos;
            //Debug.Log(minutos + ":" + segundos);
        }

        backToMainMenu(); 
    }
    public void endGame()
    {
        StopAllCoroutines();
        Debug.Log(score); 
        if (minutos > 0 && segundos > 0 || score >= maxScore)
        {
            feedBackText.text = "Winner!!!";
            feedBackText.color = Color.green;
        }
        else
        {
            feedBackText.text = "Game over";
            feedBackText.color = Color.red;
        }
    }
}
