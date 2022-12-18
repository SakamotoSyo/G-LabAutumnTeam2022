using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkBench : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("アイテムの製造時間")]
    [SerializeField] float _craftTime;
    [Header("加工が始まるまでの猶予時間")]
    [SerializeField] float _craftStartTime = 10;
    [SerializeField] Animator _workAnim;
    [SerializeField] SpriteRenderer _sr;

    [Tooltip("加工中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemInformation _itemData;
    [Tooltip("合成後のアイテム")]
    ItemInformation _resultSynthetic;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="itemInfo"></param>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        if (_manufactureing) return itemInfo;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic != null && itemInfo == null)
        {
            //アイテムがNullになった時アイテムを返す
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _sr.sprite = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return itemInfo;
        }


        //アイテムがマシンの許与量を超えていたらまたはアイテムが入っていなかったらアイテムを返す
        if (_itemData != null || itemInfo == null)
        {
            Debug.Log("アイテムを返すマス");
            return itemInfo;
        }
        //アイテムがNullではないときかつクラフトできるものだったら
        if (_itemData == null && itemInfo.Item.Processing)
        {
            Debug.Log("入ってきた");
            //アイテムが入ったことでクラフト待機スタート
            _itemData = itemInfo;
            _sr.sprite = itemInfo.Item.ItemSprite;
            return null;
        }

        return itemInfo;

    }

    /// <summary>
    /// アイテムを加工するメソッド
    /// </summary>
    void ItemManufacture()
    {
    
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i].Item1 == _itemData.Item.ItemName)
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                //Debug.Log($"結果は{_resultSynthetic}");
                break;
            }
            //Debug.Log($"結果判定中:アイテム１{itemArray[0]}、アイテム２:{itemArray[1]}、アイテム３:{itemArray[2]}");
        }

        _itemData = null;
    }

    /// <summary>
    /// クラフトをスタートする
    /// </summary>
    public float Craft()
    {
        if (_itemData == null) return 0;
        _manufactureing = true;
        //加工が始まったら煙を出す
        _workAnim.SetBool("WorkCraft", true);
        ChooseSe(_itemData.Item.ItemName);
        return _itemData. Item.CraftTime;

    }

    /// <summary>
    /// クラフトが終った時に呼ぶ
    /// </summary>
    public void CraftEnd()
    {
        ItemManufacture();
        _workAnim.SetBool("WorkCraft", false);
        _workAnim.SetTrigger("Comp");
        _manufactureing = false;
        _sr.sprite = _resultSynthetic.Item.ItemSprite;
        Debug.Log(_resultSynthetic.Item.ItemSprite);
    }

    void ChooseSe(string name) 
    {
        if (name[0] == '木')
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_wood);

        }
        else if (name[0] == '鉄')
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_iron);
        }
        else if (name[0] == '布') 
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_cloth);
        }
    }
}
