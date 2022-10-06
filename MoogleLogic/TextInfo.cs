using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MoogleLogic;

public class TextInfo : IDictionary<string, WordProp>
{
    private Dictionary<string, WordProp> _textInfo;

    public TextInfo()
    {
        _textInfo = new Dictionary<string, WordProp>();
    }

    public WordProp this[string key] { get => ((IDictionary<string, WordProp>)_textInfo)[key]; set => ((IDictionary<string, WordProp>)_textInfo)[key] = value; }

    public ICollection<string> Keys => ((IDictionary<string, WordProp>)_textInfo).Keys;

    public ICollection<WordProp> Values => ((IDictionary<string, WordProp>)_textInfo).Values;

    public int Count => ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).IsReadOnly;

    public void Add(string key, WordProp value)
    {
        ((IDictionary<string, WordProp>)_textInfo).Add(key, value);
    }

    public void Add(KeyValuePair<string, WordProp> item)
    {
        ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).Add(item);
    }
    public bool TryAdd(string key, WordProp value)
    {
        return _textInfo.TryAdd(key, value);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).Clear();
    }

    public bool Contains(KeyValuePair<string, WordProp> item)
    {
        return ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return ((IDictionary<string, WordProp>)_textInfo).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, WordProp>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, WordProp>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<string, WordProp>>)_textInfo).GetEnumerator();
    }

    public bool Remove(string key)
    {
        return ((IDictionary<string, WordProp>)_textInfo).Remove(key);
    }

    public bool Remove(KeyValuePair<string, WordProp> item)
    {
        return ((ICollection<KeyValuePair<string, WordProp>>)_textInfo).Remove(item);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out WordProp value)
    {
        return ((IDictionary<string, WordProp>)_textInfo).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_textInfo).GetEnumerator();
    }
}