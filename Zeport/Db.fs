namespace Zeport

module Db =

    let checkLogin (username : Username) (password : string) =
        match username.Value, password with
        | "nghia.buivan", "12345678" ->
            Some { Username = username; IsAdmin = true }
        | _ ->
            None

    let updatePassword (username : Username) (password : string) =
        if true then
            Ok ()
        else
            Error ("Could not found the user with Username: " + username.Value)
