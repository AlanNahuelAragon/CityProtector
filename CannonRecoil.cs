using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRecoil : MonoBehaviour
{
    public GameObject recoilPoint = null;
    public GameObject initialPoint = null;

    public static CannonRecoil instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
    public void ActivateRecoil()
    {
        StartCoroutine(ActivateRecoilCoroutine());
    }
    IEnumerator ActivateRecoilCoroutine()
    {
        gameObject.transform.position = recoilPoint.transform.position;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = initialPoint.transform.position;
    }
}
