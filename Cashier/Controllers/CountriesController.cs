using Cashier.Helpers;
using Cashier.Models.Articles;
using Cashier.Models.Countries;
using Entities;
using Entities.Articles;
using InfrastructureMongoDB;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cashier.Controllers
{
    
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IRepository<Country> _countryRepository;

        public CountriesController(
           ILogger<CountriesController> logger,
           IRepository<Country> countryRepository)
        {
            _logger = logger;
            _countryRepository = countryRepository;
           
        }
        public IActionResult Index()
        {
            _logger.LogInformation("CountriesController.Index");
            return View();
        }

        public async Task<IActionResult> GetAllCountries()
        {
            _logger.LogInformation("CountriesController.GetAllCountries");
            var countries = await _countryRepository.GetAll();
            var countriesViewModelList = new List<CountryViewModel>();
            foreach (var country in countries)
            {
                var countryViewModel = new CountryViewModel
                {
                    Id = country.Id,
                    IsoNumericCode = country.IsoNumericCode,
                    Name = country.Name,
                    Alpha2Code = country.Alpha2Code,
                    Alpha3Code = country.Alpha3Code,
                    ContinentCode = country.ContinentCode
                };
                countriesViewModelList.Add(countryViewModel);
            }
            _logger.LogInformation($"CountriesController.GetAllCountries return: {countriesViewModelList}");
            return Json(countriesViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCountry([FromBody] CountryViewModel country)
        {
            try
            {
                _logger.LogInformation($"CountriesController.AddNewCountry country: {country}");
                if (ModelState.IsValid)
                {
                    var newCountry = CountryMapper.ToCountryEntity(country);

                    await _countryRepository.Add(newCountry);
                    _logger.LogInformation($"CountriesController.AddNewCountry country id={newCountry.Id} created successfuly");
                    return Json(new { success = true, description = "Country added successfully" });
                }
                _logger.LogInformation($"CountriesController.AddNewCountry validation error");
                return Json(new { success = false, description = "Country not added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"CountriesController.CreateNewArticle exception", ex);
                return Json(new { success = false, description = ex.Message });
            }
        }

        public async Task<IActionResult> GetCountry(long id)
        {
            try
            {
                _logger.LogInformation($"CountriesController.GetCountry id={id}");
                var country = await _countryRepository.GetById(id);

                var countryViewModel = new CountryViewModel();
                if (country != null)
                {
                    countryViewModel = CountryMapper.ToCountryViewModel(country);

                }
                return View("Country", countryViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CountriesController.GetArticle exception", ex);
                throw;
            }
        }
    }
}
