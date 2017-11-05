namespace Zeport

[<AutoOpen>]
module DomainCommon =

    type Username = Username of string with
        member x.Value = let (Username value) = x in value

    type User = {
        Id : int
        Username : Username
        IsAdmin : bool }

    type MaybeAccessDeniedError<'T> =
        | AccessDenied
        | Other of 'T

    let accessDeniedResult = function
        | Ok x -> Ok x
        | Error x -> Error (Other x)
