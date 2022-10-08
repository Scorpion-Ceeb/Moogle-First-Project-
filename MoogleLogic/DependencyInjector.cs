using System.Reflection;

namespace MoogleLogic;


public class DependencyInjector : IDependencyContainer
{
    private Dictionary<Type, Type> dependencies;
    public DependencyInjector()
    {
        dependencies = new Dictionary<Type, Type>();
    }
    public object GetInstance(Type t)
    {
        if(!dependencies.ContainsKey(t)) throw new ArgumentException();
        Type type = dependencies[t];

        //Activator.CreateInstance(type)!;
        ConstructorInfo[] ctors = type.GetConstructors();
        ConstructorInfo ci = ctors[0];
        ParameterInfo[] args = ci.GetParameters();
        object[] ctrParams = new object[args.Length];
        
        for (int i = 0; i < ctrParams.Length; i++)
        {
            ctrParams[i] = GetInstance(args[i].ParameterType);
        }
        return ci.Invoke(ctrParams);

    }

    public void Register(Type interfaceType, Type implementationType)
    {
        if(!dependencies.ContainsKey(interfaceType))
            dependencies.Add(interfaceType, implementationType);
        else
            dependencies[interfaceType] = implementationType;
    }
}