namespace Zeport

open Domain

module DomainFuns =

    let viewLogin alreadyLogin =
        if alreadyLogin then
            GoHome
        else
            Stay

    let doLogin check (username, password) =
        match check username password with
        | Some user ->
            Success user
        | None ->
            Failed username
