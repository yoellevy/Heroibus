using UnityEngine;
using System.Collections;

public class InfoText : MonoBehaviour
{

    [SerializeField]
    private float destroyTime;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, this.destroyTime);
    }
}