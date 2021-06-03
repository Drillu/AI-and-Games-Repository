using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perquisitionManager : MonoBehaviour
{
    public static perquisitionManager instance;
    public float timeToPerquisition;
    public float settleTime;
    public List<guardMove> guards;
    private bool isTimerGoing;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isTimerGoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerGoing)
        {
            StartCoroutine(Timer());
        }
    }
    private IEnumerator Timer()
    {
        isTimerGoing = true;
        yield return new WaitForSeconds(timeToPerquisition);

        foreach(guardMove g in guards)
        {
            g.perquisition(true, settleTime);
        }
        isTimerGoing = false;
    }
    public void EndPerquisition()
    {
        foreach (guardMove g in guards)
        {
            g.perquisition(false, settleTime);
        }
    }
}
