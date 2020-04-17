using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalsKeeper : MonoBehaviour
{
    public static GlobalsKeeper GK;
    public int playerScore;
    void Awake()
    {
        if (GK != null)
        {
            GameObject.Destroy(GK);
        }
        else
        {
            GK = this;
        }
    }
}
