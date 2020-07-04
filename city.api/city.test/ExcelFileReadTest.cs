using city.core.entities;
using city.core.repositories;
using city.core.services;
using crud.api.core.enums;
using crud.api.core.interfaces;
using crud.api.core.repositories;
using data.provider.core.mongo;
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
        public void ReadFile()
        {
            var path = @"D:\Projetos\DotNet Core\city-api\city.core\city.test\Lista-de-Municípios-com-IBGE-Brasil.xlsx";

            var stream = new FileInfo(path);
            var service = new CityService(null);
            var cities = service.LoadExcelData(stream);

            Assert.True(cities.Count == 5570);
        }

        [Fact]
        public void ImportCityData()
        {
            var path = @"D:\Projetos\DotNet Core\city-api\city.api\city.test\Lista-de-Municípios-com-IBGE-Brasil.xlsx";

            var stream = new FileInfo(path);
            var service = new CityService(new CityRepository(new DataProvider(new MongoClientFactory(), "city")));
            var cities = service.LoadExcelData(stream);

            var result = new List<IHandleMessage>();

            foreach (var item in cities)
            {
                var response = service.SaveData(item);

                result.AddRange(response);
            }

            Assert.DoesNotContain(result, x => !x.Code.Equals(HandlesCode.Accepted));
        }

        [Fact]
        public void ImportStateData()
        {
            var path = @"D:\Projetos\DotNet Core\city-api\city.api\city.test\Lista-de-Municípios-com-IBGE-Brasil.xlsx";

            var stream = new FileInfo(path);
            var service = new CityService(new CityRepository(new DataProvider(new MongoClientFactory(), "city")));
            var cities = service.LoadExcelStateData(stream);

            var result = new List<IHandleMessage>();

            foreach (var item in cities)
            {
                var repository = new BaseRepository<State>(new DataProvider(new MongoClientFactory(), "city"));
                var response = repository.AppenData(item);

                result.AddRange(response);
            }

            Assert.DoesNotContain(result, x => !x.Code.Equals(HandlesCode.Accepted));
        }
    }
}
