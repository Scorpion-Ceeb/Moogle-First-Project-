namespace MoogleLogic;


public interface IWordValue
{
    void Calculate(string[] paths, AllTextsInfo info, TextsPerDoc textPerDoc);
}
public interface ILoadInfo
{
    void Load(string[] paths, AllTextsInfo info, TextsPerDoc texts);
}
public interface IReadQuery
{
    string[] Read(string[] wordsQuery, Searcher searcher);
}