using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTypes : MonoBehaviour
{
    // Start is called before the first frame update
    public enum OrbType {Fire, Water, Lightning, Earth, FakeFire, FakeLightning, FakeWater, FakeEarth };
    public OrbType type;
}
