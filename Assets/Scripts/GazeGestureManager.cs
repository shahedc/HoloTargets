using UnityEngine;


public class GazeGestureManager : MonoBehaviour
{
    public GameObject ProjectileToShoot;
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            //
            SpawnProjectile();

            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            } 
        };
        recognizer.StartCapturingGestures();
    }
    
    void SpawnProjectile()
    {
        Vector3 ProjectilePosition = Camera.main.transform.position + Camera.main.transform.forward * 0.85f;
        Vector3 ProjectileDirection = Camera.main.transform.forward;

        ShootProjectile(ProjectilePosition, ProjectileDirection);        
    }

    void ShootProjectile(Vector3 start, Vector3 direction)
    {
        //ProjectileToShoot = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Projectile.prefab", typeof(GameObject));
        //GameObject spawnedProjectile = (GameObject)Instantiate(ProjectileToShoot);

        GameObject spawnedProjectile = Instantiate(ProjectileToShoot);
        spawnedProjectile.transform.position = start;
        float force = 100.0f;
        spawnedProjectile.GetComponent<Rigidbody>().AddForce(direction * force);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}