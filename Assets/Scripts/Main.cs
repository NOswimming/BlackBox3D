using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public Material DefaultMaterial;

    GameObject[,,] grid;
    GameObject[,,,] gridArrows;

    GameObject mainCamera;

    enum TAG
    {
        GRID_SPHERE,
        GRID_ARROW
    }

    enum GRID_ARROW_DIRECTION
    {
        X_MIN,
        X_MAX,
        Y_MIN,
        Y_MAX,
        Z_MIN,
        Z_MAX,
    }

    // Use this for initialization
    void Start () {
        int GX = 3, GY = 3, GZ = 3;
        float spacing = 2.0f;
        float spacingSQD = spacing * spacing;
        float distance = Mathf.Sqrt(GX * GX * spacingSQD + GY * GY * spacingSQD + GZ * GZ * spacingSQD);

        CreateCamera(new Vector3(GX / 2 * spacing, GY / 2 * spacing, GZ / 2 * spacing), distance);
        CreateGrid(GX, GY, GZ, spacing);
        

        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform.tag.StartsWith(TAG.GRID_SPHERE.ToString()))
                {
                    print("Clicked:" + hitInfo.transform.gameObject.name);
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.2f);
                }

                if (hitInfo.transform.tag.StartsWith(TAG.GRID_ARROW.ToString()))
                {
                    print("Clicked:" + hitInfo.transform.gameObject.name);
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.2f);
                }

            }
        }
    }

    void CreateGrid(int gridSizeX, int gridSizeY, int gridSizeZ, float spacing)
    {
        grid = new GameObject[gridSizeX, gridSizeY, gridSizeZ];
        gridArrows = new GameObject[gridSizeX, gridSizeY, gridSizeZ, 6];

        // Add spheres
        int x = 0, y = 0, z = 0;

        for (x = 0; x < gridSizeX; x++)
        {


            for (y = 0; y < gridSizeY; y++)
            {


                for (z = 0; z < gridSizeZ; z++)
                {
                    // Create Grid Sphere
                    var gridSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gridSphere.name = string.Format("{0}[{1},{2},{3}]", TAG.GRID_SPHERE.ToString(), x, y, z);
                    gridSphere.transform.tag = TAG.GRID_SPHERE.ToString();
                    gridSphere.transform.position = new Vector3(x * spacing, y * spacing, z * spacing);
                    var gridSphereRenderer = gridSphere.GetComponent<Renderer>();
                    gridSphereRenderer.material = new Material(DefaultMaterial);
                    gridSphereRenderer.material.color = new Color(1, 1, 1, 0.1f);

                    grid[x, y, z] = gridSphere;
                }
            }
        }

        for (x = 0; x < gridSizeX; x++)
        {


            for (y = 0; y < gridSizeY; y++)
            {
                // Add arrows Z
                CreateGridArrow(new Vector3(x * spacing, y * spacing, -spacing), new Vector3(90, 0, 0),
                    new Vector4(x, y, 0, GRID_ARROW_DIRECTION.Z_MIN.GetHashCode()));
                CreateGridArrow(new Vector3(x * spacing, y * spacing, gridSizeZ * spacing), new Vector3(90, 0, 0),
                    new Vector4(x, y, gridSizeZ-1, GRID_ARROW_DIRECTION.Z_MAX.GetHashCode()));
            }


            for (z = 0; z < gridSizeZ; z++)
            {
                // Add arrows Y
                CreateGridArrow(new Vector3(x * spacing, -spacing, z * spacing), new Vector3(),
                    new Vector4(x, 0, z, GRID_ARROW_DIRECTION.Y_MIN.GetHashCode()));
                CreateGridArrow(new Vector3(x * spacing, gridSizeY * spacing, z * spacing), new Vector3(),
                    new Vector4(x, gridSizeY-1, z, GRID_ARROW_DIRECTION.Y_MAX.GetHashCode()));


            }
        }

        for (y = 0; y < gridSizeY; y++)
        {
            for (z = 0; z < gridSizeZ; z++)
            {
                // Add arrows X
                CreateGridArrow(new Vector3(-spacing, y * spacing, z * spacing), new Vector3(0, 0, 90),
                    new Vector4(0, y, z, GRID_ARROW_DIRECTION.X_MIN.GetHashCode()));
                CreateGridArrow(new Vector3(gridSizeX * spacing, y * spacing, z * spacing), new Vector3(0, 0, 90),
                    new Vector4(gridSizeX-1, y, z, GRID_ARROW_DIRECTION.X_MAX.GetHashCode()));
            }
        }
    }


        

    void CreateGridArrow(Vector3 position, Vector3 rotation, Vector4 arrowTarget)
    {
        var gridArrow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        gridArrow.name = string.Format("{0}[{1},{2},{3},{4}] ({5})", TAG.GRID_ARROW.ToString(), arrowTarget.x, arrowTarget.y, arrowTarget.z, arrowTarget.w, (GRID_ARROW_DIRECTION)arrowTarget.w);
        gridArrow.transform.tag = TAG.GRID_ARROW.ToString();
        gridArrow.transform.position = position;
        gridArrow.transform.Rotate(rotation);
        gridArrow.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        var gridArrowRenderer = gridArrow.GetComponent<Renderer>();
        gridArrowRenderer.material = new Material(DefaultMaterial);
        gridArrowRenderer.material.color = new Color(1, 1, 1, 0.1f);

        gridArrows[(int) arrowTarget.x, (int) arrowTarget.y, (int) arrowTarget.z, (int) arrowTarget.w] = gridArrow;
    }

    GameObject CreateCamera (Vector3 targetPosition, float distance)
    {
        mainCamera = new GameObject("Camera");
        mainCamera.AddComponent<Camera>();
        mainCamera.AddComponent<DragMouseOrbit>();

        // Add target
        var target = new GameObject("CameraTarget");
        target.transform.position = targetPosition;
        mainCamera.GetComponent<DragMouseOrbit>().target = target.transform;

        mainCamera.GetComponent<DragMouseOrbit>().distance = distance;
        return mainCamera;
    }
}
