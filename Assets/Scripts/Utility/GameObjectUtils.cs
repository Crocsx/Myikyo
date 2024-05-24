using UnityEngine;

public static class GameObjectUtils
{
    public static void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static void DestroyAllInstances<T>() where T : TileBase
    {
        // Find all instances of the class in the scene
        T[] instances = Object.FindObjectsByType<T>(FindObjectsSortMode.None);

        // Destroy each instance
        foreach (T instance in instances)
        {
            // If the instance is a component, destroy its game object
            if (instance is Component)
            {
                Object.Destroy((instance as Component).gameObject);
            }
            // If the instance is a scriptable object, destroy it directly
            else if (instance is ScriptableObject)
            {
                Object.Destroy(instance);
            }
        }
    }
}
