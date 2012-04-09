using UnityEngine;

using Exception = System.Exception;

public sealed class Neoclassical : Base.Building
{
  /*************** FIELDS ***************/

  private const float _componentWidthMin = 1.4f;
  private const float _componentWidthMax = 1.6f;
  private const float _componentSpaceMin = 2f;
  private const float _componentSpaceMax = 2.25f;

  public float windowHeight;

  public float doorHeight;

  public float balconyHeight;

  /*************** CONSTRUCTORS ***************/
  
  /// <summary>
  /// Initializes a new instance of the <see cref="Neoclassical"/> class.
  /// The boundaries of the base of this building must be given in 
  /// clockwise order.
  /// </summary>
  /// <param name='p1'>
  /// A point in space.
  /// </param>
  /// <param name='p2'>
  /// A point in space.
  /// </param>
  /// <param name='p3'>
  /// A point in space.
  /// </param>
  /// <param name='p4'>
  /// A point in space.
  /// </param>
  public Neoclassical (Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    : base(p1, p2, p3, p4)
  {
    floorHeight = Random.Range(4.25f, 4.75f);
    floorNumber = Util.RollDice(new float[] {0.15f, 0.7f, 0.15f});

    windowHeight = Random.Range(2.1f, 2.3f);
    doorHeight = Random.Range(3.5f, 3.7f);

    //float height_modifier = floorHeight / 2 - windowHeight / 2;
    balconyHeight = windowHeight / 2 + floorHeight / 2;

    ConstructFaces();
    ConstructFaceComponents();

    Draw();
    CombineWindowFrames();
    CombineWindowGlasses();
    SetActiveRecursively(true);
  }
  
  
  /*************** METHODS ***************/

  public void ConstructFaceComponents ()
  {
    if (faces.Count == 0) throw new Exception("There are no faces to construct the components.");
  
    float component_width = Random.Range(_componentWidthMin, _componentWidthMax);
    float inbetween_space = Random.Range(_componentSpaceMin, _componentSpaceMax);
  
    foreach (Base.Face face in faces)
      face.ConstructFaceComponents(component_width, inbetween_space);
  }

  public override void ConstructFaces ()
  {
    faces.Add(new NeoclassicalFace(this, boundaries[0], boundaries[1]));
    faces.Add(new NeoclassicalFace(this, boundaries[1], boundaries[2]));
    faces.Add(new NeoclassicalFace(this, boundaries[2], boundaries[3]));
    faces.Add(new NeoclassicalFace(this, boundaries[3], boundaries[0]));

    this.sortedFaces = this.GetSortedFaces();
  }
}
