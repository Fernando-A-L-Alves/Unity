using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    public GameObject Target; // The prefab to be spawned. (Set by dragging the "cube" prefab unto the script)    
    public float SpawnTime = 2.5f; // How long between each spawn.    

    public Text ActiveTargetText; //Set by dragging the UI Text element to the script
    public Text WinText;

    public int NumberOfTargets = 12;
    public float CenterX = 0.0f;
    public float CenterY = 0.0f;
    public float Radius = 4.0f;

    int _activeTargetCount;
    IList<GameObject> _targets;

    // Use this for initialization
    void Start () {

        _targets = new List<GameObject>();
        SpawnAll();
        _activeTargetCount = _targets.Count();
        SetUIText();
        WinText.text = "";
        // Call the function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Reactivate", SpawnTime/*start time*/, SpawnTime/*repeat time*/);
    }

    void Update()
    {
        _activeTargetCount = _targets.Count(x => x.active);
        SetUIText();
        CheckForWin();
    }

    void SetUIText()
    {
        ActiveTargetText.text = "Targets remaining: " + _activeTargetCount;
    }

    void CheckForWin()
    {
        if (_activeTargetCount == 0)
            WinText.text = "Congratulations! You Win!";
    }

    void SpawnAll()
    {
        //circle eq: "r" is the radius, "cx,cy" the origin, and "a" the angle.
        //x = cx + r * cos(a)
        //y = cy + r * sin(a)

        for(var i=0; i<NumberOfTargets; i++)
        {
            float deltaAngle = 360f / NumberOfTargets;

            var a = deltaAngle * i;
            var x = CenterX + Radius * Mathf.Cos(a * Mathf.Deg2Rad);
            var y = CenterY + Radius * Mathf.Sin(a * Mathf.Deg2Rad);

            var targetPosition = new Vector3(x/*floor*/, 0.8f/*height = y*/, y/*floor = z*/);
            var targetRotation = new Vector3(30, 15, -15);
            var targetRotationQuaternion = Quaternion.LookRotation(targetRotation);

            var targetInstance = Instantiate(Target, targetPosition, targetRotationQuaternion);
            _targets.Add(targetInstance);
        }
    }

    void Reactivate()
    {
        if (_activeTargetCount == 0)
            return;

        var targetInstance = _targets.FirstOrDefault(x => !x.active);//get the first unactive target
        if (targetInstance != null)
            targetInstance.SetActive(true);//activate it
    }
}
