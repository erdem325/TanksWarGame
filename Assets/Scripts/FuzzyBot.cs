using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge;
using AForge.Fuzzy;
using System;
using UnityEngine.AI;

public class FuzzyBot : MonoBehaviour
{

    public float speed, distance;
    public Transform player;
    //distance
    FuzzySet fsNear, fsMed, fsFar;
    LinguisticVariable lvDistance;
    NavMeshAgent agent;

    //speed
    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable lvSpeed;

    Database database;
    InferenceSystem infSystem;

    // Start is called before the first frame update
    void Start()
    {
        Initializate();
    }

    private void Initializate()
    {
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddRulesTODataBase();
    }

    private void SetDistanceFuzzySets()
    {
        fsNear = new FuzzySet("Near", new TrapezoidalFunction(5, 6, TrapezoidalFunction.EdgeType.Right));
        fsMed = new FuzzySet("Med", new TrapezoidalFunction(6, 8, 9, 10));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(9, 11, TrapezoidalFunction.EdgeType.Left));

        lvDistance = new LinguisticVariable("Distance", 0, 11);
        lvDistance.AddLabel(fsNear);
        lvDistance.AddLabel(fsMed);
        lvDistance.AddLabel(fsFar);

    }

    private void SetSpeedFuzzySets()
    {
        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(4, 5, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(4, 5, 7, 10));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));
        lvSpeed = new LinguisticVariable("Speed", 0, 10);

        lvSpeed.AddLabel(fsSlow);
        lvSpeed.AddLabel(fsMedium);
        lvSpeed.AddLabel(fsFast);
    }

    private void AddRulesTODataBase()
    {
        database = new Database();
        database.AddVariable(lvDistance);
        database.AddVariable(lvSpeed);
        SetRules();
    }
    private void SetRules()
    {
        infSystem = new InferenceSystem(database, new CentroidDefuzzifier(120));
        infSystem.NewRule("Rule 1", "IF Distance IS Near THEN Speed IS Slow");
        infSystem.NewRule("Rule 2", "IF Distance IS Med THEN Speed IS Medium");
        infSystem.NewRule("Rule 3", "IF Distance IS Far THEN Speed IS Fast");
    }
    // Update is called once per frame
    void Update()
    {
        Evalueate();
    }
    private void Evalueate()
    {
        distance = Vector3.Distance(player.position, transform.position);
        infSystem.SetInput("Distance", distance);
        speed = infSystem.Evaluate("Speed");

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        Debug.Log(speed);
    }
}
