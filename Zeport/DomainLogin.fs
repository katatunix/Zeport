namespace Zeport

[<AutoOpen>]
module DomainLogin =

    type ViewLoginResult =
        | GoHome
        | Stay

    let viewLogin alreadyLogin =
        if alreadyLogin then
            GoHome
        else
            Stay

    let doLogin check username password =
        match check username password with
        | Some user ->
            Ok user
        | None ->
            Error username
