using Cashier.Models.Articles;
using Cashier.Models.Countries;
using Entities;
using Entities.Articles;
using SharpCompress.Common;

namespace Cashier.Helpers
{
    public class CountryMapper
    {
        public static CountryViewModel ToCountryViewModel(Country entity)
        {
            return new CountryViewModel
            {
                Id = entity.Id,
                IsoNumericCode = entity.IsoNumericCode,
                Name = entity.Name,
                Alpha2Code = entity.Alpha2Code,
                Alpha3Code = entity.Alpha3Code,
                ContinentCode = entity.ContinentCode
            };
        }

        public static Country ToCountryEntity(CountryViewModel countryViewModel, string modifiedBy = "admin")
        {
            return new Country
            {
                Id = countryViewModel.Id,
                IsoNumericCode = countryViewModel.IsoNumericCode,
                Name = countryViewModel.Name,
                Alpha2Code = countryViewModel.Alpha2Code,
                Alpha3Code = countryViewModel.Alpha3Code,
                ContinentCode = countryViewModel.ContinentCode,
                ModifiedOn = DateTime.UtcNow,
                ModifiedBy = modifiedBy
            };
        }
    }
}
