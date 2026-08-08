using Quokka.ListItems;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Keys = System.Windows.Forms.Keys;
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
      Resources.Add("GridMargin", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridMargin));
      Resources.Add("GridPadding", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridPadding));
      Resources.Add("GridBackgroundColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridBackgroundColor));
      Resources.Add("GridBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderColor));
      Resources.Add("GridBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderThickness));
      Resources.Add("GridRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRounding));
      Resources.Add("GridCellBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderColor));
      Resources.Add("GridCellBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderThickness));
      //Column Headers
      Resources.Add("GridColumnHeadersHorizontalAlignment", ParseHorizontalAlignmentSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersHorizontalAlignment));
      Resources.Add("GridColumnHeadersBackground", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBackground));
      Resources.Add("GridColumnHeadersBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderColor));
      Resources.Add("GridColumnHeadersBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderThickness));
      Resources.Add("GridColumnHeadersRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersRounding));
      Resources.Add("GridColumnHeadersTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtColor));
      Resources.Add("GridColumnHeadersTxtSize", ParseDoubleSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtSize));
      Resources.Add("GridColumnHeadersTxtWeight", ParseFontWeight(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtWeight));
      //Rows
      Resources.Add("GridRowColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowColor));
      Resources.Add("GridRowBorderColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderColor));
      Resources.Add("GridRowBorderThickness", ParseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderThickness));
      Resources.Add("GridRowRounding", ParseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowRounding));
      Resources.Add("GridRowTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtColor));
      Resources.Add("GridRowTxtSize", ParseDoubleSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtSize));
      Resources.Add("SelectedGridRowTxtColor", ParseSolidColorBrush(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.SelectedGridRowTxtColor));
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
        if (e.Key == Key.Enter)
        {
          if (SupportedFunctionsTable.SelectedItem is SupportedFunction function && function.Expression is not null)
          {
            Clipboard.SetText(function.Expression);
          }
          ReturnToSearch();
          e.Handled = true;
        }
        else if (e.Key == KeyInterop.KeyFromVirtualKey((int)(Keys)Application.Current.Resources["ContextPaneKey"]))
        {
          ReturnToSearch();
          e.Handled = true;
        }
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
