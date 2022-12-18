using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class CriPlayer : MonoBehaviour
{
    [SerializeField]CriAtomSource _criSource;

    private void Start()
    {
        GameManager.GameEnd += AudioStop; 
    }

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

    public void AudioStop() 
    {
        _criSource.Stop();
    }
}
