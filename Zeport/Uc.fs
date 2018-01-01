namespace Zeport

open Suave
open Suave.Operators

open Controller

module Uc =

    let view404 =
        userconn (fun user conn ->
            UiError.render404 ()
            |> UiMainLayout.render user UiBanner.Nope (navi conn)
            |> makeWebPart)

    let viewHome =
        userconn (fun user conn ->
            UiHome.render ()
            |> UiMainLayout.render user UiBanner.Home (navi conn)
            |> makeWebPart)

    let logout =
        Session.clearUser ()
        >=> Redirection.FOUND Path.Home

    let viewLogin =
        userconn (fun user conn ->
            user.IsSome
            |> viewLogin
            |> UiLogin.renderView
            |> UiMainLayout.render None UiBanner.Login (navi conn)
            |> makeWebPart)

    let doLogin =
        requestconn (fun request conn ->
            let loginResult = request
                                |> parseDoLogin
                                ||> doLogin (Db.checkLogin conn)
            (loginResult |> updateSession)
            >=> (   loginResult
                    |> UiLogin.renderDo
                    |> UiMainLayout.render None UiBanner.Login (navi conn)
                    |> makeWebPart ))

    let viewCpass =
        userconn (fun user conn ->
            user.IsSome
            |> viewCpass
            |> UiCpass.renderView
            |> UiMainLayout.render user UiBanner.Cpass (navi conn)
            |> makeWebPart)

    let doCpass =
        userequestconn (fun user request conn ->
            parseDoCpass request
            |||> doCpass user (Db.checkPassword conn)
            |> Result.bind (fun (user, password) ->
                Db.updatePassword conn user.Id password |> accessDeniedResult)
            |> UiCpass.renderDo
            |> UiMainLayout.render user UiBanner.Cpass (navi conn)
            |> makeWebPart)
