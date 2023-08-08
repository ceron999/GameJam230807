using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    int min = 10;
    [SerializeField]
    int max = 20;
    float randomY = 0;

    Vector3 spawnVector;
    // Start is called before the first frame update
    void Start()
    {
        spawnVector = this.transform.localPosition;
        randomY = Random.Range(min, max);
        spawnVector.y = randomY;
        this.transform.localPosition = spawnVector;
    }

}
