using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "AudioDataBase", menuName = "SakamotoScriptable/AudioDataBase")]
public class AudioDataBase : ScriptableObject
{
    public List<AudioData> paramsList = new List<AudioData>();
}

