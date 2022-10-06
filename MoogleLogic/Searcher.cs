namespace MoogleLogic;


public class Searcher
{
    public AllTextsInfo Info{get; }
    public string[] Paths{get; set;}
    public TextsPerDoc Texts{get; }
    public Operator[] Operators{get; set;}
    public bool[] WordsMask{get; set;}
    public string[] WordsList{get;set;}

    public Searcher(AllTextsInfo info, string[] paths, string[] wordsList, TextsPerDoc texts, Operator[] operators, bool[] mask)
    {
        this.Info = info;
        this.Paths = paths;
        this.WordsList = wordsList;
        this.Texts = texts;
        this.Operators = operators;
        this.WordsMask = mask;
    }
    public Searcher(){}
}