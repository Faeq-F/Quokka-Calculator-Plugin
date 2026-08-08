using Quokka.ListItems;
using Quokka.PluginArch;
using System.Windows;

namespace PluginCalculator
{
  internal sealed class CalculationItem : ListItem
  {

    /// <summary>
    /// A list item to show the result of a mathematical calculation. The name is the result, the description tells the user that the menu key shows all supported functions (in the context pane) and the icon used is calculator.png.
    /// </summary>
    /// <param name="calculationResult">The result of evaluating the mathematical expression</param>
    public CalculationItem(string calculationResult)
    {
      Name = calculationResult;
      Description = "Menu key will show all supported functions";
      Icon = IconCache.GetOrAdd(
        Environment.CurrentDirectory + "\\PlugBoard\\PluginCalculator\\Plugin\\calculator.png"
      );
    }

    /// <summary>
    /// Copies the result to the clipboard and closes the search window
    /// </summary>
    public override void Execute()
    {
      Clipboard.SetText(Name);
      Application.Current.MainWindow.Close();
    }
  }
}
