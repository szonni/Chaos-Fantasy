using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drop
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    public List<Drop> drops;

    // Call this method whenever an enemy dies
    void OnDestroy()
    {
        float cumulatedChance = 0f;
        foreach (Drop rate in drops)
        {
            cumulatedChance += rate.dropRate;
        }
        float randNum = UnityEngine.Random.Range(0f, cumulatedChance);
        
        foreach (Drop item in drops)
        {
            if (randNum <= item.dropRate)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                break;
            }
            else
            {
                randNum -= item.dropRate;
            }
        }
    }
}