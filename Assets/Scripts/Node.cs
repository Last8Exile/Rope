using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [HideInInspector]
    public Node NextNode;

    public HingeJoint2D HingeJoint { get { return mHingeJoint; } }
    [SerializeField]
    private HingeJoint2D mHingeJoint;

    public Rigidbody2D Rigidbody { get { return mRigidBody; } }
    [SerializeField]
    private Rigidbody2D mRigidBody;

    public void Init(Rigidbody2D attachTo)
    {
        mHingeJoint.connectedBody = attachTo;
    }

    public void Init(Node previousNode)
    {
        mHingeJoint.connectedBody = previousNode.Rigidbody;
        previousNode.NextNode = this;
    }
}
