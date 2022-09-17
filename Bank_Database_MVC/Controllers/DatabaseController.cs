﻿using Bank_Database_MVC.Data.Bank_DB;
using Bank_Database_MVC.Models;
using Bank_Database_MVC.Models.Bank_DB;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Bank_Database_MVC.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly Bank_DB_Context _context;
        private readonly IDistributedCache _distributedCache;
        private static int _instanceCounter = 0;
        private Bank_Tabelle[] _query;
        private string _recordKey;
        private const string _databaseSource = "data fetched from database";
        private const string _distributedCacheSource = "data fetched from distributed cache";

        public DatabaseController(ILogger<DatabaseController> logger, Bank_DB_Context context, IDistributedCache distributedCache)
        {
            _logger = logger;
            _context = context;
            _distributedCache = distributedCache;
            _instanceCounter++;
        }

        public async Task<IActionResult> QueryPageAsync()
        {
            _query = null;

            //string recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Bank_DB", "Bank_Tabelle-w-o-PK.json"), FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    foreach (Bank_Tabelle dataSet in jsonData)
                    {
                        _context.Add(dataSet);
                    }

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                    _context.SaveChanges();

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");

                    //ViewBag.QueryResult = jsonData.ToArray();
                    //ViewBag.DataSource = _databaseSource;
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }

                else
                {
                    //ViewBag.QueryResult = queryResult.ToArray();
                    //ViewBag.DataSource = _databaseSource;
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }
            }

            else
            {
                //ViewBag.QueryResult = _query;
                //ViewBag.DataSource = _distributedCacheSource;
            }


            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View();
        }

        public async Task<IActionResult> SeedAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "Seed_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "Seed_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Bank_DB", "Bank_Tabelle-w-o-PK.json"), FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    foreach (Bank_Tabelle dataSet in jsonData)
                    {
                        _context.Add(dataSet);
                    }

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                    _context.SaveChanges();

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");

                    ViewBag.QueryResult = jsonData.ToArray();
                    ViewBag.DataSource = _databaseSource;
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }

                else
                {
                    ViewBag.QueryResult = queryResult.ToArray();
                    ViewBag.DataSource = _databaseSource;
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        public async Task<IActionResult> DatabaseAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Bank_DB", "Bank_Tabelle-w-o-PK.json"), FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    foreach (Bank_Tabelle dataSet in jsonData)
                    {
                        _context.Add(dataSet);
                    }

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                    _context.SaveChanges();

                    // doesn't work with Entity Framework Core ???
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");

                    ViewBag.QueryResult = jsonData.ToArray();
                    ViewBag.DataSource = _databaseSource;
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }

                else
                {
                    ViewBag.QueryResult = queryResult.ToArray();
                    ViewBag.DataSource = _databaseSource;
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(_recordKey, _query);
                }
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        [HttpPost]
        public async Task<IActionResult> NameQueryAsync(string name)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "NameQuery_" + name + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "NameQuery_" + name + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.Bezeichnung.ToUpper().Contains(name.ToUpper()) || modelData.Kurzbezeichnung.ToUpper().Contains(name.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToArray();
                ViewBag.DataSource = _databaseSource;
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(_recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        [HttpPost]
        public async Task<IActionResult> PLZqueryAsync(int PLZ)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "PLZquery_" + PLZ.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "PLZquery_" + PLZ.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.PLZ == PLZ
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToArray();
                ViewBag.DataSource = _databaseSource;
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(_recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        [HttpPost]
        public async Task<IActionResult> OrtQueryAsync(string ort)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "OrtQuery_" + ort + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "OrtQuery_" + ort + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.Ort.ToUpper().Contains(ort.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToArray();
                ViewBag.DataSource = _databaseSource;
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(_recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        [HttpPost]
        public async Task<IActionResult> BLZqueryAsync(string BLZ)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "BLZquery_" + BLZ + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "BLZquery_" + BLZ + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.BLZ == BLZ
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToArray();
                ViewBag.DataSource = _databaseSource;
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(_recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        [HttpPost]
        public async Task<IActionResult> BICqueryAsync(string BIC)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _query = null;

            //string recordKey = "BICquery_" + BIC + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _recordKey = "BICquery_" + BIC + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(_recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.BIC.ToUpper().Contains(BIC.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToArray();
                ViewBag.DataSource = _databaseSource;
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(_recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
                ViewBag.DataSource = _distributedCacheSource;
            }

            stopwatch.Stop();
            ViewBag.Stopwatch = " in " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View("QueryResult");
        }

        public IActionResult Privacy()
        {
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.ControllerInstanceCounter = _instanceCounter.ToString();
            ViewBag.ContextInstanceCounter = Bank_DB_Context.instanceCounter.ToString();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
