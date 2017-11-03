namespace Zeport

open Suave.DotLiquid

module Ui =

    type Output =
        | Text of string
        | Redirect of string

    do setCSharpNamingConvention ()

    let mutable private templatesDir = "."
    let setTemplatesDir dir = templatesDir <- dir

    let renderTemplate file =
        renderPageFile (System.IO.Path.Combine (templatesDir, file))
