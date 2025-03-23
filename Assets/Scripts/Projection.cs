using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {

    private Scene simulationScene;
    private PhysicsScene2D physicsScene;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private LineRenderer line;
    [SerializeField] private int maxIterations;
    [SerializeField] private LayerMask stopLayers;

    private Dictionary<Transform, Transform> spawnedObjects = new Dictionary<Transform, Transform>();

    // Start is called before the first frame update
    void Start() {
        Physics.simulationMode = SimulationMode.Script;
        CreatePhysicsScene();
    }

    // Update position of simulation items when real items are moved
    void Update() {
        foreach (var item in spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    // Creates a simulated scene where the bullet path is simulated
    void CreatePhysicsScene() {

        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        // Create copy of each of the items in the simulated scene as ghost object
        foreach (Transform item in itemsParent) {
            var ghostObj = Instantiate(item.gameObject, item.position, item.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
            if (!ghostObj.isStatic) {
                spawnedObjects.Add(item.gameObject.transform, ghostObj.transform);
            }
        }

    }

    // Perform bullet siulation
    public void SimulateTrajectory(GameObject bulletPrefab, Transform spawn, Vector3 velocity) {
        // Create ghost bullet
        var ghostObj = Instantiate(bulletPrefab, spawn.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);
        ghostObj.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);

        line.positionCount = maxIterations;

        // for each iteration move one step in the physics scene, find where bullet is and record that in line
        for (int i = 0; i < maxIterations; i++) {
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);
            // Line cast between points to see if object is in the way
            if (i > 0) {
                RaycastHit2D hit = Physics2D.Linecast(line.GetPosition(i - 1), line.GetPosition(i), stopLayers);
                if (hit) {
                    line.SetPosition(i, hit.point);
                }
            }

        }
        // Destroy siulated bullet
        Destroy(ghostObj.gameObject);
    }
}
