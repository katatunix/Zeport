namespace Zeport

open UiCommon

module UiBanner =

    type Item =
        | Nope
        | Home
        | Login
        | Setting
        | Cpass

    type ItemInfo = {
        Url : string
        Css : string }

    type Model = {
        Home        : ItemInfo
        Login       : ItemInfo
        LogoutUrl   : string
        Setting     : ItemInfo
        Cpass       : ItemInfo
        Username    : string option }

    let render (user : User option) (currentItem : Item) =
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
