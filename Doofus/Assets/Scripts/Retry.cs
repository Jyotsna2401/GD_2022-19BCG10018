using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    // Start is called before the first frame update
    public void RetryGame()
    {
        SceneManager.LoadScene("Game");
    }
}
