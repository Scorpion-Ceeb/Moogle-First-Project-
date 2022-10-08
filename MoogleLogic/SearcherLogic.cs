namespace MoogleLogic;


public class SearcherLogic
{
    public IReadQuery ReadQuery{ get;}
    public IWordsValue WordsValue{ get;}
    public ILoadInfo LoadInfo{ get;}

    public SearcherLogic(IReadQuery readQuery, IWordsValue wordsValue, ILoadInfo loadInfo)
    {
        this.ReadQuery = readQuery;
        this.WordsValue = wordsValue;
        this.LoadInfo = loadInfo;
    }

}