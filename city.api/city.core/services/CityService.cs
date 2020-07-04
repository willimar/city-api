using city.core.entities;
using crud.api.core.fieldType;
using crud.api.core.repositories;
using crud.api.core.services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace city.core.services
{
    public class CityService : BaseService<City>
    {
        public CityService(IRepository<City> repository) : base(repository)
        {
        }

        private List<State> GetStates(ExcelWorksheet states)
        {
            int rows = states.Dimension.Rows;

            const int IBGE = 1;
            const int Estado = 2;
            const int UF = 3;
            const int Regiao = 4;
            const int QtdMun = 5;

            var result = new List<State>();

            for (int i = 2; i <= rows; i++)
            {
                var state = new State()
                {
                    IbgeCode = Convert.ToInt32(states.Cells[i, IBGE].Value),
                    Id = Guid.NewGuid(),
                    Initials = Convert.ToString(states.Cells[i, UF].Value),
                    LastChangeDate = DateTime.UtcNow,
                    Name = Convert.ToString(states.Cells[i, Estado].Value),
                    NumberCities = Convert.ToInt32(states.Cells[i, QtdMun].Value),
                    Region = Convert.ToString(states.Cells[i, Regiao].Value),
                    RegisterDate = DateTime.UtcNow,
                    Status = RecordStatus.Active
                };

                result.Add(state);
            }

            return result;
        }

        public List<City> LoadExcelData(FileInfo excelFile)
        {
            ExcelPackage package = new ExcelPackage(excelFile);

            var states = this.GetStates(package.Workbook.Worksheets[1]);
            var cities = this.GetCities(package.Workbook.Worksheets[0], states);

            return cities;
        }

        public List<State> LoadExcelStateData(FileInfo excelFile)
        {
            ExcelPackage package = new ExcelPackage(excelFile);

            var states = this.GetStates(package.Workbook.Worksheets[1]);

            return states;
        }

        private List<City> GetCities(ExcelWorksheet cities, List<State> states)
        {
            const int IBGE = 2;
            const int IBGE7 = 3;
            const int UF = 4;
            const int Municipio = 5;
            const int Regiao = 6;
            const int Populacao = 7;
            const int Porte = 8;
            const int Capital = 9;

            int rows = cities.Dimension.Rows;

            var result = new List<City>();

            for (int i = 2; i <= rows; i++)
            {
                var city = new City()
                {
                    Ibge7Code = Convert.ToInt32(cities.Cells[i, IBGE7].Value),
                    IbgeCode = Convert.ToInt32(cities.Cells[i, IBGE].Value),
                    Id = Guid.NewGuid(),
                    IsCapital = !string.IsNullOrEmpty(Convert.ToString(cities.Cells[i, Capital].Value)),
                    LastChangeDate = DateTime.UtcNow,
                    Name = Convert.ToString(cities.Cells[i, Municipio].Value),
                    Population = Convert.ToInt32(cities.Cells[i, Populacao].Value),
                    Region = Convert.ToString(cities.Cells[i, Regiao].Value),
                    RegisterDate = DateTime.UtcNow,
                    Size = Convert.ToString(cities.Cells[i, Porte].Value),
                    State = states.FirstOrDefault(s => s.Initials.Equals(Convert.ToString(cities.Cells[i, UF].Value))),
                    Status = RecordStatus.Active
                };

                using (MD5 md5 = MD5.Create())
                {
                    var value = $"{city.Ibge7Code}{city.IbgeCode}{city.Name}";
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    for (int x = 0; x < hashBytes.Length; x++)
                    {
                        sb.Append(hashBytes[x].ToString("X2"));
                    }

                    city.HashId = sb.ToString();
                }

                result.Add(city);
            }

            return result;
        }
    }
}
