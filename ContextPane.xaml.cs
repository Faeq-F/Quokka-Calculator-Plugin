using Quokka;
using Quokka.ListItems;
using System.Windows.Controls;
using System.Windows.Input;
using static Quokka.Settings.SettingParsers;

namespace PluginCalculator
{

  /// <summary>
  /// The context pane for a calculation item
  /// </summary>
  public partial class ContextPane : ItemContextPane
  {

    /// <summary>
    /// Loads the style settings for the Supported Functions grid
    /// </summary>
    public ContextPane()
    {
      InitializeComponent();
      #region settings
      SupportedFunctionsTable.GridLinesVisibility = DataGridGridLinesVisibility.None;
      //Grid
      this.Resources.Add("GridMargin", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridMargin));
      this.Resources.Add("GridPadding", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridPadding));
      this.Resources.Add("GridBackgroundColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridBackgroundColor));
      this.Resources.Add("GridBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderColor));
      this.Resources.Add("GridBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderThickness));
      this.Resources.Add("GridRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRounding));
      this.Resources.Add("GridCellBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderColor));
      this.Resources.Add("GridCellBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderThickness));
      //Column Headers
      this.Resources.Add("GridColumnHeadersHorizontalAlignment", ParseHorizontalAlignmentSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersHorizontalAlignment));
      this.Resources.Add("GridColumnHeadersBackground", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBackground));
      this.Resources.Add("GridColumnHeadersBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderColor));
      this.Resources.Add("GridColumnHeadersBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderThickness));
      this.Resources.Add("GridColumnHeadersRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersRounding));
      this.Resources.Add("GridColumnHeadersTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtColor));
      this.Resources.Add("GridColumnHeadersTxtSize", ParseDoubleSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtSize));
      this.Resources.Add("GridColumnHeadersTxtWeight", ParseFontWeight(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtWeight));
      //Rows
      this.Resources.Add("GridRowColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowColor));
      this.Resources.Add("GridRowBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderColor));
      this.Resources.Add("GridRowBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderThickness));
      this.Resources.Add("GridRowRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowRounding));
      this.Resources.Add("GridRowTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtColor));
      this.Resources.Add("GridRowTxtSize", ParseDoubleSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtSize));
      this.Resources.Add("SelectedGridRowTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.SelectedGridRowTxtColor));
      #endregion
    }

    /// <summary>
    /// Enter key copies the expression from the selected row.
    /// <inheritdoc/>
    /// </summary>
    /// <param name="sender"><inheritdoc/></param>
    /// <param name="e"><inheritdoc/></param>
    protected override void PageKeyDown(object sender, KeyEventArgs e)
    {
      if (e != null)
      {
        switch (e.Key)
        {
          case Key.Enter:
            //copy expression from selected row
            SupportedFunction function = (SupportedFunction)SupportedFunctionsTable.SelectedItem;
            System.Windows.Clipboard.SetText(function.Expression);
            ReturnToSearch();
            break;
          case var value when value == (System.Windows.Input.Key)App.Current.Resources["ContextPaneKey"]:
            ReturnToSearch();
            break;
          default:
            return;
        }
        e.Handled = true;
      }
    }
  }

  /// <summary>
  /// A Supported Function - a row in the grid
  /// </summary>
  public class SupportedFunction
  {
    /// <summary>
    /// The expression that is typed out
    /// </summary>
    public string? Expression { get; set; }
    /// <summary>
    /// Description of the function
    /// </summary>
    public string? Description { get; set; }
  }

}
