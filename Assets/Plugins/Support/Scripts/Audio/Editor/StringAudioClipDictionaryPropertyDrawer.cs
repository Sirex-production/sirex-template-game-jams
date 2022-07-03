using UnityEditor;

namespace Support.Audio
{
    [CustomPropertyDrawer(typeof(StringAudioClipDictionary))]
    public class StringAudioClipDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}