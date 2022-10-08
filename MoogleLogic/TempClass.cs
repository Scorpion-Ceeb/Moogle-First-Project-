namespace MoogleLogic;


public class TempClass
{
    public void TempMethod()
    {
        IDependencyContainer dc = new DependencyInjector();

        Utils.LoadMyInstances(dc);

        ISearcherLogic sl = (ISearcherLogic)dc.GetInstance(typeof(ISearcherLogic));
    }
}