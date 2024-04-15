using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject creditsList;
    public GameObject creditsResetPoint;
    public float timeOut = 10f;

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        StartCoroutine(CreditsTimeOut());
        
    }
    public void CloseCredits()
    {
        StopAllCoroutines();
        creditsList.transform.position = creditsResetPoint.transform.position;
        creditsPanel.SetActive(false);
    }
    IEnumerator CreditsTimeOut()
    {
        yield return new WaitForSeconds(timeOut);
        CloseCredits();
    }
}
