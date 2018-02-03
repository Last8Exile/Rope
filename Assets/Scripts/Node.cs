using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [HideInInspector]
    public Node NextNode;

    public HingeJoint HingeJoint { get { return mHingeJoint; } }
    [SerializeField]
    private HingeJoint mHingeJoint;

    public Rigidbody Rigidbody { get { return mRigidBody; } }
    [SerializeField]
    private Rigidbody mRigidBody;

    public void Init(Rigidbody attachTo)
    {
        mHingeJoint.connectedBody = attachTo;
    }

    public void Init(Node previousNode)
    {
        mHingeJoint.connectedBody = previousNode.Rigidbody;
        previousNode.NextNode = this;
    }
}
