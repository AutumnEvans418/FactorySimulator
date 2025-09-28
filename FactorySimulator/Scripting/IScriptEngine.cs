using FactorySimulator.Factories;

namespace FactorySimulator.Scripting
{
    public interface IScriptEngine
    {
        Factory Execute(Factory factory);
    }
}
