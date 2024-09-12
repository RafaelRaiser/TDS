using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation anime = GetComponent<Animation>();

        anime.Play("Outer_Door_Open");
    }

}
