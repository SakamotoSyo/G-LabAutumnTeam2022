using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TakeOrderPresenter : MonoBehaviour
{
    [SerializeField] TakeOrdersScript _takeOrderModel;
    [SerializeField] TakeOrdarView _takeOrdarView;

    private void Start()
    {
        _takeOrderModel.CurrentSynthetic.Subscribe(value => _takeOrdarView.SetRenderer(value));

        _takeOrderModel.MaxOrderTime.Subscribe(value => _takeOrdarView.MaxTimeSet(value));
        _takeOrderModel.CountOrderTime.Subscribe(value => _takeOrdarView.SetTimeCurrent(value));
    }

}
