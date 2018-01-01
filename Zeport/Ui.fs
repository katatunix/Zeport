namespace Zeport

open Suave.DotLiquid

module Ui =

    do setCSharpNamingConvention ()

    let mutable private templatesDir = "."
    let init dir = templatesDir <- dir

    let renderTemplate file =
        renderPageFile (System.IO.Path.Combine (templatesDir, file))

    type FinalOutput =
        | Text of string
        | Redirect of string

    type ZoneOutput =
        | Text of string * string // title * content
        | Redirect of string

    type DisplayMessage = {
        Success : bool
        Content : string }
