using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolable : MonoBehaviour
{
    public IObjectPool<GameObject> Poolable { get; private set; }

    public void SetManagedPool(IObjectPool<GameObject> pool)
    {
        Poolable = pool;
    }

    public void ReleaseObject()
    {
        Poolable.Release(gameObject);
    }

    public void ObjectOff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
