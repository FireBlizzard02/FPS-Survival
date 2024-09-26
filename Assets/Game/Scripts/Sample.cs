using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private void Awake(){
        Debug.Log(" Awake Command .");
    }

    private void OnEnable()
    {
        Debug.Log("Enable Command .");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
