using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGroup : ScriptableObject
{
    public int priority;
    public int freq;

    public List<Event> events;

    public List<Event> getEvents(int num)
    {
        if (num > events.Count) return null;
        List<int> selected = new List<int>();
        List<Event> ret_events = new List<Event>();
        for (int i = 0; i < num; i++)
        {
            int j = Random.Range(0, events.Count);
            while (selected.Contains(j))
            {
                j = Random.Range(0, events.Count);
            }
            selected.Add(j);
            ret_events.Add(events[j]);
        }
        return ret_events;
    }
}
