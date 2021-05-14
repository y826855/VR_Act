using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollower : MonoBehaviour
{
    public GameObject _target = null;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _target.transform.position;
    }
}
