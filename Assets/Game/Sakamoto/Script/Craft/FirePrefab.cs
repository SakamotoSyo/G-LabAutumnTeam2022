using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class FirePrefab : MonoBehaviour
{
    [Header("Ç±ÇÃobjectÇ™è¡Ç¶ÇÈéûä‘")]
    [SerializeField] float _destroyTime;
    async void Start()
    {
       AudioManager.Instance.PlaySound(SoundPlayType.SE_fire);
       await UniTask.Delay(TimeSpan.FromSeconds(_destroyTime));
       AudioManager.Instance.PlaySound(SoundPlayType.SE_fire_finish);
       Destroy(gameObject);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamage damage)) 
        {
            damage.ApplyDamage(10);
        }
    }
}
