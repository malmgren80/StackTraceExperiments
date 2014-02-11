using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StackTraceExperiments
{
    public class InvokerMethodInfo
    {
        private readonly StackTrace _trace;
        private const int FramesToSkip = 2;
        private const string AnonymousMethodDescription = " --- () => {...}";

        public InvokerMethodInfo()
        {
            _trace = new StackTrace(FramesToSkip, false);
        }

        private string GetDescription(MethodBase method, bool isCalledFromAnonymous)
        {
            string fullMethodName = string.Format("{0}.{1}()", method.DeclaringType.FullName, method.Name);
            if (isCalledFromAnonymous)
            {
                return string.Concat(fullMethodName, AnonymousMethodDescription);
            }
            return fullMethodName;
        }

        public override string ToString()
        {
            int index = 0;
            while (index < _trace.FrameCount)
            {
                var frame = _trace.GetFrame(index);

                if (!IsAnonymousMethod(frame))
                {
                    var method = frame.GetMethod();
                    return GetDescription(method, index != 0);
                }

                index++;
            }

            return string.Empty;
        }

        private bool IsAnonymousMethod(StackFrame frame)
        {
            /// Tried this one as in SO answer, but no CustomAttributes found
            /// return frame.GetMethod().GetCustomAttributes(typeof(CompilerGeneratedAttribute)).Any();
            /// http://stackoverflow.com/questions/2503336/how-to-identify-anonymous-methods-in-system-reflection

            // Don't know if this will work in all cases...

            return frame.ToString().StartsWith("<");
        }
    }
}
