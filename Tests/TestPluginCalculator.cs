using System.IO;

namespace PluginCalculator.Tests
{
  /// <summary>
  /// Integration tests for the <see cref="Calculator"/> plugin.
  /// </summary>
  public class TestPluginCalculator : IDisposable
  {
    private readonly string _tempPath;
    private readonly string _originalCurrentDirectory;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestPluginCalculator"/> class and prepares the sandboxed plugboard environment.
    /// </summary>
    public TestPluginCalculator()
    {
      _originalCurrentDirectory = Environment.CurrentDirectory;

      // Clean up legacy directories from previous runs
      try
      {
        foreach (var dir in Directory.GetDirectories(_originalCurrentDirectory, "TempCalculator_*"))
        {
          try
          {
            Directory.Delete(dir, recursive: true);
          }
          catch
          {
            // Ignore if locked
          }
        }
      }
      catch
      {
      }

      string uniqueId = Guid.NewGuid().ToString("N");
      _tempPath = Path.Combine(_originalCurrentDirectory, "TempCalculator_" + uniqueId);
      Directory.CreateDirectory(_tempPath);

      // Create target directory structure matching expected plugin path
      string targetPluginDir = Path.Combine(_tempPath, "PlugBoard", "PluginCalculator", "Plugin");
      Directory.CreateDirectory(targetPluginDir);

      // Copy DLL and settings.json from the build output
      string buildOutputPlugBoard = @"C:\Users\faeqf\Documents\git\Quokka\BuildOutput\Quokka\PlugBoard";
      string sourcePluginDir = Path.Combine(buildOutputPlugBoard, "PluginCalculator", "Plugin");

      if (Directory.Exists(sourcePluginDir))
      {
        foreach (var file in Directory.GetFiles(sourcePluginDir))
        {
          File.Copy(file, Path.Combine(targetPluginDir, Path.GetFileName(file)), overwrite: true);
        }
      }

      // Redirect CurrentDirectory
      Environment.CurrentDirectory = _tempPath;
    }

    /// <summary>
    /// Verifies that the plugin correctly loads its settings and executes calculation queries.
    /// </summary>
    [Fact]
    public void TestCalculator_CalculatesMathematicalExpressions()
    {
      // 1. Instantiate the plugin (reads settings.json)
      var calc = new Calculator();

      // 2. Initialize it (loads Dangl.Calculator.dll)
      calc.OnAppStartup();

      // 3. Test CommandSignifiers
      calc.CommandSignifiers().Should().ContainSingle().Which.Should().Be("= ");

      // 4. Test math evaluation on query change
      var results = calc.OnQueryChange("5 + 5 * 2");
      results.Should().ContainSingle();
      results.First().Name.Should().Be("15");

      // 5. Test signifier calculation execution
      var signifierResults = calc.OnSignifier("= (10 - 2) / 4");
      signifierResults.Should().ContainSingle();
      signifierResults.First().Name.Should().Be("2");

      // 6. Test invalid math expressions do not crash and return empty
      var invalidResults = calc.OnQueryChange("5 + ");
      invalidResults.Should().BeEmpty();
    }

    /// <summary>
    /// Restores the original current directory and cleans up the sandbox files.
    /// </summary>
    public void Dispose()
    {
      Environment.CurrentDirectory = _originalCurrentDirectory;
      try
      {
        if (Directory.Exists(_tempPath))
        {
          Directory.Delete(_tempPath, recursive: true);
        }
      }
      catch
      {
        // Ignore locked files
      }
    }
  }
}
