using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNext : MonoBehaviour
{
    [SerializeField] GameObject g;

    public void Active()
    {
        g.SetActive(true);
    }
}
