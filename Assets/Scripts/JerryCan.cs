using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryCan : MonoBehaviour
{
public void JerryCanBang()
    {
        Destroy(gameObject);
        Debug.Log("Bang-bang! motherfucker");
    }
}
