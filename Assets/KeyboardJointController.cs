using UnityEngine;
using Preliy.Flange;

public class KeyboardJointController : MonoBehaviour
{
    public Controller controller;
    public float speed = 10f;

    private float[] jointValues;

    void Start()
    {
        if (controller == null)
        {
            Debug.LogError("Controller not assigned!");
            enabled = false;
            return;
        }

        // Ambil nilai awal seluruh joint
        jointValues = controller.MechanicalGroup.JointState.Value.Clone() as float[];
    }

    void Update()
    {
        if (controller == null || jointValues == null) return;

        // J1 - A/D
        if (Input.GetKey(KeyCode.A)) jointValues[0] -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) jointValues[0] += speed * Time.deltaTime;

        // J2 - W/S
        if (Input.GetKey(KeyCode.W)) jointValues[1] += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) jointValues[1] -= speed * Time.deltaTime;

        // J3 - Q/E
        if (Input.GetKey(KeyCode.Q)) jointValues[2] += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) jointValues[2] -= speed * Time.deltaTime;

        // J5 - Arrow Up/Down
        if (Input.GetKey(KeyCode.UpArrow)) jointValues[4] += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) jointValues[4] -= speed * Time.deltaTime;

        // E1 - Z/X
        if (Input.GetKey(KeyCode.Z)) jointValues[6] -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.X)) jointValues[6] += speed * Time.deltaTime;

        // Clamp nilai agar tetap dalam batas
        jointValues[0] = Mathf.Clamp(jointValues[0], -170f, 170f); // J1
        jointValues[1] = Mathf.Clamp(jointValues[1], -120f, 120f); // J2
        jointValues[2] = Mathf.Clamp(jointValues[2], -120f, 120f); // J3
        jointValues[4] = Mathf.Clamp(jointValues[4], -120f, 120f); // J5
        jointValues[6] = Mathf.Clamp(jointValues[6], 0f, 5f);      // E1

        // Apply seluruh joint
        controller.MechanicalGroup.SetJoints(new JointTarget(jointValues), true);
    }
}
