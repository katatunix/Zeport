namespace Zeport

open Domain

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
        CurrentUser : User option }

    let render (user : User option) (currentItem : Item) =
        let css item =
            if item = currentItem then " class=\"active\"" else ""
        let info url css =
            { Url = url; Css = css }

        async {
            let model = {
                Home        = info Path.Home (css Home)
                Login       = info Path.Login (css Login)
                LogoutUrl   = Path.Logout
                Setting     = info Path.Setting (css Setting)
                Cpass       = info Path.Cpass (css Cpass)
                CurrentUser = user }
            return! Ui.renderTemplate "Banner.liquid" model }
