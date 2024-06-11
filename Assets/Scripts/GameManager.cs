using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public GameObject ballPrefab;
    public Transform ballInitialPos;

    private int points;
    private bool gameMode;
    private bool practiceMode;
    private GameObject ball; 

    void Start()
    {
        gameMode = false;
        practiceMode = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMode)
        {
            
        }
    }

    private void RespawnBall()
    {
        ball = Instantiate(ball, ballInitialPos.transform.position, ballInitialPos.transform.rotation);
    }
}
