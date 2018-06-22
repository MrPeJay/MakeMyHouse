using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour {

    public int Width;
    public int Length;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
