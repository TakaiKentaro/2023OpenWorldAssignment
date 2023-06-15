using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IPool
{
    bool Waiting { get; set; }

    void SetUp(Transform parent);
    void IsUseSetUp();
    bool Execute();
    void Delete();
}

public class ObjectPool<TPool> where TPool : MonoBehaviour, IPool
{
    private List<TPool> _pools;
    private TPool _type;
    private Transform _parent;
    private const int DEFAULT_POOL_COUNT= 10; //生成数

    public ObjectPool(TPool pool, Transform parent = null, int poolObjectCount = DEFAULT_POOL_COUNT)
    {
        _pools = new List<TPool>();
        _type = pool;
        _parent = parent;

        CreatObject();
    }

    void CreatObject()
    {
        for (int i = 0; i < DEFAULT_POOL_COUNT; i++)
        {
            TPool p = Object.Instantiate(_type);

            if (_parent != null)
            {
                p.transform.SetParent(_parent);
                p.transform.position = _parent.position;
            }

            p.Waiting = true;
            p.SetUp(_parent);

            _pools.Add(p);
        }
    }

    public TPool Use()
    {
        TPool useObj = _pools.FirstOrDefault(p => p.Waiting);

        if (useObj != null)
        {
            useObj.Waiting = false;
            useObj.IsUseSetUp();
            useObj.StartCoroutine(WaitUsing(useObj));

            return useObj;
        }
        else
        {
            CreatObject();
            return Use();
        }
    }

    IEnumerator<TPool> WaitUsing(TPool useObj)
    {
        while (useObj.Execute())
        {
            yield return null;
        }

        useObj.Delete();
        useObj.Waiting = true;
    }
}