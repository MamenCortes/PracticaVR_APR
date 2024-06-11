using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area1 : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            GameManager.instance.inArea1 = false; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            GameManager.instance.inArea1 = true;
        }
    }
}
