using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	//initialize mesh and material
	//public Mesh mesh;
	public Material material;
	public int maxDepth;
	private int depth;
	public float childScale;

    private Material[,] materials;

	public Mesh[] meshes;

	public float spawnProbablity;

	public float maxRotationSpeed;

	private float rotationSpeed;
	public float maxTwist;

	private void InitializeMaterials()
		{
			materials = new Material[maxDepth+1,2];
			for (int i = 0; i <= maxDepth; i++) {
				float t = i / (maxDepth - 1f);
				t *= t;
				materials[i,0] = new Material (material);
				materials[i,0].color= Color.Lerp (Color.white, Color.yellow, t);
				materials[i,1] = new Material (material);
				materials[i,1].color= Color.Lerp (Color.white, Color.black, t);
			}
		materials [maxDepth,0].color = Color.yellow;
		materials [maxDepth,1].color = Color.black;
		}

	private void Start()
	{
		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);
		transform.Rotate (Random.Range (-maxTwist, maxTwist), 0f, 0f);
		if (materials == null) {
			InitializeMaterials();
		}
		gameObject.AddComponent<MeshFilter>().mesh =
			meshes[Random.Range(0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = 
		materials[depth, Random.Range (0, 2)];
		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}
	private static Vector3[] childDirections ={
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.down
	};

	private static Quaternion[] childOrientation ={
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(180f,0f, 0f)
	};
	private IEnumerator CreateChildren()
	{
		for (int i = 0; i < childDirections.Length; i++) {
			if (Random.value < spawnProbablity) {
				yield return new WaitForSeconds (Random.Range (0.1f, 0.5f));
				new GameObject ("FractalChild").AddComponent<Fractal> ().
			Initialize (this, i);
			}
		}
	}
	private void Initialize(Fractal parent, int childIndex)
	{
		maxTwist = parent.maxTwist;
		maxRotationSpeed = parent.maxRotationSpeed;
		spawnProbablity = parent.spawnProbablity;
		meshes = parent.meshes;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;

		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition =
			childDirections [childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation =
			childOrientation [childIndex];


	}

	private void Update()
	{
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}

}
