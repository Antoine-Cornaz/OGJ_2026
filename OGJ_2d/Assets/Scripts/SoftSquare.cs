using UnityEngine;

public class SoftSquare : SoftBody
{
    public int gridSize;

    public override void InitMassPoints()
    {
        massPoints = new SoftBodyMassPoint[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject massPointObj = Instantiate(massPointPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
                SoftBodyMassPoint massPoint = massPointObj.AddComponent<SoftBodyMassPoint>();
                massPoints[i, j] = massPoint;
            }
        }
    }

    public override void CreateSprings()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (i < gridSize - 1) CreateSpring(massPoints[i, j], massPoints[i + 1, j]);
                if (j < gridSize - 1) CreateSpring(massPoints[i, j], massPoints[i, j + 1]);
            }
        }
    }
}
