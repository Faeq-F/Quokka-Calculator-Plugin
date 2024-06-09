using Quokka.PluginArch;
using Quokka.ListItems;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using WinCopies.Util;
namespace Plugin_Calculator
{
    class CalculationItem : ListItem
    {        
        public CalculationItem(string calculation, string description)
        {
            this.Name = calculation;
            this.Description = description;
            this.Icon = new BitmapImage(new Uri(
                Environment.CurrentDirectory + "\\Config\\Resources\\information.png"));
        }

        public override void Execute()
        {
            
        }
    }
   

    public partial class Calculator : IPlugger
    {

        private Assembly danglCalculator;
        private Type calculator;
        private Type result;
        private MethodInfo calculate;
        PropertyInfo resultProp;
        PropertyInfo validityProp;

        public Calculator() { }

        public string PluggerName { get; set; } = "Calculator";

        /// <summary>
        /// This will get called when user types a query into the search field
        /// </summary>
        public List<ListItem> OnQueryChange(string query)
        {
            List<ListItem> ItemList = new List<ListItem>();
            object calculation = calculate.Invoke(null, new object[] { query } );
            if ((bool) validityProp.GetValue(calculation))
            {
                ItemList.Add(new CalculationItem(query + " = " + resultProp.GetValue(calculation).ToString() , query));
            }
            return ItemList;
        }

        public List<String> SpecialCommands()
        {
            return new List<String>();
        }

        public List<ListItem> OnSpecialCommand(string command)
        {
            return new List<ListItem>();
        }

        public void OnAppStartup() {
          danglCalculator = Assembly.LoadFrom(Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Calculator\\Plugin\\Dangl.Calculator.dll");

          calculator = danglCalculator.GetType("Dangl.Calculator.Calculator");
          calculate = calculator.GetMethod("Calculate", new Type[] { typeof(string) });

          result = danglCalculator.GetType("Dangl.Calculator.CalculationResult");
          resultProp = result.GetProperty("Result");
          validityProp = result.GetProperty("IsValid");
        }

        public void OnAppShutdown() { }

        public void OnSearchWindowStartup() { }

    }

}
