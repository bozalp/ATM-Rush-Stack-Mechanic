using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cubes;
    [SerializeField]
    private float swingValue = .3f;
    [SerializeField]
    private float forwardSpeed, swipeSpeed, rightMovementLimitPos, leftMovementLimitPos;

    private GameObject currCube, prevCube;
    private Vector3 inputDrag, preMousePos;

    public void AddCube(GameObject collectedCube)
    {
        cubes.Add(collectedCube);
        collectedCube.transform.parent = gameObject.transform;
        collectedCube.transform.localPosition = new Vector3(cubes[cubes.Count - 2].transform.localPosition.x, 0,
            cubes[cubes.Count - 2].transform.localPosition.z + 1.1f);
    }
    private void Update()
    {        
        MoveHorizontal();
    }
    void FixedUpdate()
    {
        Swipe();
        SwipeOnKeyboard();
        MoveForward();    
        SwingMovement();
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
    }

    private void SwingMovement()
    {
        for (int i = 1; i < cubes.Count; i++)
        {
            prevCube = cubes[i - 1];
            currCube = cubes[i];

            Vector3 newPos;
            newPos.x = prevCube.transform.localPosition.x;
            newPos.y = cubes[0].transform.localPosition.y;// root
            newPos.z = cubes[i].transform.localPosition.z;

            currCube.transform.localPosition = Vector3.Lerp(currCube.transform.localPosition, newPos, swingValue);
        }
    }
    private void MoveHorizontal()
    {
        var currentPos = transform.localPosition;
        var dragPos = Vector3.right * inputDrag.x * swipeSpeed * Time.deltaTime;

        if (cubes[0].transform.position.x > rightMovementLimitPos)
        {
            cubes[0].transform.position = new Vector3(rightMovementLimitPos - .02f, 0, 0);
        }
        if (cubes[0].transform.position.x < leftMovementLimitPos)
        {
            cubes[0].transform.position = new Vector3(leftMovementLimitPos + .02f, 0, 0);
        }
        else
        {
            cubes[0].transform.position += dragPos;
        }
    }
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            preMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = Input.mousePosition - preMousePos;
            inputDrag = deltaMouse;
            preMousePos = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector3.zero;
        }
    }

    private void SwipeOnKeyboard()
    {
        if (Input.GetKey(KeyCode.A))
        {
            cubes[0].transform.position += new Vector3(-swipeSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cubes[0].transform.position += new Vector3(swipeSpeed * Time.deltaTime, 0, 0);
        }
    }
}
