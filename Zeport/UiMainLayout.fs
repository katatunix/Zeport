namespace Zeport

open Ui

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
            | Redirect path ->
                return FinalOutput.Redirect path
            | Text (title, text) ->
                let! banner = UiBanner.render user curBannerItem
                let! sidebar = UiNavi.render navi
                let model = {
                    ResPath     = Path.Res
                    Title       = title
                    Banner      = banner
                    Sidebar     = sidebar
                    Body        = text }
                let! fullHtml = renderTemplate "MainLayout.liquid" model
                return FinalOutput.Text fullHtml }
