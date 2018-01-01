namespace Zeport

[<AutoOpen>]
module Login =

    type ViewResult =
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
