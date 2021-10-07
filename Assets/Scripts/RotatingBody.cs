using UnityEngine;

public class RotatingBody : MonoBehaviour
{
	private Rigidbody2D body;
	private float appliedTorque;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();
	}

	public void ApplyTorque(float torque)
	{
		this.appliedTorque = torque;
	}


	private void FixedUpdate()
	{
		this.body.rotation += appliedTorque;
	}
}
