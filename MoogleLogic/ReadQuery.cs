namespace MoogleLogic;

public class ReadQuery : IReadQuery
{
    public InputUser Read(string[] query)
    {
        return ReadInput(query);
    }
    private InputUser ReadInput(string[] query)
    {
        List<string> words = new List<string>();
        Operator[] operators = Utils.GetOperators();

        for (int i = 0; i < query.Length; i++)
        {
            if(query[i].Any(word => char.IsLetterOrDigit(word))){
                words.Add(SeparateWordFromOperator(operators, query[i]).ToLower());
            }
        }
        return new InputUser(operators.ToArray(), words.ToArray());
    }
    private string SeparateWordFromOperator(Operator[] operators, string word)
    {
        for (int i = 0; i < operators.Length; i++)
        {
            if(word.StartsWith(operators[i].OperatorCharacter))
            {
                (string Word, int Times) tuple = WordAndCountOperator(word, operators[i].OperatorCharacter);

                operators[i].TryAddWordToOperator(tuple.Word, tuple.Times);

                return tuple.Word;
            }
        }
        
        return !char.IsLetter(word[0])? CleanWord(word) : word;
    }
    private (string,int) WordAndCountOperator(string word, char operChar)
    {
        int times = 0;
        while (!char.IsLetter(word[0]))
        {
            if(word[0] == operChar)
                times+=1;
            word=word.Substring(1);
        }
        return new (word, times);
    }
    private string CleanWord(string word)
    {
        while (!char.IsLetter(word[0]))
        {
            word=word.Substring(1);
        }
        return word;
    }
}