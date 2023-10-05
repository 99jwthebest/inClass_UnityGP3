using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : ScriptableObject
{
    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfullySensed);
    public event OnPerceptionUpdated onPerceptionUpdated;

    [SerializeField] private float forgetTime = 2f;

    List<PerceptionStimuli> currentlyPercievableStimulis = new List<PerceptionStimuli>();
    Dictionary<PerceptionStimuli, Coroutine> currentForgettingCoroutines = new Dictionary<PerceptionStimuli, Coroutine>();
    public MonoBehaviour Owner
    {
        get;
        private set;
    }

    public virtual void Init(MonoBehaviour owner)
    {
        Owner = owner;
    }

    public void Update()
    {
        foreach(PerceptionStimuli stimuli in registeredStimulis)
        {
            if (IsStimuliSensible(stimuli) && !IsStimuliSensed(stimuli))
            {
                currentlyPercievableStimulis.Add(stimuli);
                if (currentForgettingCoroutines.ContainsKey(stimuli))
                {
                    StopForgettingStimuli(stimuli);
                }
                else
                {
                    onPerceptionUpdated.Invoke(stimuli, true);
                }
            }

            if(!IsStimuliSensible(stimuli) && IsStimuliSensed(stimuli))
            {
                currentlyPercievableStimulis.Remove(stimuli);
                StartForgettingStimuli(stimuli);
            }
        }

    }

    private void StopForgettingStimuli(PerceptionStimuli stimuli)
    {
        Owner.StopCoroutine(currentForgettingCoroutines[stimuli]);
        currentForgettingCoroutines.Remove(stimuli);
    }

    private void StartForgettingStimuli(PerceptionStimuli stimuli)
    {
        Coroutine forgettingCoroutine =  Owner.StartCoroutine(ForgettingCoroutine(stimuli));
        currentForgettingCoroutines.Add(stimuli, forgettingCoroutine);
    }

    private IEnumerator ForgettingCoroutine(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        currentForgettingCoroutines.Remove(stimuli); // we have already forgetten, coroutine is done.
        onPerceptionUpdated.Invoke(stimuli, false);
    }

    private bool IsStimuliSensed(PerceptionStimuli stimuli)
    {
        return currentlyPercievableStimulis.Contains(stimuli); // constant time 0(1)
    }

    public abstract bool IsStimuliSensible(PerceptionStimuli stimuli);

    public virtual void DrawDebug()
    {

    }



























    static HashSet<PerceptionStimuli> registeredStimulis = new HashSet<PerceptionStimuli>();  // ask about what it does
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Add(stimuli);
    }

    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }
}
