using UnityEngine;

public class Scroll : MonoBehaviour, IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
