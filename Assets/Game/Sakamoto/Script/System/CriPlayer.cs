using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class CriPlayer : MonoBehaviour
{
    [SerializeField]CriAtomSource _criSource;

    void OnEnable()
    {
        _criSource.Play();
        StartCoroutine(ActiveTimer());
    }

    IEnumerator ActiveTimer() 
    {        
        yield return new WaitForSeconds(_criSource.time);
        gameObject.SetActive(false);
    }
}
