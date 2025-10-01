using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolAtlas : Singleton<ObjectPoolAtlas>
{
    [Header("=== Object Pool Atlas ===")]

    [Header("Pool atlas")]
    private readonly Dictionary<Object, ObjectPool<PoolMember>> poolByPrefabMapping = new();
    private readonly Dictionary<ObjectPool<PoolMember>, Transform> poolHolderMapping = new();

    public PoolMember Get(GameObject prefab, Transform holder = null)
    {
        if (prefab.GetComponent<PoolMember>() == null)
        {
            Debug.LogError($"Prefab need to be a {typeof(PoolMember)}");
            return null;
        }

        poolByPrefabMapping.TryGetValue(prefab, out ObjectPool<PoolMember> pool);
        if (pool == null)
        {
            pool = CreatePool(prefab, holder);
            poolByPrefabMapping[prefab] = pool;
        }
        return pool.Get();
    }

    private ObjectPool<PoolMember> CreatePool(GameObject prefab, Transform holder)
    {
        PoolMember prefabPoolMember = prefab.GetComponent<PoolMember>();
        ObjectPool<PoolMember> pool = new(
            createFunc: () => CreatePoolMember(prefab),
            actionOnGet: (PoolMember poolMember) => poolMember.OnGetFromPool(),
            actionOnRelease: (PoolMember poolMember) =>
            {
                poolMember.OnReleaseToPool();
                poolMember.transform.SetParent(poolHolderMapping[poolMember.Pool]);
            },
            actionOnDestroy: (PoolMember poolMember) => poolMember.OnDestroyFromPool(),
            collectionCheck: false,
            defaultCapacity: prefabPoolMember.DefaultCapacity,
            maxSize: prefabPoolMember.MaxSize);

        if (holder == null)
        {
            GameObject holderObj = new GameObject($"{prefab.name} holder");
            holder = holderObj.transform;
            holder.SetParent(transform);
        }
        poolHolderMapping[pool] = holder;

        return pool;
    }

    private PoolMember CreatePoolMember(GameObject prefab)
    {
        poolByPrefabMapping.TryGetValue(prefab, out ObjectPool<PoolMember> pool);
        if (pool == null)
        {
            Debug.LogError("No pool found");
            return null;
        }

        PoolMember poolMember = Instantiate(prefab, poolHolderMapping[pool]).GetComponent<PoolMember>();
        poolMember.SetPool(pool);
        return poolMember;
    }
}