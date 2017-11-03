namespace Zeport

module UiMainLayout =

    type Model = {
        HomePath    : string
        Title       : string
        Banner      : string
        Sidebar     : string
        Body        : string }

    let render title banner sidebar body =
        async {
            let! banner = banner
            let! sidebar = sidebar
            let! body = body
            let model = {
                HomePath    = Path.Home
                Title       = title
                Banner      = banner
                Sidebar     = sidebar
                Body        = body }
            return! Ui.renderTemplate "MainLayout.liquid" model }
