using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour {


	//prefabs of the link
	public Transform Prefab;

	//
	public int MaxLength=10;

	//
	public int MinLenght=2;

	//Array of the links forming the chain
	private Transform[] Sections;

	// Current size of the chain
	private int Length;

	// Current link which is fixed to the chick
	private int CurrentAnchor;
	
	// Use this for initialization
	void Start () {
		Length = MaxLength;
		CurrentAnchor = 0;
		Sections = new Transform[MaxLength];
		var pos = transform.position;
		var collider = transform.GetComponent<CircleCollider2D>();	
		for (int i=0; i<MaxLength; i++) {
			var s = Instantiate(Prefab) as Transform;
			s.position = pos;
			var join = s.GetComponent<HingeJoint2D>();
			if(i>0){
				join.connectedBody = Sections[i-1].rigidbody2D;
			}
			join.anchor = new Vector2(0,collider.radius/2);
			join.connectedAnchor = new Vector2(0, -collider.radius/2);
			Sections[i]=s;
			pos.y += collider.radius*2;

		}
	}



	
	// Update is called once per frame
	void Update () {
	
	}

	//private methods
	private bool changeChainLength(int delta){
		if (delta > 0 && Length < MaxLength) { // the chain grows
			var section = Sections [CurrentAnchor];
			Length++;
			CurrentAnchor--;
			var nsection = Sections [CurrentAnchor];
			changeStateSection (section, true);
			nsection.GetComponent<HingeJoint2D> ().connectedBody = section.GetComponent<DistanceJoint2D> ().connectedBody;
			section.GetComponent<HingeJoint2D> ().connectedBody = nsection.rigidbody2D;
		} else if (delta < 0 && Length > MinLenght) {
			var section = Sections [CurrentAnchor];
			Length--;
			CurrentAnchor++;
			var nsection = Sections [CurrentAnchor];
			changeStateSection (section, false);
			nsection.GetComponent<HingeJoint2D> ().connectedBody = section.GetComponent<DistanceJoint2D> ().connectedBody;
			section.GetComponent<HingeJoint2D> ().connectedBody = nsection.rigidbody2D;
		} else {
			return false;
		}
		return true;	
	}

	private void changeStateSection(Transform s, bool state){
		s.GetComponent<HingeJoint2D> ().enabled = state;
		s.collider2D.enabled = state;
		//s.rigidbody2D.enabled = state;	
	}



}
