namespace Zeport

open UiCommon

module UiError =

    let renderAccessDenied navi user =
        async {
            let! html =
                renderMainLayout
                    "Access denied"
                    (renderBanner user Nope)
                    (UiNavi.render navi)
                    (renderTemplate "AccessDenied.liquid" Path.Res)
            return Text html }

    let render404 navi user =
        async {
            let! html =
                renderMainLayout
                    "Not found"
                    (renderBanner user Nope)
                    (UiNavi.render navi)
                    (renderTemplate "Error404.liquid" Path.Res)
            return Text html }
