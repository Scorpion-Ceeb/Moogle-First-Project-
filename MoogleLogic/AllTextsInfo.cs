using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MoogleLogic;


public class AllTextsInfo : IDictionary<string, TextInfo>
{
    private Dictionary<string, TextInfo> _allInfo;

    public AllTextsInfo()
    {
        _allInfo = new Dictionary<string, TextInfo>();
    }

    public TextInfo this[string key] { get => ((IDictionary<string, TextInfo>)_allInfo)[key]; set => ((IDictionary<string, TextInfo>)_allInfo)[key] = value; }

    public ICollection<string> Keys => ((IDictionary<string, TextInfo>)_allInfo).Keys;

    public ICollection<TextInfo> Values => ((IDictionary<string, TextInfo>)_allInfo).Values;

    public int Count => ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).IsReadOnly;

    public void Add(string key, TextInfo value)
    {
        ((IDictionary<string, TextInfo>)_allInfo).Add(key, value);
    }

    public void Add(KeyValuePair<string, TextInfo> item)
    {
        ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).Add(item);
    }
    public bool TryAdd(string key, TextInfo value)
    {
        return _allInfo.TryAdd(key, value);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).Clear();
    }

    public bool Contains(KeyValuePair<string, TextInfo> item)
    {
        return ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return ((IDictionary<string, TextInfo>)_allInfo).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, TextInfo>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, TextInfo>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<string, TextInfo>>)_allInfo).GetEnumerator();
    }

    public bool Remove(string key)
    {
        return ((IDictionary<string, TextInfo>)_allInfo).Remove(key);
    }

    public bool Remove(KeyValuePair<string, TextInfo> item)
    {
        return ((ICollection<KeyValuePair<string, TextInfo>>)_allInfo).Remove(item);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out TextInfo value)
    {
        return ((IDictionary<string, TextInfo>)_allInfo).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_allInfo).GetEnumerator();
    }
}