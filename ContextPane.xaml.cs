using Quokka.ListItems;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static Quokka.Settings.SettingParsers;

namespace Plugin_Calculator {
  /// <summary>
  /// Interaction logic for ContextPane.xaml
  /// </summary>
  public partial class ContextPane : ItemContextPane {
    /// <summary>
    /// 
    /// </summary>
    public ContextPane() {
      InitializeComponent();
      #region settings
      SupportedFunctionsTable.GridLinesVisibility = DataGridGridLinesVisibility.None;
      //Grid
      this.Resources.Add("GridMargin", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridMargin));
      this.Resources.Add("GridPadding", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridPadding));
      this.Resources.Add("GridBackgroundColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridBackgroundColor)!);
      this.Resources.Add("GridBorderColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderColor)!);
      this.Resources.Add("GridBorderThickness", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridBorderThickness));
      this.Resources.Add("GridRounding", parseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRounding));
      this.Resources.Add("GridCellBorderColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderColor)!);
      this.Resources.Add("GridCellBorderThickness", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridCellBorderThickness));
      //Column Headers
      this.Resources.Add("GridColumnHeadersHorizontalAlignment", parseHorizontalAlignmentSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersHorizontalAlignment));
      this.Resources.Add("GridColumnHeadersBackground", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBackground)!);
      this.Resources.Add("GridColumnHeadersBorderColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderColor)!);
      this.Resources.Add("GridColumnHeadersBorderThickness", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersBorderThickness));
      this.Resources.Add("GridColumnHeadersRounding", parseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersRounding));
      this.Resources.Add("GridColumnHeadersTxtColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtColor)!);
      this.Resources.Add("GridColumnHeadersTxtSize", double.Parse(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtSize));
      this.Resources.Add("GridColumnHeadersTxtWeight", (FontWeight) new FontWeightConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridColumnHeaders.GridColumnHeadersTxtWeight)!);
      //Rows
      this.Resources.Add("GridRowColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowColor)!);
      this.Resources.Add("GridRowBorderColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderColor)!);
      this.Resources.Add("GridRowBorderThickness", parseThicknessSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowBorderThickness));
      this.Resources.Add("GridRowRounding", parseCornerRadiusSetting(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowRounding));
      this.Resources.Add("GridRowTxtColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtColor)!);
      this.Resources.Add("GridRowTxtSize", double.Parse(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.GridRowTxtSize));
      this.Resources.Add("SelectedGridRowTxtColor", (SolidColorBrush) new BrushConverter().ConvertFromString(Calculator.PluginSettings.SupportedFunctionsGrid.GridRows.SelectedGridRowTxtColor)!);
      #endregion
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void Page_KeyDown(object sender, KeyEventArgs e) {
      switch (e.Key) {
        case Key.Enter:
          //copy expression from selected row
          break;
        case Key.Apps: //This is the menu key
          base.ReturnToSearch();
          break;
        default:
          return;
      }
      e.Handled = true;
    }


  }

  /// <summary>
  /// 
  /// </summary>
  public class SupportedFunction {
    /// <summary>
    /// 
    /// </summary>
    public string? Expression { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
  }

}
