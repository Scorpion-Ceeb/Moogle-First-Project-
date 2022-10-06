using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MoogleLogic;

public class TextsPerDoc : IDictionary<string, List<string>>
{
    private Dictionary<string, List<string>> _texts;

    public TextsPerDoc()
    {
        _texts = new Dictionary<string, List<string>>();
    }

    public List<string> this[string key] { get => ((IDictionary<string, List<string>>)_texts)[key]; set => ((IDictionary<string, List<string>>)_texts)[key] = value; }

    public ICollection<string> Keys => ((IDictionary<string, List<string>>)_texts).Keys;

    public ICollection<List<string>> Values => ((IDictionary<string, List<string>>)_texts).Values;

    public int Count => ((ICollection<KeyValuePair<string, List<string>>>)_texts).Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, List<string>>>)_texts).IsReadOnly;

    public void Add(string key, List<string> value)
    {
        ((IDictionary<string, List<string>>)_texts).Add(key, value);
    }

    public void Add(KeyValuePair<string, List<string>> item)
    {
        ((ICollection<KeyValuePair<string, List<string>>>)_texts).Add(item);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<string, List<string>>>)_texts).Clear();
    }

    public bool Contains(KeyValuePair<string, List<string>> item)
    {
        return ((ICollection<KeyValuePair<string, List<string>>>)_texts).Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return ((IDictionary<string, List<string>>)_texts).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, List<string>>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<string, List<string>>>)_texts).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<string, List<string>>>)_texts).GetEnumerator();
    }

    public bool Remove(string key)
    {
        return ((IDictionary<string, List<string>>)_texts).Remove(key);
    }

    public bool Remove(KeyValuePair<string, List<string>> item)
    {
        return ((ICollection<KeyValuePair<string, List<string>>>)_texts).Remove(item);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out List<string> value)
    {
        return ((IDictionary<string, List<string>>)_texts).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_texts).GetEnumerator();
    }
}