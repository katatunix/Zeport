namespace Zeport

open System.Text

open Ui

module UiNavi =

    let private reportLink projectId =
        sprintf "%s?p=%d" Path.Report projectId

    let private renderProject (sb : StringBuilder) (project : Project) =
        sb  .Append("""<li data-jstree='{"icon":"glyphicon glyphicon-file"}'>""")
            .Append("<a href=\"").Append(reportLink project.Id).Append("\">")
                .Append(project.Name).Append("</a>")
            .Append("""</li>""") |> ignore

    let private isTeamNode = function
        | TeamNode _ -> true
        | _ -> false

    let rec private renderNode (sb : StringBuilder) = function
        | ProjectNode project ->
            renderProject sb project
        | TeamNode (team, childNodes) ->
            sb.Append("<li>").Append(team.Name).Append("<ul>") |> ignore
            let teamNodes, projectNodes = childNodes |> List.partition isTeamNode
            teamNodes @ projectNodes
            |> List.iter (renderNode sb)
            sb.Append("</ul></li>") |> ignore

    let render (navi : Navi) =
        let sb = StringBuilder ()
        navi |> List.iter (renderNode sb)
        renderTemplate "Navi.liquid" (sb.ToString ())
