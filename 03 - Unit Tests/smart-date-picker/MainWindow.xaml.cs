using Sodium;
using SWidgets;
using System.Windows;

namespace smart_date_picker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataAvailabilityService mDataAvailabilityService;

        public MainWindow()
        {
            InitializeComponent();
            mDataAvailabilityService = new DataAvailabilityService();
            
            //Wrap FRP initialisation code in a transaction
            Transaction.RunVoid(() =>
            {
                //Create period selection controls
                var weeks = new SButton {Content = "Weeks", Width = 75};
                var months = new SButton {Content = "Months", Width = 75};
                var quarters = new SButton {Content = "Quarters", Width = 75};

                var startDate = new SDateField();
                var endDate = new SDateField();
                
                //Convert click event streams into PeriodType streams
                Stream<PeriodType> sWeeks = weeks.SClicked.Map(_ => PeriodType.Weeks);
                Stream<PeriodType> sMonths = months.SClicked.Map(_ => PeriodType.Months);
                Stream<PeriodType> sQuarters = quarters.SClicked.Map(_ => PeriodType.Quarters);

                //Merge the streams into a single stream with orElse
                Stream<PeriodType> sPeriodType = sWeeks.OrElse(sMonths).OrElse(sQuarters);

                //Hold a value of the stream in a cell, and specifies Weeks as the initial value
                Cell<PeriodType> cPeriodType = sPeriodType.Hold(PeriodType.Weeks);

                //Which date has priority?
                Stream<DatePriority> sStartPriority = startDate.SelectedDateChanged.Map(_ => DatePriority.StartDate);
                Stream<DatePriority> sEndPriority = endDate.SelectedDateChanged.Map(_ => DatePriority.EndDate);
                Stream<DatePriority> sDatePriority = sStartPriority.OrElse(sEndPriority);
                Cell<DatePriority> cDatePriority = sDatePriority.Hold(DatePriority.StartDate);

                //Make a DateRange
                Cell<DateRange> cDateRange = startDate.SelectedDate.Lift(endDate.SelectedDate,
                    mDataAvailabilityService.DataAvailability, cPeriodType, cDatePriority,
                    ReportDateRangeBumper.BumpDates);

                //Display the contents of the cell by mapping it to a string cell
                Cell<string> cPeriodTypeString = cPeriodType.Map(period => period.ToString());
                var periodTypeLabel = new SLabel(cPeriodTypeString) { Width = 75, Margin = new Thickness(5, 0, 0, 0)};
                var selectedDateLabel = new SLabel(cDateRange.Map(dateRange => dateRange.ToString()));

                //Add controls to relevante WPF containers
                Container.Children.Add(weeks);
                Container.Children.Add(months);
                Container.Children.Add(quarters);

                DateRangeContainer.Children.Add(startDate);
                DateRangeContainer.Children.Add(endDate);
                
                ResultsContainer.Children.Add(periodTypeLabel);
                ResultsContainer.Children.Add(selectedDateLabel);

            });
        }
    }
}