using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public Chunk[] chunks;
}
