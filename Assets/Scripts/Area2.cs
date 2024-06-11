using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2 : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamer")
        {
            GameManager.instance.inArea2 = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            GameManager.instance.inArea2 = true;
        }
    }
}
