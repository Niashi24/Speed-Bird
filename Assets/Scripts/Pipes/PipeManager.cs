using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Pool;
using System;

public class PipeManager : MonoBehaviour
{
    [SerializeField]
    PipeScript _pipePrefab;

    [SerializeField]
    FloatReference _speed;

    [SerializeField]
    FloatReference _secondsPerPipeSpawn = new FloatReference(5f);

    [SerializeField]
    FloatReference _maxDeviationFromCenter = new FloatReference(3);

    [SerializeField]
    FloatReference _gapHeight = new FloatReference(2);

    [SerializeField]
    Transform _spawnLocation;
    
    ObjectPool<PipeScript> pipePool;

    float timer = 0;

    void Start()
    {
        pipePool = _pipePrefab.CreateMonoPool(parent: transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_secondsPerPipeSpawn.Value == 0)
        {
            SpawnPipe();
            return;
        }

        float newTimer = timer + Time.deltaTime;
        if (Mathf.Floor(newTimer/_secondsPerPipeSpawn) != Mathf.Floor(timer/_secondsPerPipeSpawn))
        {
            SpawnPipe();
        }

        timer = newTimer;
    }

    public void SpawnPipe()
    {
        var pipe = pipePool.Get();

        pipe.transform.position = _spawnLocation.position;
        pipe.Randomize(_maxDeviationFromCenter, _gapHeight);
        pipe.OnHitEdge += Release;
    }

    private void Release(PipeScript pipe)
    {
        pipePool.Release(pipe);

        pipe.OnHitEdge -= Release;
    }
}
