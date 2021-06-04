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
        UIManager.Instance.AddInfo("the guards are going to search you!");
        foreach(guardMove g in guards)
        {
            g.perquisition(true, settleTime);
        }
        isTimerGoing = false;
    }
    public void EndPerquisition()
    {
        UIManager.Instance.AddInfo("All good, go on");
        foreach (guardMove g in guards)
        {
            g.perquisition(false, settleTime);
        }
    }
}
