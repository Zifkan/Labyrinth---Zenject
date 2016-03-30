using System.ComponentModel;
using Assets.Scripts.InteractiveObjects;
using Assets.Scripts.InteractiveObjects.Interfaces;

namespace Assets.Scripts.Extensions
{
    public static class Extensions
    {
        public static string GetAttrDescription(this IUsableObject usableObject)
        {
            var descriptions = (DescriptionAttribute[])usableObject.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
            {
                return null;
            }
            return descriptions[0].Description;
        }
    }
}
