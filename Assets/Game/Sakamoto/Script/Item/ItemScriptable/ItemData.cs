using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("�A�C�e����")]
    [SerializeField] public string ItemName;

    [Header("���H�ł��邩�ǂ���")]
    [SerializeField] public bool Processing;

    [Header("�����ł��邩�ǂ���")]
    [SerializeField] public bool Craft;

    [Header("����ł��邩")]
    [SerializeField] public bool Packing;

    [Header("�A�C�e���̃X�v���C�g")]
    [SerializeField] public Sprite _itemSprite;

    [Header("�A�C�e���f�[�^�̃v���n�u")]
    [SerializeField] public GameObject ItemPrefab;

    [Header("�������ɓ��ꂽ�Ƃ��̂��̑f�ނ��\�����N�����܂ł̎���")]
    [SerializeField] public float RunawayTime;

}