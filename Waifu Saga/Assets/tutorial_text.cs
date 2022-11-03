using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_text : MonoBehaviour
{
    public GameObject CurrentUI;
    public GameObject NextUI;
    private float horizontalMoveInput;
    public float time;
    private float timeStore;
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        timeStore = time;
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(step);
        if (step == 0)
        {
            MovementTutorial();
        }
        else if (step == 1)
        {
            JumpTutorial();
        }
    }

    public void MovementTutorial(){

        horizontalMoveInput = Input.GetAxis("Horizontal");
        if (horizontalMoveInput != 0)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                CurrentUI.SetActive(false); // Set current tutorial text to inactive
                NextUI.SetActive(true);     // Set next tutorial text to active
                step += 1;
            }
        }
    }

    public void JumpTutorial(){
        if (Input.GetButtonDown("Jump"))
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                CurrentUI.SetActive(false); // Set current tutorial text to inactive
                NextUI.SetActive(true);     // Set next tutorial text to active
                step += 1;
            }
        }
    }
}
