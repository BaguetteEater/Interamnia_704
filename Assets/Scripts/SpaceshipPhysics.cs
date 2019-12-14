//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

using UnityEngine;

/// <summary>
/// Applies linear and angular forces to a ship.
/// This is based on the ship physics from https://github.com/brihernandez/UnityCommon/blob/master/Assets/ShipPhysics/ShipPhysics.cs
/// </summary>
public class SpaceshipPhysics : MonoBehaviour
{
    [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
    public Vector3 linearForce = new Vector3(100.0f, 100.0f, 100.0f);

    [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
    public Vector3 angularForce = new Vector3(1.0f, 1.0f, 1.0f);

    [Range(0.0f, 1.0f)]
    [Tooltip("Multiplier for longitudinal thrust when reverse thrust is requested.")]
    public float reverseMultiplier = 1.0f;

    [Tooltip("Multiplier for all forces. Can be used to keep force numbers smaller and more readable.")]
    private float forceMultiplier = 1f;

    public Rigidbody Rigidbody { get { return rbody; } }

    private Vector3 appliedLinearForce = Vector3.zero;
    private Vector3 appliedAngularForce = Vector3.zero;

    private Rigidbody rbody;

    // Keep a reference to the ship this is attached to just in case.
    private Spaceship ship;

    // Use this for initialization
    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
        {
            Debug.LogWarning(name + ": ShipPhysics has no rigidbody.");
        }

        ship = GetComponent<Spaceship>();
		rbody.maxAngularVelocity = 1;
    }

    void FixedUpdate()
    {
        if (rbody != null)
        {
			if (appliedLinearForce.z < 0)
			{
				this.rbody.AddRelativeForce(new Vector3(
					appliedLinearForce.x, 
					appliedLinearForce.y, 
					-this.rbody.velocity.z * (-appliedLinearForce.z)/100
				));
			} 
			else if (Mathf.Abs(this.rbody.velocity.z) < 100) 
			{
				// Max speed
				// TODO: should be abs
				rbody.AddRelativeForce(appliedLinearForce, ForceMode.Force);
			}
			
            rbody.AddRelativeTorque(appliedAngularForce * forceMultiplier, ForceMode.Force);

			//todo voir comment on tourne

        }
    }

    /// <summary>
    /// Sets the input for how much of linearForce and angularForce are applied
    /// to the ship. Each component of the input vectors is assumed to be scaled
    /// from -1 to 1, but is not clamped.
    /// </summary>
    public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput)
    {
        appliedLinearForce = MultiplyByComponent(linearInput, linearForce);
        appliedAngularForce = MultiplyByComponent(angularInput, angularForce);
    }

    /// <summary>
    /// Returns a Vector3 where each component of Vector A is multiplied by the equivalent component of Vector B.
    /// </summary>
    private Vector3 MultiplyByComponent(Vector3 a, Vector3 b)
    {
        Vector3 ret;

        ret.x = a.x * b.x;
        ret.y = a.y * b.y;
        ret.z = a.z * b.z;

        return ret;
    }
}