using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour, ITrajectoryRenderer
{
    [SerializeField] private GameObject trajectoryPointPrefab; 
    [SerializeField] private int numberOfPoints = 50; 
    [SerializeField] private float spaceBetweenPoints = 0.1f; 
    [SerializeField] private float gravity = 9.81f;

    private GameObject[] trajectoryPoints; 
    private bool isVisible = false; 

    private void Start()
    {
        trajectoryPoints = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, transform);
            trajectoryPoints[i].SetActive(false);
        }
    }

    public void ShowTrajectory(Vector3 origin, Vector2 direction, float initialForce)
    {
        if (!isVisible)
        {
            foreach (var point in trajectoryPoints)
            {
                point.SetActive(true);
            }
            isVisible = true;
        }
        UpdateTrajectoryPoints(origin, direction, initialForce);
    }

    public void UpdateTrajectory(Vector3 origin, Vector2 direction,float force)
    {
        if (isVisible)
        {
            UpdateTrajectoryPoints(origin, direction, force); 
        }
    }

    public void HideTrajectory()
    {
        if (isVisible)
        {
            foreach (var point in trajectoryPoints)
            {
                point.SetActive(false);
            }
            isVisible = false;
        }
    }

    private void UpdateTrajectoryPoints(Vector3 origin, Vector2 direction, float initialForce)
    {
        Vector2 velocity = direction.normalized * initialForce;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * spaceBetweenPoints;
            float xPosition = origin.x + velocity.x * time;
            float yPosition = origin.y + velocity.y * time - (0.5f * gravity * time * time);
            trajectoryPoints[i].transform.position = new Vector3(xPosition, yPosition, origin.z);
        }
    }
}

