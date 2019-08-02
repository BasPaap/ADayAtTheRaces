using Bas.ADayAtTheRaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{
    public Vector3 firstCornerPosition;
    public Vector3 secondCornerPosition;
    public Vector3 thirdCornerPosition;
    public Vector3 finishLinePosition;

    private Horse horse;
    private RunningPhase currentRunningPhase;
    private float currentRunningPhaseStartTime;
    private NavMeshAgent navMeshAgent;
    private bool isInLastPhase = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (horse != null)
        {
            UpdateSpeed();
        }

        UpdateDestination();
    }

    int lapsRun = 0;

    private void UpdateDestination()
    {
        if (navMeshAgent != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (Vector3.Distance(navMeshAgent.destination, firstCornerPosition) < 0.02f)
            {
                navMeshAgent.destination = secondCornerPosition;
                //Debug.Log($"{gameObject.name} set to second corner.");
            }
            else if (Vector3.Distance(navMeshAgent.destination, secondCornerPosition) < 0.02f)
            {
                navMeshAgent.destination = thirdCornerPosition;
                //Debug.Log($"{gameObject.name} set to third corner.");
            }
            else if (Vector3.Distance(navMeshAgent.destination, thirdCornerPosition) < 0.02)
            {   
                navMeshAgent.destination = (this.lapsRun == 0) ? firstCornerPosition : finishLinePosition;
                this.lapsRun++;
                //Debug.Log($"{gameObject.name} set to finish line.");
            }
        }
    }

    public void Run(Horse horse)
    {
        this.horse = horse;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(transform.position);
        navMeshAgent.destination = firstCornerPosition;
    }

    private void UpdateSpeed()
    {
        if (currentRunningPhase == null)
        {
            SetRunningPhase(horse.RunningPhases.First());
        }

        if (!isInLastPhase)
        {
            if (currentRunningPhase.Duration.TotalSeconds <= (Time.time - currentRunningPhaseStartTime))
            {
                int nextRunningPhaseIndex;

                if (currentRunningPhase == horse.RunningPhases.Last())
                {
                    nextRunningPhaseIndex = 0;
                    isInLastPhase = true;
                }
                else
                {
                    nextRunningPhaseIndex = horse.RunningPhases.IndexOf(currentRunningPhase) + 1;
                }

                SetRunningPhase(horse.RunningPhases[nextRunningPhaseIndex]);
            }
        }

        
    }

    private void SetRunningPhase(RunningPhase runningPhase)
    {
        currentRunningPhaseStartTime = Time.time;
        currentRunningPhase = runningPhase;

        navMeshAgent.speed *= currentRunningPhase.Speed;

        gameObject.GetComponentInChildren<Animator>().SetFloat("HorseSpeed", currentRunningPhase.Speed);

        //navMeshAgent.acceleration *= currentRunningPhase.Speed;
        //navMeshAgent.angularSpeed *= currentRunningPhase.Speed;

        Debug.Log($"{gameObject.name} speed set to {runningPhase.Speed} for {runningPhase.Duration.TotalSeconds} seconds (index {this.horse.RunningPhases.IndexOf(runningPhase)})");
    }
}
