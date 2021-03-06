﻿namespace Zeport

[<AutoOpen>]
module Base =

    type Username = Username of string with
        member x.Value = let (Username value) = x in value

    type User = {
        Id : int
        Username : Username
        IsAdmin : bool }

    type Team = {
        Id : int
        Name : string
        Des : string option }
    
    type Project = {
        Id : int
        Name : string
        Des : string option }

    type NaviNode =
        | TeamNode of Team * (NaviNode list)
        | ProjectNode of Project

    type Navi = NaviNode list

    type AccessDeniedError<'T> =
        | AccessDenied
        | Other of 'T

    let buildNavi findTeamsByParent findProjectsByParent : Navi =
        let rec buildTeam (team : Team) =
            let projectNodes =
                findProjectsByParent (Some team.Id)
                |> List.map ProjectNode
            let teamNodes =
                findTeamsByParent (Some team.Id)
                |> List.map buildTeam
            TeamNode (team, teamNodes @ projectNodes)

        let rootTeamNodes =
            findTeamsByParent None |> List.map buildTeam
        let rootProjectsNodes =
            findProjectsByParent None |> List.map ProjectNode
        rootTeamNodes @ rootProjectsNodes

    let accessDeniedResult = function
        | Ok x -> Ok x
        | Error x -> Error (Other x)
