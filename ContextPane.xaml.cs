using Quokka.ListItems;


namespace Plugin_Calculator
{
    /// <summary>
    /// Interaction logic for ContextPane.xaml
    /// </summary>
    public partial class ContextPane : ItemContextPane
    {
        /// <summary>
        /// 
        /// </summary>
        public ContextPane()
        {
            InitializeComponent();
            base.ReturnToSearch();
        }
    }
}
