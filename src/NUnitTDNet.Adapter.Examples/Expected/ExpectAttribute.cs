namespace NUnitTDNet.Adapter.Examples.Expected
{
    using System;
    using TestDriven.Framework;

    public class ExpectAttribute : Attribute
    {
        public bool Namespace
        {
            get; set;
        }
    }

    public class ExpectTestRunAttribute : ExpectAttribute
    {
        public ExpectTestRunAttribute(TestRunState testRunState)
        {
            TestRunState = testRunState;
        }

        public TestRunState? TestRunState
        {
            get; private set;
        }

        public int PassedCount
        {
            get; set;
        }

        public int IgnoredCount
        {
            get; set;
        }

        public int FailedCount
        {
            get; set;
        }
    }

    public class ExpectTestAttribute : ExpectAttribute
    {
        public ExpectTestAttribute(string name, TestState testState) : this(name)
        {
            State = testState;
        }

        public ExpectTestAttribute(string name)
        {
            Name = name;
            TotalTests = -1; // negative is unspecified
        }

        public string Name
        {
            get; private set;
        }

        public string Message
        {
            get; set;
        }

        public string StackTraceStartsWith
        {
            get; set;
        }

        public string StackTraceEndsWith
        {
            get; set;
        }

        public int TotalTests
        {
            get; set;
        }

        public TestState? State
        {
            get; private set;
        }
    }


    public class ExpectOutputLineAttribute : ExpectAttribute
    {
        public ExpectOutputLineAttribute(string text)
        {
            Text = text;
        }

        public ExpectOutputLineAttribute(string text, Category category)
        {
            Text = text;
            Category = category;
        }

        public string Text
        {
            get; private set;
        }

        public Category? Category
        {
            get; private set;
        }
    }
}
