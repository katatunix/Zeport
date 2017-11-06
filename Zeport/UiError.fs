namespace Zeport

open UiCommon

module UiError =

    let renderAccessDenied () =
        async {
            let! html = renderTemplate "AccessDenied.liquid" Path.Res
            return Content ("Access denied", html) }

    let render404 () =
        async {
            let! html = renderTemplate "Error404.liquid" Path.Res
            return Content ("Not found", html) }
