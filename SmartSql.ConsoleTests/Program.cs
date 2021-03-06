﻿using SmartSql.Abstractions;
using SmartSql.ZooKeeperConfig;
using System;
using System.Threading;
using System.Linq;
using SmartSql.Abstractions.Logging;
using Microsoft.Extensions.Logging;
namespace SmartSql.ConsoleTests
{
    class Program
    {
        static ILoggerFactory loggerFactory = new LoggerFactory();
        static void Main(string[] args)
        {
            loggerFactory.AddConsole(LogLevel.Trace);
            loggerFactory.AddDebug(LogLevel.Trace);

            LocalFileConfigLoader_Test();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


        static void LocalFileConfigLoader_Test()
        {

            var sqlMapper = new SmartSqlMapper(loggerFactory);
            while (true)
            {
                string ipt = Console.ReadLine();
                var list = sqlMapper.Query<T_Test>(new RequestContext
                {
                    Scope = "T_Test",
                    SqlId = "GetList",
                    Request = new { Ids = new long[] { 1, 2, 3, 4 } }
                });
            }


        }

        static void ZooKeeperConfigLoader_Test()
        {
            string connStr = "192.168.31.103:2181";//192.168.31.103:2181 192.168.1.5:2181,192.168.1.5:2182,192.168.1.5:2183
            var configLoader = new ZooKeeperConfigLoader(connStr);
            string configPath = "/Config/App1/SmartSqlMapConfig.xml";
            var SqlMapper = new SmartSqlMapper(loggerFactory, configPath, configLoader);

            int i = 0;
            for (i = 0; i < 10; i++)
            {
                Console.ReadLine();
                var list = SqlMapper.Query<T_Test>(new RequestContext
                {
                    Scope = "T_Test",
                    SqlId = "GetList",
                    Request = new { Ids = new long[] { 1, 2, 3, 4 } }
                });
                Console.WriteLine($"{list.Count()}");
            }
        }
    }


    public class T_Test
    {
        public long Id { get; set; }
        public String Name { get; set; }
    }
}