namespace Zeport

open Suave.DotLiquid

module UiCommon =

    do setCSharpNamingConvention ()

    let mutable private templatesDir = "."
    let init dir = templatesDir <- dir

    let renderTemplate file =
        renderPageFile (System.IO.Path.Combine (templatesDir, file))

    type Output =
        | Text of string
        | Redirect of string

    type Body =
        | Content of string * string
        | Redirect of string

    type Message = {
        Success : bool
        Content : string }
