namespace SolforbTest.WebClient.Models.BindingModels
{
    public abstract class FilterBindingModel
    {
        public string FilterHeader { get; }
        public string FilterFormName { get; }

        public FilterBindingModel(string filterHeader, string filterFormName)
        {
            FilterHeader = filterHeader;
            FilterFormName = filterFormName;
        }
    }
}
