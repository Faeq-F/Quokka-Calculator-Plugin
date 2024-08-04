using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using System.Reflection;

namespace Plugin_Calculator {

  /// <summary>
  /// The calculator plugin
  /// </summary>
  public partial class Calculator : IPlugger {
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
      //Get Plugin Specific settings
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Calculator\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string PluggerName { get; set; } = "Calculator";

    /// <summary>
    /// Tries to calculate the query as a mathematical expression. If the expression is valid, a list item is made.
    /// </summary>
    public List<ListItem> OnQueryChange(string query) {
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
    /// <inheritdoc />
    /// Loads the Dangl.Calculator assembly.
    /// </summary>
    public void OnAppStartup() {
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

    #region do nothing

    /// <summary>
    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    /// </summary>
    public void OnAppShutdown() { }

    /// <summary>
    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    /// </summary>
    public void OnSearchWindowStartup() { }

    /// <summary>
    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    /// </summary>
    public List<String> SpecialCommands() {
      return new List<String>();
    }

    /// <summary>
    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    /// </summary>
    public List<ListItem> OnSpecialCommand(string command) {
      return new List<ListItem>();
    }

    #endregion
  }
}
