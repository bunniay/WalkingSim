using UnityEngine;

public class deletelater : MonoBehaviour
{
    public functiontest testScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testScript.CustomFunctions("I add in a new message");
    }
}
