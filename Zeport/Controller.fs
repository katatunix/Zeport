namespace Zeport

open Suave.Http
open Domain

module Controller =

    let parseDoLoginRequest (r : HttpRequest) =
        let username = r.Item UiLogin.USERNAME_FIELD |> Option.defaultValue "" |> Username
        let password = r.Item UiLogin.PASSWORD_FIELD |> Option.defaultValue ""
        username, password
