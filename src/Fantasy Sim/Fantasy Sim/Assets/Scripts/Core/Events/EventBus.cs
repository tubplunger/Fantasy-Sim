using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> eventListeners = new();

    public static void Subscribe<T>(Action<T> listener) where T : IWorldEvent
    {
        Type eventType = typeof(T);

        if (eventListeners.TryGetValue(eventType, out Delegate existingListeners))
        {
            eventListeners[eventType] = Delegate.Combine(existingListeners, listener);
        }
        else
        {
            eventListeners[eventType] = listener;
        }
    }

    public static void Unsubscribe<T>(Action<T> listener) where T : IWorldEvent
    {
        Type eventType = typeof(T);

        if (!eventListeners.TryGetValue(eventType, out Delegate existingListeners))
            return;

        Delegate updatedListeners = Delegate.Remove(existingListeners, listener);

        if (updatedListeners == null)
        {
            eventListeners.Remove(eventType);
        }
        else
        {
            eventListeners[eventType] = updatedListeners;
        }
    }

    public static void Publish<T>(T worldEvent) where T : IWorldEvent
    {
        Type eventType = typeof(T);

        if (eventListeners.TryGetValue(eventType, out Delegate listeners))
        {
            ((Action<T>)listeners)?.Invoke(worldEvent);
        }

        // Also publish to the universal logger stream.
        if (eventListeners.TryGetValue(typeof(IWorldEvent), out Delegate globalListeners))
        {
            ((Action<IWorldEvent>)globalListeners)?.Invoke(worldEvent);
        }
    }

    public static void Clear()
    {
        eventListeners.Clear();
    }
}
