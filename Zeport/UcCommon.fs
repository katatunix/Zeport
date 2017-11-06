namespace Zeport

open Suave
open Suave.Successful
open UiCommon
open MySql.Data.MySqlClient

module UcCommon =

    let makeWebPart output ctx =
        async {
            let! o = output
            match o with
            | Output.Text text ->
                return! OK text ctx
            | Output.Redirect path ->
                return! Redirection.FOUND path ctx }

    let user (f : User option -> WebPart) =
        context (Session.getUser >> f)

    let userequest (f : User option -> HttpRequest -> WebPart) =
        context (fun ctx -> f (Session.getUser ctx) ctx.request)

    let userconn (f : User option -> MySqlConnection -> WebPart) =
        context (fun ctx -> use conn = Db.openConn () in f (Session.getUser ctx) conn)

    let requestconn (f : HttpRequest -> MySqlConnection -> WebPart) =
        context (fun ctx -> use conn = Db.openConn () in f ctx.request conn)

    let userequestconn (f : User option -> HttpRequest -> MySqlConnection -> WebPart) =
        context (fun ctx -> use conn = Db.openConn () in f (Session.getUser ctx) ctx.request conn)

    let navi conn =
        buildNavi (Db.findTeamsByParent conn) (Db.findProjectsByParent conn)
