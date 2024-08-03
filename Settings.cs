/// <summary>
/// Style settings for the Supported Functions grid's headers
/// </summary>
public class GridColumnHeaders {
  /// <summary>
  /// The horizontal alignment of text
  /// </summary>
  public string GridColumnHeadersHorizontalAlignment { get; set; } = "Center";
  /// <summary>
  /// The background color
  /// </summary>
  public string GridColumnHeadersBackground { get; set; } = "#fff";
  /// <summary>
  /// The border color
  /// </summary>
  public string GridColumnHeadersBorderColor { get; set; } = "#ddd";
  /// <summary>
  /// The border thickness
  /// </summary>
  public string GridColumnHeadersBorderThickness { get; set; } = "0, 0, 2, 2";
  /// <summary>
  /// The corner radius
  /// </summary>
  public string GridColumnHeadersRounding { get; set; } = "8, 8, 0, 0";
  /// <summary>
  /// The text color
  /// </summary>
  public string GridColumnHeadersTxtColor { get; set; } = "Black";
  /// <summary>
  /// The text size
  /// </summary>
  public string GridColumnHeadersTxtSize { get; set; } = "13";
  /// <summary>
  /// The boldness of the text
  /// </summary>
  public string GridColumnHeadersTxtWeight { get; set; } = "Medium";
}

/// <summary>
/// Style settings for the Supported Functions grid's rows
/// </summary>
public class GridRows {
  /// <summary>
  /// The row color (background color)
  /// </summary>
  public string GridRowColor { get; set; } = "#fff";
  /// <summary>
  /// The row border color
  /// </summary>
  public string GridRowBorderColor { get; set; } = "#ddd";
  /// <summary>
  /// The border thickness
  /// </summary>
  public string GridRowBorderThickness { get; set; } = "0, 2, 3, 0";
  /// <summary>
  /// The corner radius
  /// </summary>
  public string GridRowRounding { get; set; } = "0";
  /// <summary>
  /// The text color
  /// </summary>
  public string GridRowTxtColor { get; set; } = "#777";
  /// <summary>
  /// The text size
  /// </summary>
  public string GridRowTxtSize { get; set; } = "12";
  /// <summary>
  /// The row's text color when the row is selected
  /// </summary>
  public string SelectedGridRowTxtColor { get; set; } = "Black";
}

/// <summary>
/// All plugin specific settings
/// </summary>
public class PluginSettings {
  /// <summary>
  /// The style settings for the Supported Functions grid
  /// </summary>
  public SupportedFunctionsGrid SupportedFunctionsGrid { get; set; } = new();
}

/// <summary>
/// The style settings for the Supported Functions grid
/// </summary>
public class SupportedFunctionsGrid {
  /// <summary>
  /// The grid's margin
  /// </summary>
  public string GridMargin { get; set; } = "10";
  /// <summary>
  /// The grid's padding
  /// </summary>
  public string GridPadding { get; set; } = "0";
  /// <summary>
  /// The grid's background color
  /// </summary>
  public string GridBackgroundColor { get; set; } = "Transparent";
  /// <summary>
  /// The grid's border color
  /// </summary>
  public string GridBorderColor { get; set; } = "#ddd";
  /// <summary>
  /// The grid's border thickness
  /// </summary>
  public string GridBorderThickness { get; set; } = "2";
  /// <summary>
  /// The corner radius
  /// </summary>
  public string GridRounding { get; set; } = "8";
  /// <summary>
  /// The border color of cells in the grid
  /// </summary>
  public string GridCellBorderColor { get; set; } = "#ddd";
  /// <summary>
  /// The border thickness of cells in the grid
  /// </summary>
  public string GridCellBorderThickness { get; set; } = "0, 0, 2, 0";
  /// <summary>
  /// Style settings for the Supported Functions grid's headers
  /// </summary>
  public GridColumnHeaders GridColumnHeaders { get; set; } = new();
  /// <summary>
  /// Style settings for the Supported Functions grid's rows
  /// </summary>
  public GridRows GridRows { get; set; } = new();
}

