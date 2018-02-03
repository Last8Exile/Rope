using UnityEngine;

public class Rope : MonoBehaviour {

    [SerializeField]
    private GameObject mHeadNodePrefab = null, mNodePrefab = null, mEndPrefab = null;
    [SerializeField]
    private float mNodeLength;


    public Rigidbody Anchor { get { return mAnchor; } }
    [SerializeField]
    private Rigidbody mAnchor;

    public Transform CursorPlane { get { return mCursorPlane; } }
    [SerializeField]
    private Transform mCursorPlane;

    public int NodeCount;
    public float Length { get { return mNodeLength * (NodeCount + 1.75f + mOffset); } }

    private Node mHeadNode;

    public float Speed;
    private float mOffset;

	// Use this for initialization
	void Start ()
	{
	    if (mAnchor == null || mHeadNodePrefab == null || mNodePrefab == null)
	    {
	        Debug.Log("One of node prefabs not specified");
	        return;
	    }
	    if (NodeCount < 0)
	    {
	        Debug.Log("Node count can't be negative");
	        return;
        }
	    if (mCursorPlane == null)
	    {
	        Debug.Log("Mouse plane not specified");
	        return;
	    }

	    var pos = mAnchor.position + Vector3.down * (mNodeLength / 2);
	    mHeadNode = Instantiate(mHeadNodePrefab, pos, Quaternion.identity, transform).GetComponent<Node>();
	    mHeadNode.Init(mAnchor);
	    pos += Vector3.down * mNodeLength;

	    var lastNode = mHeadNode;
	    for (int i = 0; i < NodeCount; i++)
	    {
	        var node = Instantiate(mNodePrefab, pos, Quaternion.identity, transform).GetComponent<Node>();
	        node.Init(lastNode);
	        lastNode = node;
	        pos += Vector3.down * mNodeLength;
        }

	    var end = Instantiate(mEndPrefab, pos, Quaternion.identity, transform).GetComponent<Node>();
	    end.Init(lastNode);
	    var followMouse = end.GetComponent<FollowMouse>();
        followMouse.mRope = this;
	}

    void FixedUpdate()
    {
        var delta = Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            mOffset += Speed * delta;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mOffset -= Speed * delta;
        }
        if (mOffset > 0)
        {
            mOffset--;
            NodeCount++;
            var node = Instantiate(mNodePrefab, mHeadNode.transform.position, mHeadNode.transform.rotation, transform).GetComponent<Node>();
            mHeadNode.NextNode.Init(node);
            node.Init(mHeadNode);
        }
        if (mOffset <= -1)
        {
            if (NodeCount == 0)
            {
                mOffset = -1;
            }
            else
            {
                mOffset++;
                NodeCount--;
                var node = mHeadNode.NextNode;
                node.NextNode.Init(mHeadNode);
                Destroy(node.gameObject);
            }

        }
        mHeadNode.HingeJoint.anchor = -Vector3.up * (mOffset * -0.3f - 0.3f);
    }
}
