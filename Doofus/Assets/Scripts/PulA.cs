using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PulA : MonoBehaviour
{
    public static bool canScore = true;
    public TMP_Text text;
    private void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.tag == "Player")
        {
            //print("Here");
            PlayerMovement.score += 1;
            text.SetText("Score : " + PlayerMovement.score);
            canScore = false;
        }
    }
}
