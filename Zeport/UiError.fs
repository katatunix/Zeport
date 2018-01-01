namespace Zeport

open Ui

module UiError =

    let renderAccessDenied () =
        async {
            let! html = renderTemplate "AccessDenied.liquid" Path.Res
            return Text ("Access denied", html) }

    let render404 () =
        async {
            let! html = renderTemplate "Error404.liquid" Path.Res
            return Text ("Not found", html) }
