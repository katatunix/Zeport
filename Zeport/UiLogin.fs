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

    let private render username errorMessage =
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
                    (renderProjectTree ())
                    (renderTemplate "Login.liquid" model)
            return Text html }

    let renderView (result : ViewLoginResult) =
        async {
            match result with
            | GoHome ->
                return Redirect Path.Home
            | Stay ->
                return! render "" None }

    let renderDo result =
        async {
            match result with
            | Ok _ ->
                return Redirect Path.Home
            | Error (Username username) ->
                let errorMessage = Some "Unable to login: incorrect username or password"
                return! render username errorMessage }
