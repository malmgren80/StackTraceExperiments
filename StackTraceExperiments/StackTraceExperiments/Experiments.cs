using NUnit.Framework;
using StackTraceExperiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackTraceExperiments
{
    [TestFixture]
    public class InvokerMethodInfoTests
    {
        [Test]
        public void Should_get_invoker_method_when_called_from_method()
        {
            var invoker = new MethodInvoker();

            invoker.Execute();

            Assert.That(invoker.MethodInfo.ToString(), Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.Should_get_invoker_method_when_called_from_method()"));
        }

        [Test]
        public void Should_get_property_when_called_from_property()
        {
            string actual = PropertyInvoker.ToString();

            Assert.That(actual, Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.get_PropertyInvoker()"));
        }

        [Test]
        public void Should_get_invoker_method_when_called_from_method_group()
        {
            var invoker = new MethodInvoker();
            Action action = invoker.Execute;

            action.Invoke();

            Assert.That(invoker.MethodInfo.ToString(), Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.Should_get_invoker_method_when_called_from_method_group()"));
        }

        [Test]
        public void Should_get_invoker_method_and_anonymous_description_when_called_from_anonymous_method()
        {
            var invoker = new MethodInvoker();
            Action action = delegate() { invoker.Execute(); };

            action.Invoke();

            Assert.That(invoker.MethodInfo.ToString(), Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.Should_get_invoker_method_and_anonymous_description_when_called_from_anonymous_method() --- () => {...}"));
        }

        [Test]
        public void Should_get_invoker_method_and_anonymous_description_when_called_from_lambda()
        {
            var invoker = new MethodInvoker();
            Action action = () => invoker.Execute();

            action.Invoke();

            Assert.That(invoker.MethodInfo.ToString(), Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.Should_get_invoker_method_and_anonymous_description_when_called_from_lambda() --- () => {...}"));
        }

        [Test]
        public void Should_get_invoker_method_and_anonymous_description_when_called_from_lambda_in_lambda()
        {
            var invoker = new MethodInvoker();
            Action outer = () => 
            {
                Action inner = () => invoker.Execute();
                inner.Invoke();
            };

            outer.Invoke();

            Assert.That(invoker.MethodInfo.ToString(), Is.EqualTo("StackTraceExperiments.InvokerMethodInfoTests.Should_get_invoker_method_and_anonymous_description_when_called_from_lambda_in_lambda() --- () => {...}"));
        }

        public InvokerMethodInfo PropertyInvoker
        {
            get
            {
                var invoker = new MethodInvoker();
                invoker.Execute();
                return invoker.MethodInfo;
            }
        }

        class MethodInvoker
        {
            public InvokerMethodInfo MethodInfo;

            public void Execute()
            {
                MethodInfo = new InvokerMethodInfo();
            }
        }
    }
}
