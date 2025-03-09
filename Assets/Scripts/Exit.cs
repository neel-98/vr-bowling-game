using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // detect the collision
    void OnTriggerEnter(Collider other)
    {
        // if collision with player, exit the application
        if(other.CompareTag("Player"))
        {
            // Quit the application
        	Application.Quit();
        }
    }
}
