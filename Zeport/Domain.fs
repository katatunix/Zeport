namespace Zeport

module Domain =

    type Username = Username of string with
        member x.Value = let (Username value) = x in value

    type User = {
        Username : Username
        IsAdmin : bool }

    type ViewLoginResult =
        | GoHome
        | Stay

    type DoLoginResult =
        | Success of User
        | Failed of Username
