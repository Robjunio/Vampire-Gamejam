using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject basePrefab;
    protected List<GameObject> objectsFree = new List<GameObject>();
    protected List<GameObject> objectsUsed = new List<GameObject>();

    protected virtual GameObject CreateObject()
    {
        var obj = Instantiate(basePrefab);
        obj.SetActive(false);
        objectsFree.Add(obj);

        return obj;
    }

    public virtual GameObject GetFreeObject()
    {
        if(objectsFree.Count == 0)
        {
            CreateObject();
        }
        
        var obj = objectsFree[0];
        objectsFree.RemoveAt(0);

        return obj;
    }

    protected virtual void ReturnObject(GameObject gameObject)
    {
        objectsUsed.Remove(gameObject);
        objectsFree.Add(gameObject);

        gameObject.SetActive(false);
    }
}
