namespace Zeport

open MySql.Data.MySqlClient

module Db =

    let private SERVER      = "localhost"
    let private PORT        = 3306
    let private USERNAME    = "root"
    let private PASSWORD    = "root"
    let private DB_NAME     = "zeport"

    let private CONN_STRING =
        sprintf "Server=%s; Port=%d; UserID=%s; Password=%s; Database=%s"
                    SERVER PORT USERNAME PASSWORD DB_NAME
    
    let openConn () =
        let conn = new MySqlConnection (CONN_STRING)
        conn.Open ()
        conn

    let checkLogin conn (username : Username) (password : string) =
        use cmd = new MySqlCommand ("SELECT * FROM user WHERE username=@U AND password=@P", conn)
        cmd.Prepare ()
        cmd.Parameters.AddWithValue ("@U", username.Value) |> ignore
        cmd.Parameters.AddWithValue ("@P", password) |> ignore
        use reader = cmd.ExecuteReader ()
        if reader.Read () then
            Some {
                Id = reader.GetInt32 "id"
                Username = reader.GetString "username" |> Username
                IsAdmin = reader.GetInt32 "is_admin" = 1 }
        else
            None

    let checkPassword (conn : MySqlConnection) (userId : int) (password : string) =
        true

    let updatePassword (conn : MySqlConnection) (userId : int) (password : string) =
        if true then
            Ok ()
        else
            Error (sprintf "Could not found the user with ID %d" userId)

    //===================================================================================================
    // TODO
    let team1 : Team = { Id = 1; Name = "Team1"; Des = None }
    let team2 : Team = { Id = 2; Name = "Team2"; Des = None }
    let project1 : Project = { Id = 1; Name = "Project1"; Des = None }
    let project2 : Project = { Id = 2; Name = "Project2"; Des = None }
    let project3 : Project = { Id = 3; Name = "Project3"; Des = None }
    let project4 : Project = { Id = 4; Name = "Project4"; Des = None }

    let findTeamsByParent conn = function
        | None -> [ team1; team2 ]
        | Some _ -> []

    let findProjectsByParent conn = function
        | None -> []
        | Some 1 -> [ project1; project2 ]
        | Some 2 -> [ project3; project4 ]
        | _ -> []
