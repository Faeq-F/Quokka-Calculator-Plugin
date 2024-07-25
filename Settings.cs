public class GridColumnHeaders {
  public string GridColumnHeadersHorizontalAlignment { get; set; }
  public string GridColumnHeadersBackground { get; set; }
  public string GridColumnHeadersBorderColor { get; set; }
  public string GridColumnHeadersBorderThickness { get; set; }
  public string GridColumnHeadersRounding { get; set; }
  public string GridColumnHeadersTxtColor { get; set; }
  public string GridColumnHeadersTxtSize { get; set; }
  public string GridColumnHeadersTxtWeight { get; set; }
}

public class GridRows {
  public string GridRowColor { get; set; }
  public string GridRowBorderColor { get; set; }
  public string GridRowBorderThickness { get; set; }
  public string GridRowRounding { get; set; }
  public string GridRowTxtColor { get; set; }
  public string GridRowTxtSize { get; set; }
  public string SelectedGridRowTxtColor { get; set; }
}

internal class PluginSettings {
  public SupportedFunctionsGrid SupportedFunctionsGrid { get; set; }
}

public class SupportedFunctionsGrid {
  public string GridMargin { get; set; }
  public string GridPadding { get; set; }
  public string GridBackgroundColor { get; set; }
  public string GridBorderColor { get; set; }
  public string GridBorderThickness { get; set; }
  public string GridRounding { get; set; }
  public GridColumnHeaders GridColumnHeaders { get; set; }
  public GridRows GridRows { get; set; }
}

