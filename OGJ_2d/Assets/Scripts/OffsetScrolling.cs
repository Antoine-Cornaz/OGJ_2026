using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;


public class OffsetScrolling : MonoBehaviour {
    [SerializeField] private GameObject ToFollow;
    public float scrollSpeed;

    
    void Start () {

    }

    void Update () {
        transform.position = new Vector3(ToFollow.transform.position.x*0.8f+25, transform.position.y, transform.position.z);
    }
}
