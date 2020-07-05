using city.core.entities;
using city.core.repositories;
using city.core.services;
using crud.api.core.enums;
using crud.api.core.fieldType;
using crud.api.core.interfaces;
using crud.api.core.repositories;
using data.provider.core.mongo;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace city.test
{
    public class ExcelFileReadTest
    {

        [Fact]
        public void ImportStateData()
        {
            var path = @"D:\Projetos\DotNet Core\city-api\city.api\city.test\Lista-de-Municípios-com-IBGE-Brasil.xlsx";

            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Initials = "BRA",
                Language = "pt_BR",
                TimeZone1 = "America/Sao_Paulo",
                TimeZone2 = "E. South America Standard Time",
                LastChangeDate = DateTime.UtcNow,
                Name = "Brasil",
                RegisterDate = DateTime.UtcNow,
                Status = RecordStatus.Active
            };

            var stream = new FileInfo(path);
            var service = new CityService(new CityRepository(new DataProvider(new MongoClientFactory(), "crud-api")));
            var states = service.LoadExcelStateData(stream, country);

            var stateResult = new List<IHandleMessage>();
            var cityResult = new List<IHandleMessage>();

            var countryRepository = new BaseRepository<Country>(new DataProvider(new MongoClientFactory(), "crud-api"));

            countryRepository.AppenData(country);

            var repository = new BaseRepository<State>(new DataProvider(new MongoClientFactory(), "crud-api"));

            foreach (var item in states)
            {
                var response = repository.AppenData(item);

                stateResult.AddRange(response);
            }

            var cities = service.LoadExcelData(stream, states);

            foreach (var item in cities)
            {
                var response = service.SaveData(item);

                cityResult.AddRange(response);
            }

            Assert.DoesNotContain(stateResult, x => !x.Code.Equals(HandlesCode.Accepted));
            Assert.DoesNotContain(cityResult, x => !x.Code.Equals(HandlesCode.Accepted));
        }
    }
}
