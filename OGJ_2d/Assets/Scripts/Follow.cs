using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
    
    [SerializeField] private GameObject ToFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (ToFollow == null)
            ToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ToFollow.transform.position.x, ToFollow.transform.position.y + 4, transform.position.z);
    }
}
