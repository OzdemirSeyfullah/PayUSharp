using System;
using PayU.IPN;
using System.Reflection;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.CodeDom.Compiler;
using PayU;
using Fasterflect;
using System.Collections.Generic;
using PayU.Core;
using Newtonsoft.Json;

namespace Doc
{
  class MainClass
  {
    private static CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
    private static string OutputBasePath = "docs/";

    public static int Main(string[] args)
    {
      if (args.Length < 1)
      {
        Console.Error.WriteLine("Supply the path to the docs folder");
        return 1;
      }

      OutputBasePath = args[0];

      Console.WriteLine("Generating fields...");

      ProcessIPN();
      Console.WriteLine("  Generated fields for IPN");
      ProcessLU();
      Console.WriteLine("  Generated fields for LU");
      ProcessALU();
      Console.WriteLine("  Generated fields for ALU");
      ProcessToken();
      Console.WriteLine("  Generated fields for Token");

      OutputMetadata();

      Console.WriteLine("  All done.");

      return 0;
    }

    static void OutputMetadata()
    {
      var filename = RelativePath("support/metadata.yml");
      var assembly = Assembly.GetAssembly(typeof(PayU.Core.PayuException));
      var version = assembly.GetName().Version.ToString();
      var date = string.Format("{0:d}", DateTime.Now);

      var contents = File.ReadAllText(filename + ".template");
      contents = contents.Replace("{{VERSION}}", version);
      contents = contents.Replace("{{DATE}}", date);
      File.WriteAllText(filename, contents);
    }

    static void ProcessToken()
    {
      using (var file = OpenFile("fields/token_fields.md")) {
        file.WriteLine("| `TokenResponse` Alanı | Alan Tipi | PayU Token Cevap Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<JsonPropertyAttribute>(typeof(PayU.Token.TokenResponse), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));

        file.WriteLine();

        file.WriteLine("| `TokenHistory` Alanı | Alan Tipi | PayU Token Cevap Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<JsonPropertyAttribute>(typeof(PayU.Token.TokenHistory), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));
      }
    }

    static void ProcessALU()
    {
      using (var file = OpenFile("fields/alu_fields.md")) {
        file.WriteLine("| `OrderDetails` Alanı | Alan Tipi | PayU ALU İstek Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<ParameterAttribute>(typeof(PayU.AutomaticLiveUpdate.OrderDetails), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));

        file.WriteLine();

        file.WriteLine("| `ALUResponse` Alanı | Alan Tipi | PayU ALU Cevap Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<XmlElementAttribute>(typeof(PayU.AutomaticLiveUpdate.ALUResponse), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));
        Process<XmlArrayAttribute>(typeof(PayU.AutomaticLiveUpdate.ALUResponse), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));

        file.WriteLine();

        file.WriteLine("| `WireAccount` Alanı | Alan Tipi | PayU ALU Cevap Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<XmlElementAttribute>(typeof(PayU.AutomaticLiveUpdate.WireAccount), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));
      }
    }

    static void ProcessLU()
    {
      using (var file = OpenFile("fields/lu_fields.md")) {
        file.WriteLine("| `OrderDetails` Alanı | Alan Tipi | PayU LiveUpdate Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<ParameterAttribute>(typeof(PayU.LiveUpdate.OrderDetails), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));
      }
    }

    static void ProcessIPN()
    {
      using (var file = OpenFile("fields/ipn_fields.md")) {
        file.WriteLine("| `IPNRequest` Alanı | Alan Tipi | PayU IPN POST Alanı |");
        file.WriteLine("| ----             | ---       | ---                 |");
        Process<XmlElementAttribute>(typeof(IPNProduct), (prop, attribute) => WritePropToFile("| `Products[idx].{0}` | `{2}` | `{1}[idx]` |", prop, attribute, file));
        Process<XmlElementAttribute>(typeof(IPNRequest), (prop, attribute) => WritePropToFile("| `{0}` | `{2}` | `{1}` |", prop, attribute, file));
      }
    }

    private static void WritePropToFile(string format, PropertyInfo prop,  ParameterAttribute attribute, StreamWriter file) {
      if (attribute.IsNested) {
        var enumerable = prop.PropertyType.Implements(typeof(System.Collections.IEnumerable));
        var type = enumerable ? prop.PropertyType.GetGenericArguments()[0] : prop.PropertyType;
        var fmt = string.Format("| `{0}{1}.{{0}}` | `{{2}}` | `{{1}}{1}` |", prop.Name, enumerable ? "[idx]" : "");
        Process<ParameterAttribute>(type, (_prop, _attribute) => WritePropToFile(fmt, _prop, _attribute, file));
      } else {
        file.WriteLine(format, prop.Name, attribute.Name.Replace("[]", ""), prop.PropertyType.Name());
      }
    }

    private static string RelativePath(string filename) {
        return Path.Combine(OutputBasePath, filename);
    }

    private static StreamWriter OpenFile(string filename) {
      return new StreamWriter(RelativePath(filename), false);
    }

    private static void WritePropToFile(string format, PropertyInfo prop,  JsonPropertyAttribute attribute, StreamWriter file) {
      file.WriteLine(format, prop.Name, attribute.PropertyName.Replace("[]", ""), prop.PropertyType.Name());
      if (prop.Name.EndsWith("DateAsString")) {
        file.WriteLine(format, prop.Name.Replace("DateAsString", ""), attribute.PropertyName, typeof(DateTime).Name());
      }
    }

    private static void WritePropToFile(string format, PropertyInfo prop,  XmlElementAttribute attribute, StreamWriter file) {
      file.WriteLine(format, prop.Name, attribute.ElementName.Replace("[]", ""), prop.PropertyType.Name());
      if (prop.Name.EndsWith("DateAsString")) {
        file.WriteLine(format, prop.Name.Replace("DateAsString", ""), attribute.ElementName, typeof(DateTime).Name());
      }
    }

    private static void WritePropToFile(string format, PropertyInfo prop,  XmlArrayAttribute attribute, StreamWriter file) {
      file.WriteLine(format, prop.Name, attribute.ElementName.Replace("[]", ""), prop.PropertyType.Name());
      if (prop.Name.EndsWith("DateAsString")) {
        file.WriteLine(format, prop.Name.Replace("DateAsString", ""), attribute.ElementName, typeof(DateTime).Name());
      }
    }

    private static string TypeName(Type type) {
      if (type.IsGenericType && type.GetGenericTypeDefinition().FullName.StartsWith("System.Nullable")) {
        return provider.GetTypeOutput(new System.CodeDom.CodeTypeReference(type.GetGenericArguments()[0])) + "?";
      }
      return provider.GetTypeOutput(new System.CodeDom.CodeTypeReference(type));
    }

    private static void Process<TAttribute>(Type type, Action<PropertyInfo, TAttribute> action) where TAttribute: Attribute {
      Dictionary<Type, int> lookup = new Dictionary<Type, int>();

      int count = 0;
      lookup[type] = count++;
      Type parent = type.BaseType;
      while (parent != null)
      {
        lookup[parent] = count;
        count++;
        parent = parent.BaseType;
      }

      var props = type.PropertiesWith(Flags.InstancePublic | BindingFlags.FlattenHierarchy, typeof(TAttribute))
        .OrderByDescending(prop => lookup[prop.DeclaringType]);

      foreach (var prop in props) {
        var attribute = prop.Attribute<TAttribute>();
        action(prop, attribute);
      }
    }

    private static int GetDepth(Type t)
    {
      int depth = 0;
      while (t != null)
      {
        depth++;
        t = t.BaseType;
      }
      return depth;
    }
  }
}
