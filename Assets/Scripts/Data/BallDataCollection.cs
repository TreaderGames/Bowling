using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BallDataCollection", menuName = "ScriptableObjects/BallDataCollection", order = 1)]
[Serializable]
public class BallDataCollection : ScriptableObject
{
    [Serializable]
    public enum MaterialType
    {
        Metal,
        Rubber
    }

    [Serializable]
    public struct BallData
    {
        public MaterialType materialType;
        public PhysicMaterial physicMaterial;
    }

    [SerializeField] BallData[] ballDatas;

    public BallData GetBallData(MaterialType materialType)
    {
        return Array.Find(ballDatas, e => e.materialType.Equals(materialType));
    }
}
