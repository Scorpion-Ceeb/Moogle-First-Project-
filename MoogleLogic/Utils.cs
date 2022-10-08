namespace MoogleLogic;

public static class Utils
{
    public static Operator[] GetOperators()
    {
        List<Operator> operators = new List<Operator>()
        {
            new Operator('!'),
            new Operator('^'),
            new OperatorSpecial('*')
        };

        return operators.ToArray();
    }
    public static void LoadMyInstances(IDependencyContainer dc)
    {
        dc.Register(typeof(IReadQuery), typeof(ReadQuery));
        dc.Register(typeof(IWordsValue), typeof(WordsValue));
        dc.Register(typeof(ILoadInfo), typeof(LoadInfo));
        dc.Register(typeof(ISearcherLogic), typeof(SearcherLogic));
    }
}