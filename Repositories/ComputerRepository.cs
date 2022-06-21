using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
    {
    private DatabaseConfig databaseConfig;
    public ComputerRepository(DatabaseConfig databaseConfig) 
    {
        this.databaseConfig = databaseConfig;
    }

//1
public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var computers = connection.Query<Computer>("SELECT * FROM Computers");
        return computers;
    }


    public Computer Save(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)",computer);
        return computer;
    }

 //2   
    public Computer GetById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id", new {Id = id});
        return computer;
    }
//3
    public Computer Update(Computer computer)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id", computer);
        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("DELETE FROM Computers WHERE id = @Id", new {Id = id});
    }

//4
    public bool ExitsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        bool result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Computers WHERE id= @Id", new {Id = id}));
        return result; 
    }

}
