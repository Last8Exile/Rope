using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowMouse : MonoBehaviour
{

    public float Speed = 0.5f;

    [HideInInspector]
    public Rope mRope;

    private Rigidbody mRigidbody;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
	{
	    if (mRope.CursorPlane == null || mRope.Anchor == null)
	        return;
	    if (!Input.GetMouseButton(0))
	    {
	        mRigidbody.isKinematic = false;
	        return;
	    }

	    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var planePos = mRope.CursorPlane.position;
	    var planeNormal = mRope.CursorPlane.forward;
	    var anchorPos = mRope.Anchor.position;

        var rayDistToPlane = (planePos - ray.origin).Multiply(planeNormal).Sum()/planeNormal.Multiply(ray.direction).Sum();
	    var cursorPos = ray.origin + ray.direction * rayDistToPlane;

	    var direction = (cursorPos - anchorPos).normalized;
	    var distToAnchor = (cursorPos - anchorPos).magnitude;
	    var clampedPosition = anchorPos + direction * Mathf.Clamp(distToAnchor, 0, mRope.Length);
        

        mRigidbody.isKinematic = true;
	    mRigidbody.MovePosition(Vector3.MoveTowards(mRigidbody.position, clampedPosition, Speed));
	}
}
