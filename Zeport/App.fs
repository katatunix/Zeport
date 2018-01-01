namespace Zeport

open Suave
open Suave.Filters
open Suave.Operators

module App =

    let create homeUrl homeDisk sessionKeyPrefix =
        Zeport.Path.init homeUrl homeDisk
        Session.setKeyPrefix sessionKeyPrefix
        Ui.init (System.IO.Path.Combine (homeDisk, "templates"))

        Session.init
        >=> choose [
            NghiaBui.MySuave.Main.resource Path.GetTail

            choose [ path Path.Home; path Path.HomeWithSlash ] >=> Uc.viewHome

            path Path.Login >=> choose [ GET >=> Uc.viewLogin; POST >=> Uc.doLogin ]
            path Path.Logout >=> Uc.logout
            path Path.Cpass >=> choose [ GET >=> Uc.viewCpass; POST >=> Uc.doCpass ]

            Uc.view404 ]

    let createDefault () = create "/zeport" "." "Zeport_"
