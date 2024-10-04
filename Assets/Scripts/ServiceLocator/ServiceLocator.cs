using System;
using System.Collections;
using System.Collections.Generic;

public class ServiceLocator
{
    public static ServiceLocator Instance { get; private set; }

    private Dictionary<string, IService> _services;

    private ServiceLocator()
    {
        if (Instance != null)
            return;

        Instance = this;
        _services = new Dictionary<string, IService>();    
    }

    public static void Init()
    {
        Instance = new ServiceLocator();
    }
    
    public void Register<T>(T service) where T : IService
    {
        var key = typeof(T).Name;

        if (_services.ContainsKey(key))
            new ArgumentOutOfRangeException($"Service {key} is already registered!");

        _services.Add(key, service);
    }

    public void Unregister<T>(T service) where T : IService
    {
        var key = typeof(T).Name;

        if (!_services.ContainsKey(key))
            new InvalidOperationException($"Service {key} is not registered!");

        _services.Add(key, service);
    }

    public T Get<T>() where T : IService
    {
        var key = typeof(T).Name;

        if (!_services.ContainsKey(key))
            new InvalidOperationException($"Service {key} is not registered!");

        return (T)_services[key];
    }
}
