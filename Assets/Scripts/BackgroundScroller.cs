using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public Transform BG1, BG2;
    public float scrollSpeed;


    private float bgWidth;


    // Start is called before the first frame update
    void Start()
    {
        bgWidth = BG1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        BG1.position = new Vector3(BG1.position.x - (scrollSpeed * Time.deltaTime), BG1.position.y, BG2.position.z);
    }
}
