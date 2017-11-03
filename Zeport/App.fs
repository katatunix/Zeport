namespace Zeport

open Suave
open Suave.Filters
open Suave.Operators

open NghiaBui.Common

module App =

    let create homeUrl homeDisk sessionKeyPrefix =
        Zeport.Path.init homeUrl homeDisk
        Session.setKeyPrefix sessionKeyPrefix
        Ui.setTemplatesDir (System.IO.Path.Combine (homeDisk, "templates"))

        Session.init
        >=> choose [
            NghiaBui.Suave.resource Path.ConvertToDisk

            choose [ path Path.Home; path Path.HomeWithSlash ] >=> Uc.viewHome

            path Path.Login >=> choose [ GET >=> Uc.viewLogin; POST >=> Uc.doLogin ]
            path Path.Logout >=> Uc.logout ]

    let createDefault () = create "/zeport" "." "Zeport_"
