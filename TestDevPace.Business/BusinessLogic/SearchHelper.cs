using TestDevPace.Business.Models;

namespace TestDevPace.Business.BusinessLogic
{
    public static class SearchHelper
    {
        public static object? SortByField(this CustomerModel model, SearchAndSort search)
        {
            return model
                    .GetType()
                    .GetProperty(search.Category.ToString())
                    .GetValue(model);
        }

        public static IEnumerable<CustomerModel> SearchWithFilters(this IEnumerable<CustomerModel> customers, SearchAndSort search, string text)
        {
            try
            {
                return customers.Where(model => model.SortByField(search).ToString().Contains(text));
            }catch(Exception)
            {
                return Enumerable.Empty<CustomerModel>();
            }
        }
    }
}
