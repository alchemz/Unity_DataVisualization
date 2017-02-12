using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid: MonoBehavior{
  public int xSize, ySize;
  public Vector3[] vertices;
  private Mesh mesh;
  
  public void Awake()
  {
    StartCoroutine(Generate());
  }
  
  private IEnumerator Generate()
  {
    WaitForSeconds wait = new WaitForSeconds(0.05f);
    GetComponent<MeshFilter>().mesh= mesh = new Mesh();
    mesh.name= "Procedural Grid";
    vertices = new Vector3[(xSize+1)*(ySize+1)];
    for(int i=0, y=0; y<=ySize; y++){
      for(int x=0; x<=xSize; x++,i++)
      {
        vertices[i]= new Vector3(x,y);
        yield return wait;
      }
    }
    mesh.vertices = vertices;
    int [] triangles = new int[3];
    triangles[0]=0;
    triangles[1]=1;
    triangles[2]=xSize+1;
    mesh.triangles= triangles;
    
  }
  private void Generate()
  {
    vertices = new Vector3[(xSize +1)* (ySize+1)];
    for(int i=0, y=0; y<=ySize; y++)
    {
      for(int x=0; x<=xSize; x++, i++)
      {
        vertices[i]= new Vector3(x,y);
      }
    }
  }
  
  //gizmos is for showing the meshes
   private void OnDrawGizmos()
   {
     if(vertices == null)
     {
       return;
     }
     Gizmos.color = Color.black;
    for(int i=0; i<vertices.Length; i++)
    {
      Gizmos.DrawSphere(vertices[i],0.1f);
    }
   }
    
    
}
