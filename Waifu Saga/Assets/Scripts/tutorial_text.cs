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

    // Start is called before the first frame update
    void Start()
    {
        timeStore = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentUI.name == "Movement Tutorial" | CurrentUI.name == "NextLevel")
        {
            MovementTutorial();
        }
        else if (CurrentUI.name == "JumpTutorial")
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
            }
        }
    }

    public void JumpTutorial(){
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space is pressed");
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                CurrentUI.SetActive(false); // Set current tutorial text to inactive
                NextUI.SetActive(true);     // Set next tutorial text to active
            }
        }
    }

}
