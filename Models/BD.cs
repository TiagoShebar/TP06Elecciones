using System.Data.SqlClient;
using Dapper;

static class BD {
    private static string _connectionString = 
        @"Server=localhost;DataBase=Elecciones2023;Trusted_Connection=True;";
    
    public static void AgregarCandidato(Candidato can){
        string SQL = "INSERT INTO Candidato(IdPartido, Apellido, Nombre, FechaNacimiento, Foto, Postulacion) VALUES (@pIdPartido, @pApellido, @pNombre, @pFechaNacimiento, @pFoto, @pPostulacion)";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(SQL, new{pIdPartido = can.IdPartido, pApellido = can.Apellido, pNombre = can.Nombre, pFechaNacimiento = can.FechaNacimiento, pFoto = can.Foto, pPostulacion = can.Postulacion});
            /*if(can.Postulacion == "DIPUTADO" || can.Postulacion == "DIPUTADA"){
                SQL = "UPDATE Partidos SET CantidadDiputados = CantidadDiputados+1 WHERE IdPartido = @pIdPartido";
                int rowsAffected = db.Execute(SQL, new {pIdPartido = can.IdPartido});
            }
            else if(can.Postulacion == "SENADOR" || can.Postulacion == "SENADORA"){
                SQL = "UPDATE Partidos SET CantidadSenadores = CantidadSenadores+1 WHERE IdPartido = @pIdPartido";
                int rowsAffected = db.Execute(SQL, new {pIdPartido = can.IdPartido});
            }*/
        }
    }

    public static void EliminarCandidato(int idCandidato){
        string SQL = "DELETE FROM Candidato WHERE IdCandidato = @pidCandidato";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            int registrosEliminados=db.Execute(SQL, new{pidCandidato = idCandidato});
            /*if(can.Postulacion.ToUpper() == "DIPUTADO" || can.Postulacion.ToUpper() == "DIPUTADA"){
                SQL = "UPDATE Partidos SET CantidadDiputados = CantidadDiputados-1 WHERE IdPartido = @pIdPartido";
                int rowsAffected = db.Execute(SQL, new {can.pIdPartido});
            }
            else if(can.Postulacion.ToUpper() == "SENADOR" || can.Postulacion.ToUpper() == "SENADORA"){
                SQL = "UPDATE Partidos SET CantidadSenadores = CantidadSenadores-1 WHERE IdPartido = @pIdPartido";
                int rowsAffected = db.Execute(SQL, new {can.pIdPartido});
            }*/
        }
    }

    public static void AgregarPartido(Partido par){
        string SQL = "INSERT INTO Partido( Nombre, Logo, SitioWeb, FechaFundacion, CantidadDiputados, CantidadSenadores) VALUES (@pNombre, @pLogo, @pSitioWeb, @pFechaFundacion, @pCantidadDiputados, @pCantidadSenadores)";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(SQL, new{pNombre = par.Nombre, pLogo= par.Logo, pSitioWeb = par.SitioWeb, pFechaFundacion = par.FechaFundacion, pCantidadDiputados = par.CantidadDiputados, pCantidadSenadores = par.CantidadSenadores});
        }
    }

    public static void EliminarPartido(int idPartido){
        string SQL = "DELETE FROM Partido WHERE IdPartido = @pidPartido";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            int registrosEliminados=db.Execute(SQL, new{pidPartido = idPartido});
            SQL = "DELETE FROM Candidato WHERE IdPartido = @pidPartido";
            registrosEliminados=db.Execute(SQL, new{pidPartido = idPartido});
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