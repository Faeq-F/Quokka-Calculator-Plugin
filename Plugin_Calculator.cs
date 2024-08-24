using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using System.Reflection;

namespace Plugin_Calculator {

  /// <summary>
  /// The calculator plugin
  /// </summary>
  public partial class Calculator : Plugin {
    private Assembly? danglCalculator;
    private Type? calculator;
    private Type? result;
    private MethodInfo? calculate;
    PropertyInfo? resultProp;
    PropertyInfo? validityProp;

    private static PluginSettings pluginSettings = new();
    internal static PluginSettings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    /// <summary>
    /// Loads Plugin specific settings
    /// </summary>
    public Calculator() {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Calculator\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluggerName { get; set; } = "Calculator";

    private List<ListItem> ProduceItems(string query) {
      List<ListItem> ItemList = new();
      try {
        object? calculation = calculate!.Invoke(null, new object[] { query });
        if (calculation == null ? false : (bool) validityProp!.GetValue(calculation)) {
          ItemList.Add(
              new CalculationItem(resultProp!.GetValue(calculation)!.ToString()!)
          );
        }
      } catch (Exception) { }
      return ItemList;
    }

    /// <summary>
    /// Tries to calculate the query as a mathematical expression. If the expression is valid, a list item is made.
    /// </summary>
    public override List<ListItem> OnQueryChange(string query) {
      return ProduceItems(query);
    }

    /// <summary>
    /// <inheritdoc />
    /// Loads the Dangl.Calculator assembly.
    /// </summary>
    public override void OnAppStartup() {
      danglCalculator = Assembly.LoadFrom(
          Environment.CurrentDirectory
              + "\\PlugBoard\\Plugin_Calculator\\Plugin\\Dangl.Calculator.dll"
      );

      calculator = danglCalculator.GetType("Dangl.Calculator.Calculator")!;
      calculate = calculator.GetMethod("Calculate", new Type[] { typeof(string) })!;

      result = danglCalculator.GetType("Dangl.Calculator.CalculationResult")!;
      resultProp = result.GetProperty("Result")!;
      validityProp = result.GetProperty("IsValid")!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CalculatorSignifier from plugin settings
    /// </returns>
    public override List<string> CommandSignifiers() {
      return new List<string>() { PluginSettings.CalculatorSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CalculatorSignifier (Since there is only 1 signifier for this plugin), followed by the mathematical expression to be evaluated</param>
    public override List<ListItem> OnSignifier(string command) {
      return ProduceItems(command.Substring(PluginSettings.CalculatorSignifier.Length));
    }

  }
}
