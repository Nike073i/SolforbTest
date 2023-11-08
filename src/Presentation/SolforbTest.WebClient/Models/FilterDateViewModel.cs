namespace SolforbTest.WebClient.Models
{
    public class FilterDateViewModel : FilterViewModel
    {
        public DateTime SelectedDate { get; set; }

        public FilterDateViewModel(string filterHeader, string filterName, DateTime DefaultDate)
            : base(filterHeader, filterName)
        {
            SelectedDate = DefaultDate;
        }
    }
}
