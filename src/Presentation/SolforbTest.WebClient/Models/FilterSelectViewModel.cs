using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolforbTest.WebClient.Models
{
    public class FilterSelectViewModel : FilterViewModel
    {
        public IEnumerable<SelectListItem> Options { get; }
        public IEnumerable<string> SelectedOptions { get; }

        public FilterSelectViewModel(
            string filterHeader,
            string filterName,
            IEnumerable<string> values,
            IEnumerable<string>? selectedOptions = null
        )
            : this(
                filterHeader,
                filterName,
                values.Select(value => new SelectListItem(value, value)),
                selectedOptions ?? Enumerable.Empty<string>()
            )
        { }

        public FilterSelectViewModel(
            string filterHeader,
            string filterName,
            IEnumerable<SelectListItem> options,
            IEnumerable<string> selectedOptions
        )
            : base(filterHeader, filterName)
        {
            Options = options;
            SelectedOptions = selectedOptions;
        }
    }
}
