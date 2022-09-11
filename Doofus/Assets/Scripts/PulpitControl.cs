using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PulpitControl : MonoBehaviour
{
    public GameObject PulpitA;
    public GameObject PulpitB;

    private float PlaneSize = 9;
    private bool TurnA;
    private float LifeTimeA, LifeTimeB, ShowTimeA, ShowTimeB;

    public TMP_Text PulA_Text, PulB_Text;

    // Start is called before the first frame update
    void Start()
    {
        TurnA = true;
        LifeTimeA = Random.Range(4f, 5f);
        ShowTimeA = LifeTimeA;
        PulA_Text.SetText("" + LifeTimeA);
        //StartCoroutine(PrintTime());
        StartCoroutine(WaitForSecondPulpit());
    }

    /*IEnumerator PrintTime()
    {
        yield return new WaitForSeconds(1/10000f);
        //if (PulpitA.active)
        //{
        ShowTimeA = ShowTimeA - Time.deltaTime;
        PulA_Text.SetText("" + ShowTimeA);
        //}
        //else
        //{
        ShowTimeB = ShowTimeB - Time.deltaTime;
        PulB_Text.SetText("" + ShowTimeB);
        //}
        StartCoroutine(PrintTime());
    }*/

    void FixedUpdate()
    {
        ShowTimeA = ShowTimeA - Time.deltaTime;
        PulA_Text.SetText("" + ShowTimeA);
  
        ShowTimeB = ShowTimeB - Time.deltaTime;
        PulB_Text.SetText("" + ShowTimeB);
    }

    IEnumerator WaitForSecondPulpit()
    {
        yield return new WaitForSeconds(2.5f);

        SpawnOtherPulpit();

        StartCoroutine(KillCurrentPulpit());
    }

    void SpawnOtherPulpit()
    {
        Vector3 newPos;

        float direction = Random.Range(0, 10); // probability for x or z axis

        if (direction > 5f) // x axis
        {
            float sides = Random.Range(0, 10); // +x or -x side
            if(sides > 5f) // +x
            {
                newPos = new Vector3(PlaneSize, 0, 0); // size of plane is 10 default
            }

            else              // -x
            {
                newPos = new Vector3(-1 * PlaneSize, 0, 0); // size of plane is 10 default
            }
        }

        else
        {
            float sides = Random.Range(0, 10); // +z or -z side
            if (sides > 5f) // +z
            {
                newPos = new Vector3(0, 0, PlaneSize); // size of plane is 10 default
            }

            else              // -z
            {
                newPos = new Vector3(0, 0, -1 * PlaneSize); // size of plane is 10 default
            }
        }


        if (TurnA)
        {
            PulpitB.SetActive(true);
            LifeTimeB = Random.Range(4f, 5f);
            ShowTimeB = LifeTimeB;
            PulB_Text.SetText("" + LifeTimeB);
            PulpitB.transform.position = PulpitA.transform.position + newPos;
        }

        else
        {
            PulpitA.SetActive(true);
            LifeTimeA = Random.Range(4f, 5f);
            ShowTimeA = LifeTimeA;
            PulA_Text.SetText("" + LifeTimeA);
            PulpitA.transform.position = PulpitB.transform.position + newPos;
        }
    }

    IEnumerator KillCurrentPulpit()
    {
        if(TurnA)
        {
            yield return new WaitForSeconds(LifeTimeA - 2.5f);
            PulpitA.SetActive(false);
            PulB.canScore = true;
        }

        else
        {
            yield return new WaitForSeconds(LifeTimeB - 2.5f);
            PulpitB.SetActive(false);
            PulB.canScore = true;
        }

        TurnA = !TurnA;
        StartCoroutine(WaitForSecondPulpit());
    }
}
