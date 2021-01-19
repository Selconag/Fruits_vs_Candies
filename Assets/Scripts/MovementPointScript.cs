using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPointScript : MonoBehaviour
{
    [Header("PathList")]

    public GameObject FinishPoint;
    public List<Transform> movePoints = new List<Transform>();
    public GameObject[] movePointsObject;
    Transform MovePoint;
    
    public List<string> GetMoveNames = new List<string>();
    

    public List<Transform> MoveSpotsFinder()
    {
        movePointsObject = GameObject.FindGameObjectsWithTag("PathPoint");                                      //Array'a (movePointsObject'e) ekledik tüm movepointleri...
        for (int a = 0; a < movePointsObject.Length; a++)
        {
            MovePoint = movePointsObject[a].GetComponent<Transform>();
            GetMoveNames.Add(movePointsObject[a].name);
            movePoints.Add(movePointsObject[a].transform);
        }

        GetMoveNames.Sort();

        for (int b = 0; b < GetMoveNames.Count-1; b++)
        {
            if (GetMoveNames[b] == GetMoveNames[b + 1] && b<GetMoveNames.Count)
            {
                GetMoveNames.RemoveAt(b);
            }

        }
        movePoints.Add(FinishPoint.transform);
        return movePoints;
    }   
    
    public List<Transform> MoveListChecker()
    {
        return movePoints;
    }
}
