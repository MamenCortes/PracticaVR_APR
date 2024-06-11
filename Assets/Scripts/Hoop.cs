using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "ball")
        {
            GameManager.instance.updateCounter(); 
        }
    }
}
