namespace Zeport

open Domain

module Db =

    let checkLogin (username : Username) (password : string) =
        match username.Value, password with
        | "nghia.buivan", "12345678" ->
            Some { Username = username; Role = Admin }
        | _ ->
            None
