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
    [SerializeField] public Sprite ItemSprite;

    [Header("�v���[���g�̉摜")]
    [SerializeField] public Sprite PresentSprite;

    [Header("�������ɓ��ꂽ�Ƃ��̂��̑f�ނ��\�����N�����܂ł̎���")]
    [SerializeField] public float RunawayTime;

    [Header("���H�ɂ����鎞��")]
    [SerializeField] public float CraftTime;

    [Header("���̃A�C�e���Ɏg�����i�̐�")]
    [SerializeField] public float ItemParts;
}
