using UnityEngine;

public class UIConctrllo : UIBase
{
    // Start is called before the first frame update
    void Onclick()
    {
        Debug.Log(transform.name);
    }
    void Start()
    {
        addlicen("Image_I", Onclick);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
