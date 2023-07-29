using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObejct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", 2f);
    }

    // Update is called once per frame
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
