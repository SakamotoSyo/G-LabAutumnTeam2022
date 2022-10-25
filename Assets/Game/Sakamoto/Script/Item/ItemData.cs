using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemData")]
public class ItemData : MonoBehaviour
{
    [Header("���H�ł��邩�ǂ���")]
    [SerializeField] public bool Processing;

    [Header("�����ł��邩�ǂ���")]
    [SerializeField] public bool Craft;

    [Header("�A�C�e���f�[�^�̃v���n�u")]
    [SerializeField] public GameObject ItemPrefab;

}
