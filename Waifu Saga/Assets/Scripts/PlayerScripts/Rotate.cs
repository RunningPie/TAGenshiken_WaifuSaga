using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
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
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        horizontalMoveInput = Input.GetAxis("Horizontal");
        if (horizontalMoveInput > 0.01f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalMoveInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
