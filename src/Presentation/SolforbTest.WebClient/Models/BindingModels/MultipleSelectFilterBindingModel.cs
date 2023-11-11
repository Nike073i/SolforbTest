using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolforbTest.WebClient.Models.BindingModels
{
    public class MultipleSelectFilterBindingModel : FilterBindingModel
    {
        public IEnumerable<SelectListItem> Options { get; }
        public IEnumerable<string>? SelectedOptions { get; }

        public MultipleSelectFilterBindingModel(
            string filterHeader,
            string filterFormName,
            IEnumerable<string> values,
            IEnumerable<string>? selectedOptions = null
        )
            : this(
                filterHeader,
                filterFormName,
                values.Select(value => new SelectListItem(value, value)),
                selectedOptions ?? Enumerable.Empty<string>()
            )
        { }

        public MultipleSelectFilterBindingModel(
            string filterHeader,
            string filterFormName,
            IEnumerable<SelectListItem> options,
            IEnumerable<string> selectedOptions
        )
            : base(filterHeader, filterFormName)
        {
            Options = options;
            SelectedOptions = selectedOptions;
        }
    }
}
