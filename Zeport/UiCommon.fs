namespace Zeport

open Suave.DotLiquid

module UiCommon =

    do setCSharpNamingConvention ()

    let mutable private templatesDir = "."
    let init dir = templatesDir <- dir

    type Output =
        | Text of string
        | Redirect of string

    type Message = {
        Success : bool
        Content : string }

    type MainLayoutModel = {
        ResPath     : string
        Title       : string
        Banner      : string
        Sidebar     : string
        Body        : string }

    type BannerItem =
        | Nope
        | Home
        | Login
        | Setting
        | Cpass

    type BannerItemInfo = {
        Url : string
        Css : string }

    type BannerModel = {
        Home        : BannerItemInfo
        Login       : BannerItemInfo
        LogoutUrl   : string
        Setting     : BannerItemInfo
        Cpass       : BannerItemInfo
        Username    : string option }

    let renderTemplate file =
        renderPageFile (System.IO.Path.Combine (templatesDir, file))

    let renderMainLayout title banner sidebar body =
        async {
            let! banner = banner
            let! sidebar = sidebar
            let! body = body
            let model = {
                ResPath     = Path.Res
                Title       = title
                Banner      = banner
                Sidebar     = sidebar
                Body        = body }
            return! renderTemplate "MainLayout.liquid" model }

    let renderBanner (user : User option) (currentItem : BannerItem) =
        let css item =
            if item = currentItem then " class=\"active\"" else ""
        let info url item =
            { Url = url; Css = css item }
        let model = {
            Home        = info Path.Home Home
            Login       = info Path.Login Login
            LogoutUrl   = Path.Logout
            Setting     = info Path.Setting Setting
            Cpass       = info Path.Cpass Cpass
            Username    = user |> Option.map (fun u -> u.Username.Value) }
        renderTemplate "Banner.liquid" model
