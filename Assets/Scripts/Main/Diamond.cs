using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    private Transform trans;
    private float speed = 60;

    // Start is called before the first frame update
    void Start() {
        trans = this.transform;
    }

    // Update is called once per frame
    void Update() {
        trans.Rotate(Vector3.up * speed * Time.unscaledDeltaTime, Space.Self);
    }
}
