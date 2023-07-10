using System.Data.SqlClient;
using Dapper;

static class BD {
    private static string _connectionString = 
        @"Server=localhost;DataBase=Elecciones2023;Trusted_Connection=True;";
    
    public static void AgregarCandidato(Candidato can){
        string SQL = "INSERT INTO Candidato(IdPartido, Apellido, Nombre, FechaNacimiento, Foto, Postulacion) VALUES (@pIdPartido, @pApellido, @pNombre, @pFechaNacimiento, @pFoto, @pPostulacion)";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(SQL, new{pIdPartido = can.IdPartido, pApellido = can.Apellido, pNombre = can.Nombre, pFechaNacimiento = can.FechaNacimiento, pFoto = can.Foto, pPostulacion = can.Postulacion});
        }
    }

    public static void EliminarCandidato(int idCandidato){
        string SQL = "DELETE FROM Candidato WHERE IdCandidato = @pidCandidato";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            int registrosEliminados=db.Execute(SQL, new{pidCandidato = idCandidato});
        }
    }

    public static Partido VerInfoPartido(int idPartido){
        Partido partido;
        string SQL = "SELECT * FROM Partido WHERE IdPartido = @pidPartido";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            partido = db.QueryFirstOrDefault<Partido>(SQL, new{ pidPartido = idPartido });
        }
        return partido;
    }

    public static Candidato VerInfoCandidato(int idCandidato){
        Candidato candidato;
        string SQL = "SELECT * FROM Candidato WHERE IdCandidato = @pidCandidato";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            candidato = db.QueryFirstOrDefault<Candidato>(SQL, new{ pidCandidato = idCandidato });
        }
        return candidato;
    }

    public static List<Partido> ListarPartidos(){
        List<Partido> ListaPartidos;
        string SQL = "SELECT * FROM Partido";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            ListaPartidos = db.Query<Partido>(SQL).ToList();
        }
        return ListaPartidos;
    }

    public static List<Candidato> ListarCandidatos(int idPartido){
        List<Candidato> ListaCandidatos;
        string SQL = "SELECT * FROM Candidato WHERE IdPartido = @pidPartido";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            ListaCandidatos = db.Query<Candidato>(SQL, new { pidPartido = idPartido }).ToList();
        }
        return ListaCandidatos;
    }
    
}