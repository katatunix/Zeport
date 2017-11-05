namespace Zeport

open Suave.Http

module Controller =

    let private orEmpty = Option.defaultValue ""

    let parseDoLogin (r : HttpRequest) =
        let username = r.Item UiLogin.USERNAME_FIELD |> orEmpty |> Username
        let password = r.Item UiLogin.PASSWORD_FIELD |> orEmpty
        username, password

    let parseDoCpass (r : HttpRequest) =
        let c   = r.Item UiCpass.CUR_FIELD  |> orEmpty
        let n   = r.Item UiCpass.NEW_FIELD  |> orEmpty
        let n2  = r.Item UiCpass.NEW2_FIELD |> orEmpty
        c, n, n2
