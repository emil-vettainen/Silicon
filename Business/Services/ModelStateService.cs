using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services;

public interface IModelStateService
{
    void SaveModelState(string key, ModelStateDictionary modelState);
    ModelStateDictionary LoadModelState(string key);
    void ClearModelState(string key);
}

public class ModelStateService : IModelStateService
{
    private readonly IDictionary<string, ModelStateDictionary> _storage = new Dictionary<string, ModelStateDictionary>();

    public void SaveModelState(string key, ModelStateDictionary modelState)
    {
        _storage[key] = modelState;
    }

    public ModelStateDictionary LoadModelState(string key)
    {
        if (_storage.TryGetValue(key, out var modelState))
        {
            return modelState;
        }
        return new ModelStateDictionary(); 
    }

    public void ClearModelState(string key)
    {
        _storage.Remove(key);
    }
}
