using UnityEngine;
using Thesis;
using System.Linq;

public class CityMapController : MonoBehaviour {

  private Block block;

  void Start ()
  {
    // the point Vector3.zero must _not_ be used
    // as starting point and all 4 points must be
    // in the first quadrant
    block = new Block(new Vector3(1f, 0f, 0f),
                      new Vector3(0f, 0f, 300f),
                      new Vector3(500f, 0f, 300f),
                      new Vector3(500f, 0f, 0f));
    block.Bisect();
  }

  void Update ()
  {
    foreach (Block b in CityMapManager.Instance.blocks)
    {
      foreach (BuildingLot l in b.finalLots)
        foreach (Edge e in l.edges)
          Debug.DrawLine(e.start, e.end, Color.cyan);

      foreach (Edge e in b.edges)
        Debug.DrawLine(e.start, e.end, Color.green);
    }

    if (Input.GetKeyUp(KeyCode.B))
    {
      AddBuildings();
      foreach (Block b in CityMapManager.Instance.blocks)
        b.Draw();
    }
  }

  private void AddBuildings ()
  {
    foreach (Block b in CityMapManager.Instance.blocks)
      foreach (BuildingLot l in b.finalLots)
        BuildingManager.Instance.Build(l);
  }
}