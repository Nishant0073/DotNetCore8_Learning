using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextDapper dapper = new(configuration);
            DataContextEF dataContextEF = new(configuration);
            string sqlcommand = "SELECT GETDATE()";
            DateTime right = dapper.LoadDataSingle<DateTime>(sqlcommand);
            // Console.WriteLine(right);

            Computer computer = new()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.2m,
                VideoCard = "RTX 2060",
            };

            dataContextEF.Add(computer);
            dataContextEF.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer(
                ComputerId,
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES( '" + computer.ComputerId
            + "','" + computer.Motherboard
            + "','" + computer.HasWifi
            + "','" + computer.HasLTE
            + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")
            + "','" + computer.Price
            + "','" + computer.VideoCard
            + "')";

            // Console.WriteLine(sql);

            // bool result = dapper.ExecuteSql(sql);
            // Console.WriteLine(result);

            String sqlSelect = @"SELECT 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                from  TutorialAppSchema.Computer ";

            // IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            IEnumerable<Computer> computersEf =  dataContextEF.Computer.AsList<Computer>();

            foreach (Computer singleComputer in computersEf)
            {
                Console.WriteLine("'" + computer.ComputerId
                + "','" + computer.Motherboard
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")
                + "','" + computer.Price
                + "','" + computer.VideoCard);
            }
        }
    }
}