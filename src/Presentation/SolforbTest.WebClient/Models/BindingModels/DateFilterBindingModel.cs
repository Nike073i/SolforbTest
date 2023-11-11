namespace SolforbTest.WebClient.Models.BindingModels
{
    public class DateFilterBindingModel : FilterBindingModel
    {
        public DateTime? SelectedDate { get; set; }

        public DateFilterBindingModel(
            string filterHeader,
            string filterFormName,
            DateTime? defaultDate = null
        )
            : base(filterHeader, filterFormName)
        {
            SelectedDate = defaultDate;
        }
    }
}
