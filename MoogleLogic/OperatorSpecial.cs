namespace MoogleLogic;

public class OperatorSpecial : Operator
{
    public Dictionary<string, int> WordTimes{get; private set;}

    public OperatorSpecial(char character) : base(character)
    {
        WordTimes = new Dictionary<string, int>();
    }

    public override bool TryAddWordToOperator(string word, int times)
    {
        return base.TryAddWordToOperator(word, times) && this.WordTimes.TryAdd(word, times);
    }
    public override int GetTimes(string word)
    {
        if(WordTimes.ContainsKey(word))
            return WordTimes[word];
        return 0;
    }
}