using System.Windows;
using Sodium;
using SWidgets;

namespace smart_date_picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Wrap FRP initialisation code in a transaction
            Transaction.RunVoid(() =>
            {
                //Create selection controls
                SButton weeks = new SButton { Content = "Weeks", Width = 75 };
                SButton months = new SButton { Content = "Months", Width = 75, Margin = new Thickness(5, 0, 0, 0) };
                SButton quarters = new SButton { Content = "Quarters", Width = 75, Margin = new Thickness(5, 0, 0, 0) };

                //Convert click event streams into PeriodType streams
                Stream<PeriodType> sWeeks = weeks.SClicked.Map(_ => PeriodType.Weeks);
                Stream<PeriodType> sMonths = months.SClicked.Map(_ => PeriodType.Months);
                Stream<PeriodType> sQuarters = quarters.SClicked.Map(_ => PeriodType.Quarters);

                //Merge the streams into a single stream with orElse
                Stream<PeriodType> sPeriodType = sWeeks.OrElse(sMonths).OrElse(sQuarters);

                //Hold a value of the stream in a cell, and also specifies Weeks as the default value
                Cell<PeriodType> cPeriodTypeCell = sPeriodType.Hold(PeriodType.Weeks);

                //Display the contents of the cell by mapping it to a string that SLabel can display
                Cell<string> cPeriodTypeString = cPeriodTypeCell.Map(p => p.ToString());
                SLabel lbl = new SLabel(cPeriodTypeString) { Width = 75, Margin = new Thickness(5, 0, 0, 0) };

                //Add controls to WPF StackPanel container
                Container.Children.Add(weeks);
                Container.Children.Add(months);
                Container.Children.Add(quarters);
                Container.Children.Add(lbl);
            });
        }
    }
}
