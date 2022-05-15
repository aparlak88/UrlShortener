using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Aspects
{
    [PSerializable]
    public class ExceptionAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine("ERR: {0}", args.Exception.Message);
        }
    }
}