namespace Zeport

open UiCommon

module UiLogin =

    let USERNAME_FIELD = "u"
    let PASSWORD_FIELD = "p"

    type Model = {
        Title : string
        UsernameField : string
        PasswordField : string
        ErrorMessage : string option
        Username : string }

    let private render navi username errorMessage =
        async {
            let TITLE = "Please login"
            let model = {
                Title = TITLE
                UsernameField = USERNAME_FIELD
                PasswordField = PASSWORD_FIELD
                ErrorMessage = errorMessage
                Username = username }
            let! html =
                renderMainLayout
                    TITLE
                    (renderBanner None Login)
                    (UiNavi.render navi)
                    (renderTemplate "Login.liquid" model)
            return Text html }

    let renderView navi (result : ViewLoginResult) =
        async {
            match result with
            | GoHome ->
                return Redirect Path.Home
            | Stay ->
                return! render navi "" None }

    let renderDo navi result =
        async {
            match result with
            | Ok _ ->
                return Redirect Path.Home
            | Error (Username username) ->
                let errorMessage = Some "Unable to login: incorrect username or password"
                return! render navi username errorMessage }
