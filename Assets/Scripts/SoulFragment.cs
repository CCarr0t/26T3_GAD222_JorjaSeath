using System;
using UnityEngine;

public class SoulFragment : MonoBehaviour, IItem
{
    public static event Action<int> OnSoulCollect;
    public int worth = 5;

    public void Collect()
    {
        OnSoulCollect.Invoke(worth);
        Destroy(gameObject);
    }
}
