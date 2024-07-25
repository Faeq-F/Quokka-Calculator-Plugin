using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Plugin_Calculator {
  class CalculationItem : ListItem {
    public CalculationItem(string calculation, string description) {
      this.Name = calculation;
      this.Description = description;
      this.Icon = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Calculator\\Plugin\\calculator.png"));
    }

    public override void Execute() {

    }
  }

  /// <summary>
  /// 
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
    /// 
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
    /// This will get called when user types a query into the search field
    /// </summary>
    public List<ListItem> OnQueryChange(string query) {
      List<ListItem> ItemList = new List<ListItem>();

      object calculation = calculate!.Invoke(null, new object[] { query })!;
      if ((bool) validityProp!.GetValue(calculation)!) {
        ItemList.Add(
            new CalculationItem(resultProp!.GetValue(calculation)!.ToString()!, "Menu key will show all supported functions")
        );
      }

      return ItemList;
    }

    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    public List<String> SpecialCommands() {
      return new List<String>();
    }

    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    public List<ListItem> OnSpecialCommand(string command) {
      return new List<ListItem>();
    }

    ///Loads the Dangl.Calculator assembly 
    /// <inheritdoc />
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

    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    public void OnAppShutdown() { }

    /// Does Nothing - Inherited from IPlugger;
    /// <inheritdoc />
    public void OnSearchWindowStartup() { }
  }
}
