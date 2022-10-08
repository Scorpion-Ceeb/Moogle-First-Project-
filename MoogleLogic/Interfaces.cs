namespace MoogleLogic;

public interface IDependencyContainer
{
    public void Register(Type interfaceType, Type implementationType);
    public object GetInstance(Type t);
}
public interface IWordsValue
{
    void GetValue(Document[] docs);
}
public interface ILoadInfo
{
    Document[] Load(string[] paths);
}
public interface IReadQuery
{
    InputUser Read(string[] query);
}
public interface ISearcherLogic
{
    IReadQuery ReadQuery{ get;}
    IWordsValue WordsValue{ get;}
    ILoadInfo LoadInfo{ get;}
}