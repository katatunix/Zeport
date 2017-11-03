namespace Zeport

open Domain
open Ui

module UiLogin =

    let USERNAME_FIELD = "u"
    let PASSWORD_FIELD = "p"

    type Model = {
        UsernameField : string
        PasswordField : string
        Message : string option
        Username : string }

    let renderStay message username =
        async {
            let model = {
                UsernameField = USERNAME_FIELD
                PasswordField = PASSWORD_FIELD
                Message = message
                Username = username }
            let! html =
                UiMainLayout.render
                    "Please login"
                    (UiBanner.render None UiBanner.Item.Login)
                    (UiSidebar.renderProjectTree ())
                    (renderTemplate "Login.liquid" model)
            return Text html }

    let renderView (result : ViewLoginResult) =
        async {
            match result with
            | GoHome ->
                return Redirect Path.Home
            | Stay ->
                return! renderStay None "" }

    let renderDo (result : DoLoginResult) =
        async {
            match result with
            | Success _ ->
                return Redirect Path.Home
            | Failed (Username username) ->
                let message = Some "Unable to login: incorrect username or password"
                return! renderStay message username }
