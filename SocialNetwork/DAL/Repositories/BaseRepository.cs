using System.Data.SQLite;
using System.Data;
using Dapper;

namespace SocialNetwork.DAL.Repositories;
public class BaseRepository
{
  protected T QueryFirstOrDefault<T>(string sql, object parametres = null)
  {
    using(var connection = CreateConnection())
    {
      connection.Open();
      return connection.QueryFirstOrDefault<T>(sql, parametres);
    }
  }

  protected List<T> Query<T>(string sql, object parametres = null)
  {
    using(var conn = CreateConnection())
    {
      conn.Open();
      return conn.Query<T>(sql,parametres).ToList();
    }
  }

  protected int Execute(string sql, object parametres = null)
  {
    using(var conn = CreateConnection())
    {
      conn.Open();
      return conn.Execute(sql, parametres);
    }
  }

  private static SQLiteConnection CreateConnection() => new("Data Source=DAL/DB/csf_social_network_db.db;");
}