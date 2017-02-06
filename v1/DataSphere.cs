using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class DataSphere : MonoBehaviour {

    public ParticleSystem system;

	void Start()
	{
	}

	void Update()
	{
		for(int i; i<10; i++)
		{
		CreateDataPoint(Random.value, Random.Range(-1f,1f);
		}
	}

	public void CreateDataPoint(float yaw, float pitch)
	{
		var dataPosition = new Vector3 (Mathf.Sin (yaw * Mathf.PI) * Mathf.Cos (pitch * Mathf.PI), 
			                   	Mathf.Sin (pitch * Mathf.PI),
			                        Mathf.Cos (yaw * Mathf.PI) * Mathf.Cos (pitch * Mathf.PI));
		system.Emit (new ParticleSystem.EmitParams () {
			position = dataPosition,
			startColor = new Color((1+dataPosition.x)*0.5f,(1+dataPosition.y)*0.5f,(1+dataPosition.z)*0.5f)
		}, 1);
	}
}
