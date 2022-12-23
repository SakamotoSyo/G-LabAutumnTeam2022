using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField, Header("ストーリーイメージ")]
    Sprite[] _storyImages;
    [SerializeField, Header("イメージのオブジェクト")]
    Image _image;

    private int _currentNum = 0;

    private void Start()
    {
        _image.sprite = _storyImages[_currentNum];
    }

    public void NextImage()
    {
        if (_currentNum >= _storyImages.Length - 1) { return; }
        AudioManager.Instance.PlaySound(SoundPlayType.SE_select);

        _currentNum++;
        _image.sprite = _storyImages[_currentNum];

        if(_currentNum >= _storyImages.Length - 1)
        {
            this.transform.Find("Next").gameObject.SetActive(false);
            this.transform.Find("Skip").gameObject.SetActive(false);
            this.transform.Find("Play").gameObject.SetActive(true);
        }

        
    }

}
