namespace Zeport.UnitTests

open NUnit.Framework
open Zeport

module NaviTest =

    [<Test>]
    let ``test buildNavi simple`` () =
        let team1 : Team = { Id = 1; Name = "Team1"; Des = None }
        let team2 : Team = { Id = 2; Name = "Team2"; Des = None }
        let project1 : Project = { Id = 1; Name = "Project1"; Des = None }
        let project2 : Project = { Id = 2; Name = "Project2"; Des = None }
        let project3 : Project = { Id = 3; Name = "Project3"; Des = None }
        let project4 : Project = { Id = 4; Name = "Project4"; Des = None }

        let findTeamsByParent = function
            | None -> [ team1; team2 ]
            | Some _ -> []
        let findProjectsByParent = function
            | None -> []
            | Some 1 -> [ project1; project2 ]
            | Some 2 -> [ project3; project4 ]
            | _ -> []

        let expected = [
            TeamNode (team1, [  ProjectNode project1
                                ProjectNode project2 ])
            TeamNode (team2, [  ProjectNode project3
                                ProjectNode project4 ]) ]

        let result = buildNavi findTeamsByParent findProjectsByParent

        Assert.AreEqual (expected, result)
