using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateCsvImpoter")]
public class CsvImporter : ScriptableObject
{
    public TextAsset csvFile;
}
