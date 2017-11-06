namespace Zeport

open NghiaBui.Suave

[<AutoOpen>]
module Path =

    [<AllowNullLiteral>]
    type Path (homeUrl, homeDisk) =
        inherit WebPath (homeUrl, homeDisk)

        member x.Res        = x.Home + "/res"
        member x.Login      = x.Home + "/login"
        member x.Logout     = x.Home + "/logout"
        member x.Setting    = x.Home + "/setting"
        member x.Cpass      = x.Home + "/cpass"
        member x.Report     = x.Home + "/report"

    let mutable Path : Path = null

    let init homeUrl homeDisk =
        Path <- new Path (homeUrl, homeDisk)
