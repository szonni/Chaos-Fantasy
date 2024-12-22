using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    public static MapSelector instance;
    public List<GameObject> mapChunks;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Prevent the creation of another instance of this class
        else
        {
            Debug.Log("MapSelector duplicate destroyed: " + this);
            Destroy(gameObject);
        }
    }

    public static List<GameObject> LoadMap()
    {
        return instance.mapChunks;
    }

    public void SelectChunk(GameObject chunk)
    {
        mapChunks.Add(chunk);
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
