using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newRotate : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Vector3 mousePos;
    private float horizontalMoveInput;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - playerPos.x;
        mousePos.y = mousePos.y - playerPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        horizontalMoveInput = Input.GetAxis("Horizontal");
        if (horizontalMoveInput > 0.01f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        }
        else if (horizontalMoveInput < -0.01f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(-180f, 0f, -angle));
        }
    }
}
