using System;
using System.Collections.Generic;


/// <summary>
/// Dependency Injection Service Locator
/// </summary>
public class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new();

    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public static T GetService<T>()
    {
        if (services.TryGetValue(typeof(T), out object service))
        {
            return (T)service;
        }

        throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
    }
    public static object GetService(Type type)
    {
        if (services.TryGetValue(type, out object service))
        {
            return service;
        }

        throw new InvalidOperationException($"Service of type {type} is not registered.");
    }
    public static void UnRegister<T>()
    {
        services.Remove(typeof(T));
    }
    public static void Clear()
    {
        services.Clear();
    }
}
