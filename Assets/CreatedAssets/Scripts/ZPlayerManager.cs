using UnityEngine;

public class ZPlayerManager : MonoBehaviour
{
    public static ZPlayerManager instance;
    public Transform player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
