﻿using CurrencyConverter.DataLayer.Model;
using CurrencyConverter.DataLayer.Sqlite.Model;
using SQLite;

namespace CurrencyConverter.DataLayer.Sqlite
{
    public class SqliteDatabase
    {
        private static SqliteDatabase _instance;

        public readonly SQLiteConnection Database;

        private SqliteDatabase(string dbPath)
        {
            Database = new SQLiteConnection(dbPath);
            Database.CreateTable<SqliteConversionRate>();
            Database.CreateTable<SqliteConversionGroup>();
        }

        public static SqliteDatabase GetInstance(string dbPath)
        {
            return _instance ?? (_instance = new SqliteDatabase(dbPath));
        }
    }
}