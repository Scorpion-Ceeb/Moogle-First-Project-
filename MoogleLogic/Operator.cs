namespace MoogleLogic;

public class Operator
{
#region #region Fields and Variables
    public char OperatorCharacter{get;private set;}
    public List<string> Words{get; private set;}
    
#endregion

    public Operator(char operatorChar)
    {
        OperatorCharacter = operatorChar;
        Words = new List<string>();
    }

    protected bool OperatorContainsWord(string word) => this.Words.Contains(word);
    public virtual bool TryAddWordToOperator(string word, int times)
    {
        if(OperatorContainsWord(word))
        {
            this.Words.Add(word);
            return true;
        }
        return false;
    }
    public virtual int GetTimes(string word) => 1;

}