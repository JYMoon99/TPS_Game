using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private Vector3 _Direction;

    [SerializeField]
    private float _Speed = 3f;

    IObjectPool<Bullet> ManagedPool;

    public void Shoot(Vector3 dir)
    {
        _Direction = dir;
        // 5초가 지난 후 총알을 파괴
        Invoke("DestroyBullet", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_Direction * Time.deltaTime * _Speed);
    }

    public void SetManagedPool(IObjectPool<Bullet> pool)
    {
        ManagedPool = pool;
    }

    public void DestroyBullet()
    {
        ManagedPool.Release(this);
    }
}
