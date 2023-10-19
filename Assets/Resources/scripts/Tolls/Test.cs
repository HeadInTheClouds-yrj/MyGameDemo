using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int currentHearth = 1000;
    public float rectangleMaxLength = 50.9f;
    public float blackDonateLength = 10.0f;
    public float blackDonatespacing = 10f;
    public Vector2 blackDonateWidth;
    public Vector3 blackDonateScal;
    private void Awake()
    {
        blackDonateScal = GetComponent<Transform>().localScale;
        blackDonateWidth = GetComponent<SpriteRenderer>().size;
    }
    private void Update()
    {
        blackDonateWidth.x = currentHearth * 0.001f;
        blackDonateScal.x = rectangleMaxLength/ (blackDonateWidth.x * 100);

        
        GetComponent<Transform>().localScale = blackDonateScal;
        GetComponent<SpriteRenderer>().size = blackDonateWidth;
    }
}
