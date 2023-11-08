namespace SolforbTest.WebClient.Models
{
    public abstract class FilterViewModel
    {
        public string FilterHeader { get; }
        public string FilterName { get; }

        public FilterViewModel(string filterHeader, string filterName)
        {
            FilterHeader = filterHeader;
            FilterName = filterName;
        }
    }
}
