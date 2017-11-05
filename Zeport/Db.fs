namespace Zeport

module Db =

    let checkLogin (username : Username) (password : string) =
        match username.Value, password with
        | "nghia.buivan", "12345678" ->
            Some { Id = 1; Username = username; IsAdmin = true }
        | _ ->
            None

    let checkPassword (userId : int) (password : string) =
        true

    let updatePassword (userId : int) (password : string) =
        if true then
            Ok ()
        else
            Error (sprintf "Could not found the user with ID %d" userId)
