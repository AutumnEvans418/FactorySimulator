using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Threading.Tasks;

public class ScriptingManager
{
    public async Task<object> ExecuteScriptAsync(string code)
    {
        try
        {
            var options = ScriptOptions.Default.WithReferences(typeof(Console).Assembly); // Add necessary assemblies
            var script = CSharpScript.Create(code, options, typeof(ScriptGlobals));
            var state = await script.RunAsync(new ScriptGlobals());

            return state.ReturnValue;
        }
        catch (CompilationErrorException ex)
        {
            return ex.Message; // Return compilation errors
        }
        catch (Exception ex)
        {
            return ex.Message; // Handle other exceptions
        }
    }
}

public class ScriptGlobals
{
    // You can expose any necessary objects/methods to the user script by adding them here
    public int Add(int a, int b) => a + b;
}
