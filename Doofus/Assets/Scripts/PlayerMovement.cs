using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    private Animator animator;
    private Rigidbody rb;
    public Transform puppet;

    //GameScenes
    public GameObject Congrats;


    private float Speed;
    private bool canMove;

    public static int score = 0;

    int flip = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 8f;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        MovePlayer();
        if(score == 50)
        {
            StartCoroutine(DisplayCongrats());
        }
    }

    public void ResetScore()
    {
        score = -1;
    }

    IEnumerator DisplayCongrats()
    {
        Congrats.SetActive(true);
        yield return new WaitForSeconds(5);
        Congrats.SetActive(false);
    }

    void MovePlayer()
    {
        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;
            float z = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime;
            

            if(z < 0)
            {
                Vector3 temp = puppet.transform.localScale;
                temp.z = -1;
                puppet.transform.localScale = temp;
                flip = -1;
            }

            else if(z > 0)
            {
                Vector3 temp = puppet.transform.localScale;
                temp.z = 1;
                puppet.transform.localScale = temp;
                flip = 1;
            }

            if (x < 0)
            {
                Vector3 temp = puppet.transform.localEulerAngles;
                temp.y = -90 * flip;
                puppet.transform.localEulerAngles = temp;
            }

            else if (x > 0)
            {
                Vector3 temp = puppet.transform.localEulerAngles;
                temp.y = 90 * flip;
                puppet.transform.localEulerAngles = temp;
            }

            else
            {
                Vector3 temp = puppet.transform.localEulerAngles;
                temp.y = 0;
                puppet.transform.localEulerAngles = temp;
            }



            if (x != 0 || z != 0)
                animator.SetBool("IsWalking", true);
            else
                animator.SetBool("IsWalking", false);
            transform.Translate(x, 0, z);
            
        }
    }

    void IsGrounded()
    {
        if (!Physics.Raycast(GroundCheck.transform.position, Vector3.down, 0.5f, GroundLayer))
        {
            canMove = false;
            rb.isKinematic = false;
            SceneManager.LoadScene("GameLost");
            StartCoroutine(DeactivatePlayer());
        }
    }

    IEnumerator DeactivatePlayer()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
