namespace Zeport

[<AutoOpen>]
module DomainCommon =

    type MaybeAccessDeniedError<'T> =
        | AccessDenied
        | Other of 'T

    let accessDeniedResult = function
        | Ok x -> Ok x
        | Error x -> Error (Other x)

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

    let buildNavi findTeamsByParent findProjectsByParent : Navi =
        let rec buildTeam (team : Team) =
            let projectNodes =
                findProjectsByParent (Some team.Id)
                |> List.map ProjectNode
            let teamNodes =
                findTeamsByParent (Some team.Id)
                |> List.map buildTeam
            TeamNode (team, teamNodes @ projectNodes)

        let rootTeams = findTeamsByParent None |> List.map buildTeam
        let rootProjects = findProjectsByParent None |> List.map ProjectNode
        rootTeams @ rootProjects
