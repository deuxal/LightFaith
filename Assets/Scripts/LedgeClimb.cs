using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb: MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float timeToReachPoint1 = 2f;
    public float timeToReachPoint2 = 2f;

    private bool isMoving = false;
    private Transform player;
    private System.Action endAction;

    public void MoveToPoint(Transform player, System.Action startAction, System.Action endAction)
    {
        this.endAction = endAction;
        this.player = player;
        if (!isMoving && Input.GetKey(KeyCode.LeftControl))
        {
            
            startAction();
            StartCoroutine(MoveToPoint(point1.position, timeToReachPoint1, MoveToPoint(point2.position, timeToReachPoint2, EndAction())));
        }
    }

    IEnumerator MoveToPoint(Vector3 targetPosition, float timeToReachTarget, IEnumerator next)
    {
        isMoving = true;
        Vector3 startPosition = player.position;
        float elapsedTime = 0f;

        while (elapsedTime < timeToReachTarget)
        {
            player.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / timeToReachTarget));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.position = targetPosition;
        isMoving = false;
        if(next != null) { 
            yield return next;
        }

    }

    IEnumerator EndAction()
    {
        yield return 0;
        endAction();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<ObjectMovement>().lc = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<ObjectMovement>().lc = null;
        }
    }
}


