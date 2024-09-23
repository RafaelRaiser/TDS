using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] string nomePorta;

    public string NomeAnimation()
    {
        return nomePorta;
    }
}
