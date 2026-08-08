using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace PluginCalculator
{

  /// <summary>
  /// The calculator plugin
  /// </summary>
#pragma warning disable CA1724 // Type name conflicts with Dangl.Calculator namespace name
  public partial class Calculator : Plugin
  {
    private Assembly? danglCalculator;
    private Type? calculator;
    private Type? result;
    private MethodInfo? calculate;
    private PropertyInfo? resultProp;
    private PropertyInfo? validityProp;
    private static readonly Lazy<PluginSettings> _lazyPluginSettings = new(() =>
    {
      string fileName = Path.Combine(Environment.CurrentDirectory, "PlugBoard", "PluginCalculator", "Plugin", "settings.json");
      return File.Exists(fileName)
        ? JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName)) ?? new PluginSettings()
        : new PluginSettings();
    });

    internal static PluginSettings PluginSettings => _lazyPluginSettings.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    public Calculator() { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluginName { get; set; } = "Calculator";

    private Collection<ListItem> ProduceItems(string query)
    {
      Collection<ListItem> ItemList = new();
      if (calculate is null || validityProp is null || resultProp is null)
      {
        return ItemList;
      }

      try
      {
        object? calculation = calculate.Invoke(null, [query]);
        if (calculation != null)
        {
          object? isValidObj = validityProp.GetValue(calculation);
          if (isValidObj is bool isValid && isValid)
          {
            object? resultObj = resultProp.GetValue(calculation);
            if (resultObj?.ToString() is string calculationResult)
            {
              ItemList.Add(new CalculationItem(calculationResult));
            }
          }
        }
      }
      catch (TargetInvocationException)
      {
        // Ignore evaluation exceptions thrown by Dangl.Calculator when parsing invalid or partial math queries
      }
      return ItemList;
    }

    /// <summary>
    /// Tries to calculate the query as a mathematical expression. If the expression is valid, a list item is made.
    /// </summary>
    public override Collection<ListItem> OnQueryChange(string query)
    {
      return ProduceItems(query);
    }

    /// <summary>
    /// <inheritdoc />
    /// Loads the Dangl.Calculator assembly.
    /// </summary>
    public override void OnAppStartup()
    {
      string dllPath = Path.Combine(Environment.CurrentDirectory, "PlugBoard", "PluginCalculator", "Plugin", "Dangl.Calculator.dll");
      if (!File.Exists(dllPath))
      {
        return;
      }

      try
      {
        danglCalculator = Assembly.LoadFrom(dllPath);
        calculator = danglCalculator?.GetType("Dangl.Calculator.Calculator");
        calculate = calculator?.GetMethod("Calculate", [typeof(string)]);

        result = danglCalculator?.GetType("Dangl.Calculator.CalculationResult");
        resultProp = result?.GetProperty("Result");
        validityProp = result?.GetProperty("IsValid");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading Dangl.Calculator assembly: {ex.Message}");
      }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CalculatorSignifier from plugin settings
    /// </returns>
    public override Collection<string> CommandSignifiers()
    {
      return new Collection<string>() { PluginSettings.CalculatorSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CalculatorSignifier (Since there is only 1 signifier for this plugin), followed by the mathematical expression to be evaluated</param>
    public override Collection<ListItem> OnSignifier(string command)
    {
      command ??= "";
      return ProduceItems(command.Substring(PluginSettings.CalculatorSignifier.Length));
    }

  }
}
