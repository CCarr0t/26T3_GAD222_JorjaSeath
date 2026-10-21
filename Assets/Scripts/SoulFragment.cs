using System;
using UnityEngine;

public class SoulFragment : MonoBehaviour, IItem
{
    public static event Action<int> OnSoulCollect;
    public int worth = 5;

    private bool isCollected = false;

    public void Collect()
    {
        Debug.Log("Gem collected!");

        if (isCollected) return;

        isCollected = true;

        OnSoulCollect?.Invoke(worth);
        Destroy(gameObject);
    }
}
