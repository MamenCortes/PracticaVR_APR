using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]


public class ThrowBall : MonoBehaviour
{
    public GameObject ball;
    public float power;
    public GameObject ballInitPos;
    public float secsToDestroy;

    private GameObject iBall;
    public static event Action onObjectThrown;
    private bool pickedUp;
    private List<Vector3> trackingPos = new List<Vector3>(); 

    private void Start()
    {
        //RespawnBall();
        pickedUp = false; 
    }

    void RespawnBall()
    {
        iBall = Instantiate(ball, ballInitPos.transform.position, ballInitPos.transform.rotation);
        iBall.transform.parent = ballInitPos.transform;
    }

    private void Update()
    {
        if (pickedUp)
        {
            if (trackingPos.Count > 15)
            {
                trackingPos.RemoveAt(0); 
            }
            trackingPos.Add(transform.position);
            Debug.Log(trackingPos);
        }
    }

    public virtual void objectThrown()
    {
        onObjectThrown?.Invoke();
        Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.transform.parent = null;
        pickedUp = false;
        trackingPos.Clear(); 
        ball.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Acceleration);
        Debug.Log("Ball thrown"); 
    }
    public virtual void delete()
    {
        Destroy(this.gameObject);
    }
    public void ballSelected()
    {
        pickedUp = true; 
    }
}