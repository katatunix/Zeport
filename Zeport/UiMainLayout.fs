namespace Zeport

open UiCommon

module UiMainLayout =

    type Model = {
        ResPath     : string
        Title       : string
        Banner      : string
        Sidebar     : string
        Body        : string }

    let render user curBannerItem navi body =
        async {
            let! body = body
            match body with
            | Body.Redirect path ->
                return Output.Redirect path
            | Body.Content (title, bodyText) ->
                let! banner = UiBanner.render user curBannerItem
                let! sidebar = UiNavi.render navi
                let model = {
                    ResPath     = Path.Res
                    Title       = title
                    Banner      = banner
                    Sidebar     = sidebar
                    Body        = bodyText }
                let! html = renderTemplate "MainLayout.liquid" model
                return Output.Text html }
