using Python.Runtime;
using System.Drawing;

namespace Langchain
{
    public class Module : IDisposable
    {
        public static Module Instance => _instance.Value;

        public dynamic langchain = null;

        private static dynamic sys;

        public static bool DisablePySysConsoleLog { get; set; } = false;

        private static bool alreadyDisabled = false;

        public static Lazy<Module> _instance = new Lazy<Module>(() =>
            {
                var instance = new Module();
                instance.langchain = InstallAndImport("langchain");

                return instance;
            }
        );

        private static PyObject InstallAndImport(string module)
        {
            if (!PythonEngine.IsInitialized)
                PythonEngine.Initialize();

            //sys = Py.Import("sys");
            //if (DisablePySysConsoleLog && !alreadyDisabled)
            //{
            //    string codeToRedirectOutput =
            //    "import sys\n" +
            //    "from io import StringIO\n" +
            //    "sys.stdout = mystdout = StringIO()\n" +
            //    "sys.stdout.flush()\n" +
            //    "sys.stderr = mystderr = StringIO()\n" +
            //    "sys.stderr.flush()\n";

            //    PythonEngine.RunSimpleString(codeToRedirectOutput);
            //    alreadyDisabled = true;
            //}

            var mod = Py.Import(module);
            return mod;
        }

        private bool IsInitialized => langchain != null;

        public Module() { }

        public void Dispose()
        {
            langchain?.Dispose();
            PythonEngine.Shutdown();
        }

        public static PyObject ToPython(object obj)
        {
            if (obj == null) return Runtime.None;
            switch (obj)
            {
                // basic types
                case int o: return new PyInt(o);
                case float o: return new PyFloat(o);
                case double o: return new PyFloat(o);
                case string o: return new PyString(o);
                case bool o: return ConverterExtension.ToPython(o);

                // sequence types
                case Array o: return ToList(o);
                // special types from 'ToPythonConversions'
                case ValueTuple<int> o: return ToTuple(o);
                case ValueTuple<int, int> o: return ToTuple(o);
                case ValueTuple<int, int, int> o: return ToTuple(o);
                case PyObject o: return o;
                //case Sequence o: return o.PyInstance;
                case Base o: return o.PyInstance;
                default: throw new NotImplementedException($"Type is not yet supported: {obj.GetType().Name}. Add it to 'ToPythonConversions'");
            }
        }

        protected static PyTuple ToTuple(Array input)
        {
            var array = new PyObject[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                array[i] = ToPython(input.GetValue(i));
            }

            return new PyTuple(array);
        }

        protected static PyTuple ToTuple(ValueTuple<int> input)
        {
            var array = new PyObject[1];
            array[0] = ToPython(input.Item1);

            return new PyTuple(array);
        }

        protected static PyTuple ToTuple(ValueTuple<int, int> input)
        {
            var array = new PyObject[2];
            array[0] = ToPython(input.Item1);
            array[1] = ToPython(input.Item2);

            return new PyTuple(array);
        }

        protected static PyTuple ToTuple(ValueTuple<int, int, int> input)
        {
            var array = new PyObject[3];
            array[0] = ToPython(input.Item1);
            array[1] = ToPython(input.Item2);
            array[2] = ToPython(input.Item3);

            return new PyTuple(array);
        }

        protected static PyList ToList(Array input)
        {
            var array = new PyObject[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                array[i] = ToPython(input.GetValue(i));
            }

            return new PyList(array);
        }

        protected static PyDict ToDict(Dictionary<int, float> input)
        {
            PyDict dict = new PyDict();

            foreach (var item in input)
            {
                dict[item.Key.ToPython()] = item.Value.ToPython();
            }

            return dict;
        }

        public static string GetStdOut()
        {
            string data = sys.stdout.getvalue().ToString();
            return data.Replace("\n", "\r\n");
        }

        public static string GetStdError()
        {
            string data = sys.stderr.getvalue().ToString();
            return data.Replace("\n", "\r\n");
        }
    }
}