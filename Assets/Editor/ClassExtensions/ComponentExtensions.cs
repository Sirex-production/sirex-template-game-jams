using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void SetGameObjectActive(this Component component) =>
            component.gameObject.SetActive(true);

        public static void SetGameObjectInactive(this Component component) =>
            component.gameObject.SetActive(false);
    }
}