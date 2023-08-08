using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNStone : MonoBehaviour
{
    Vector3 spawnVector;
    // Start is called before the first frame update
    void Start()
    {
        float yPos = -4.0725f;
        spawnVector = this.transform.localPosition;

        if(this.name =="Tree")
        {
            yPos = -4.0725f;
        }
        else
        {
            yPos = -4.3f;
        }

        spawnVector.y = yPos;
        this.transform.localPosition = spawnVector;
    }
}
