namespace Zeport

module Domain =

    type Role =
        | Admin

    type Username = Username of string with
        member x.Value = let (Username value) = x in value

    type User = {
        Username : Username
        Role : Role }

    type ViewLoginResult =
        | GoHome
        | Stay

    type DoLoginResult =
        | Success of User
        | Failed of Username
