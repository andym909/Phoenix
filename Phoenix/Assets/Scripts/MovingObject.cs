/*
 * This is the base class of the Player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	[System.NonSerialized]
    public int moveTime;
    public LayerMask blockingLayer;
	public float hitDistance;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;


	// Use this for initialization
	protected virtual void Start () {
        // setup the colliders and move time
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
		moveTime = PlayerPrefs.GetInt("Speed");
	}

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit) {
        // determine whether or not we can move in the direction we are trying to
        Vector2 start = transform.position;
		Vector2 end = start + new Vector2(xDir * hitDistance, yDir * hitDistance);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if(hit.transform == null) {
            return true;
        }

        return false;
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        // if we didn't hit anything, move
		if(hit.transform == null) {
			Vector3 movement = new Vector3(xDir, yDir, 0f) * Time.deltaTime * moveTime;
			//float ix = transform.position.x;
			transform.Translate(movement);
			return;
		}

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected IEnumerator SmoothMovement(Vector3 end) {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        // as long as there is any distance remaining, keep moving
        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

	public void StopMotion() {
		StopCoroutine(SmoothMovement(Vector3.one));
	}
	
	protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
