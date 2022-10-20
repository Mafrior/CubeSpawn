using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeSpawner : MonoBehaviour
{
    Camera cam; // main camera

    public Transform cubePrefab;
    public Transform floor;

    public Transform garbage;

    float timer = 1;

    // all properties we should change
    float couldown = 5;
    float speed = 5;
    float distance = 10;

    void Start(){
        cam = Camera.main;
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = couldown;

            // Spawn cubes and set properties
            MoveScript cube = Instantiate(cubePrefab, transform.position, Quaternion.identity, transform).GetComponent<MoveScript>();
            cube.speed = speed;
            cube.distance = distance - 0.6f;
        }
    }

    #region propertyChanged
    public void OnDistanceChanged(InputField _distance) {
        ClearChildren();
        AttachProperty(_distance, ref distance, 5, 50);

        garbage.position = new Vector3(distance+1, -5, 2.5f);
        cam.transform.position = new Vector3(distance/2, distance/3, -distance);

        floor.localScale = new Vector3(distance, 0.1f, 5);
        floor.localPosition = new Vector3(distance/2, 0, 2.5f);
    }

    public void OnSpeedChanged(InputField _speed) {
        ClearChildren();
        AttachProperty(_speed, ref speed, 1, 10);
    }

    public void OnCouldownChanged(InputField _couldown) {
        ClearChildren();
        AttachProperty(_couldown, ref couldown, 1, 10);
    }
    #endregion

    // Set value to property and checking boundaries at the same time
    private void AttachProperty(InputField text, ref float value, float min, float max) {
        value = Convert.ToSingle(text.text);
        value = value > max ? max : value;
        value = value < min ? min : value;
        text.text = $"{value}";
    }

    // For good picture 
    private void ClearChildren() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void InputCheck(InputField text) {
        // Regular Expressions for float input check
        MatchCollection matches = new Regex(@"\d*[,]?\d*").Matches(text.text);
        text.text = matches[0].Value;
    }
}
