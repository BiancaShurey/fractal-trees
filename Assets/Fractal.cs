using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh mesh;
    public Material material;

    public int maxDepth;
    public int forks;

    private int depth;

    private void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private static Vector3[] childDirections = {
        new Vector3(1, 1, 0),
        new Vector3(-1,1,0)
    };

    private static Quaternion[] childOrientations = {
        Quaternion.Euler(0f, 0f, -20f),
        Quaternion.Euler(0f, 0f, 20f)
    };

    private IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").AddComponent<Fractal>().
                Initialize(this, i);
        }
    }


    public float childScale;
    private float foo;

    private void Initialize(Fractal parent, int childIndex)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        foo = 0.5f * childScale;
        transform.localPosition = Vector3.Scale(childDirections[childIndex],new Vector3(0.15f+foo,1.1f+foo,0f+foo));
        transform.localRotation = childOrientations[childIndex];

    }

    // Update is called once per frame
    void Update () {
		
	}
}
